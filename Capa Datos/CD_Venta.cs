using Capa_Entidad;
using CapaDatos;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace Capa_Datos
{
    public class CD_Venta
    {
        // Misma cadena de conexión que usa ClsDataBase. Acá la repetimos porque
        // este método necesita manejar la conexión él mismo (para poder abrir
        // una transacción), en vez de dejar que ClsDataBase abra una por llamada.
        private const string cadenaConexion = "Server=localhost; Database=petshopdellitoral; Uid=root; Pwd=;";

        public DataSet ListarMetodosPago()
        {
            ClsDataBase database = new ClsDataBase();
            database.NameSP = "sp_ListarMetodosPago";
            database.TableName = "TablaMetodosPago";
            database.EjecutarSelect();
            if (!string.IsNullOrEmpty(database.ErrorDB)) throw new Exception(database.ErrorDB);
            return database.DsResults;
        }

        public DataTable BuscarProductos(string texto)
        {
            ClsDataBase database = new ClsDataBase();
            database.NameSP = "sp_BuscarProductosVenta";
            database.TableName = "TablaProductosVenta";
            database.DtParameters.Rows.Add("p_texto", null, texto);
            database.EjecutarSelect();
            if (!string.IsNullOrEmpty(database.ErrorDB)) throw new Exception(database.ErrorDB);
            return database.DsResults.Tables["TablaProductosVenta"];
        }

        public DataTable ObtenerClientePorDni(string dni)
        {
            ClsDataBase database = new ClsDataBase();
            database.NameSP = "sp_ObtenerClientePorDni";
            database.TableName = "TablaClienteDni";
            database.DtParameters.Rows.Add("p_dni", null, dni);
            database.EjecutarSelect();
            if (!string.IsNullOrEmpty(database.ErrorDB)) throw new Exception(database.ErrorDB);
            return database.DsResults.Tables["TablaClienteDni"];
        }

        // proceso de venta en UNA transacción

        public int RegistrarVentaTransaccional(Venta objVenta, List<DetalleVenta> detalles)
        {
            // 1) Una sola conexión para TODO el proceso
            using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
            {
                conexion.Open();

                // 2) Se abre la transacción sobre esa conexión
                MySqlTransaction transaccion = conexion.BeginTransaction();

                try
                {
                    decimal subtotalLineas = 0;

                    // 3) Validar stock y precio real de cada producto, usando la MISMA conexión/transacción
                    foreach (DetalleVenta d in detalles)
                    {
                        using (MySqlCommand cmd = new MySqlCommand("sp_ObtenerStockProducto", conexion, transaccion))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("p_idProducto", d.IdProducto.IdProducto);

                            using (MySqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (!reader.Read())
                                {
                                    throw new Exception("El producto '" + d.IdProducto.Nombre_producto + "' ya no está disponible.");
                                }

                                int stockActual = Convert.ToInt32(reader["stock_producto"]);
                                decimal precioReal = Convert.ToDecimal(reader["precio_producto"]);

                                if (stockActual < d.Cantidad)
                                {
                                    throw new Exception("Stock insuficiente para '" + d.IdProducto.Nombre_producto +
                                                         "'. Disponible: " + stockActual + ".");
                                }

                                // El precio sale de la base (no del formulario). Subtotal de la línea con su descuento (%),
                                // redondeado a 2 decimales como lo hace la pantalla.
                                d.Precio = precioReal;
                                d.Subtotal_venta = Math.Round(precioReal * d.Cantidad * (1 - d.Descuento_detalle / 100m),
                                                              2, MidpointRounding.AwayFromZero);
                                subtotalLineas += d.Subtotal_venta;
                            }
                        }
                    }

                    // Total = suma de líneas menos el descuento (%) sobre la compra completa
                    decimal total = Math.Round(subtotalLineas * (1 - objVenta.Descuento_venta / 100m),
                                               2, MidpointRounding.AwayFromZero);

                    // 4) Insertar la cabecera de la venta
                    int idVenta;
                    using (MySqlCommand cmd = new MySqlCommand("sp_InsertarCabeceraVenta", conexion, transaccion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("p_idUsuario", objVenta.IdUsuario.idUsuario);
                        cmd.Parameters.AddWithValue("p_idCliente", objVenta.IdCliente.IdCliente);
                        cmd.Parameters.AddWithValue("p_idMetodoPago", objVenta.IdMetodoPago.IdMetodoPago);
                        cmd.Parameters.AddWithValue("p_fecha", objVenta.Fecha_venta.ToString("yyyy-MM-dd"));
                        cmd.Parameters.AddWithValue("p_total", total);
                        cmd.Parameters.AddWithValue("p_descuento", objVenta.Descuento_venta);

                        // El SP hace INSERT y después un SELECT LAST_INSERT_ID() AS idVenta;
                        // ExecuteScalar trae ese único valor.
                        idVenta = Convert.ToInt32(cmd.ExecuteScalar());
                    }

                    // 5) Insertar cada línea de detalle y descontar su stock
                    foreach (DetalleVenta d in detalles)
                    {
                        using (MySqlCommand cmd = new MySqlCommand("sp_InsertarDetalleVenta", conexion, transaccion))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("p_idVenta", idVenta);
                            cmd.Parameters.AddWithValue("p_idProducto", d.IdProducto.IdProducto);
                            cmd.Parameters.AddWithValue("p_cantidad", d.Cantidad);
                            cmd.Parameters.AddWithValue("p_precio", d.Precio);
                            cmd.Parameters.AddWithValue("p_subtotal", d.Subtotal_venta);
                            cmd.Parameters.AddWithValue("p_descuento", d.Descuento_detalle);
                            cmd.ExecuteNonQuery();
                        }

                        using (MySqlCommand cmd = new MySqlCommand("sp_DescontarStock", conexion, transaccion))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("p_idProducto", d.IdProducto.IdProducto);
                            cmd.Parameters.AddWithValue("p_cantidad", d.Cantidad);

                            // El SP hace UPDATE y después SELECT ROW_COUNT() AS filasActualizadas;
                            int filas = Convert.ToInt32(cmd.ExecuteScalar());

                            if (filas == 0)
                            {
                                // El stock cambió justo en este instante (otra venta se adelantó).
                                // Se corta todo acá, y el catch de abajo hace el Rollback.
                                throw new Exception("El stock de '" + d.IdProducto.Nombre_producto +
                                                     "' cambió mientras se procesaba la venta. Intentá de nuevo.");
                            }
                        }
                    }

                    // 6) Si se llegó hasta acá, TODO salió bien: se confirma de una
                    transaccion.Commit();
                    return idVenta;
                }
                catch (Exception)
                {
                    // 7) Si CUALQUIER paso de arriba falló, se deshace TODO:
                    // ni la venta, ni los detalles, ni el descuento de stock quedan guardados.
                    transaccion.Rollback();
                    throw;
                }
            }

        }

       /* private void CargarVentasRecientes()
        {
            try
            {
                CN_Venta cnVenta = new CN_Venta();
                DataTable dt = cnVenta.ObtenerVentasRecientes();

                // Si tu DataGridView del dashboard se llama dataGridView1:
                dataGridView1.DataSource = dt;

                // Opcional: Personalizar los nombres de las columnas para que se vean prolijas
                if (dataGridView1.Columns.Contains("idVenta")) dataGridView1.Columns["idVenta"].HeaderText = "N° Venta";
                if (dataGridView1.Columns.Contains("fecha_venta")) dataGridView1.Columns["fecha_venta"].HeaderText = "Fecha";
                if (dataGridView1.Columns.Contains("total_venta")) dataGridView1.Columns["total_venta"].HeaderText = "Total ($)";
                if (dataGridView1.Columns.Contains("cliente")) dataGridView1.Columns["cliente"].HeaderText = "Cliente";

                // Mostrar la actividad más reciente en el label de texto si hay registros
                if (dt.Rows.Count > 0)
                {
                    DataRow ultima = dt.Rows[0];
                    label8.Text = $"Venta #{ultima["idVenta"]} Completada ($ {Convert.ToDecimal(ultima["total_venta"]).ToString("N2")})";
                }
                else
                {
                    label8.Text = "No hay ventas registradas aún.";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar la actividad reciente: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }*/

        public DataTable ObtenerVentasPorVendedor(int idUsuario)
        {
            ClsDataBase database = new ClsDataBase();
            database.NameSP = "sp_ObtenerVentasPorVendedor";
            database.TableName = "TablaVentasVendedor";
            database.DtParameters.Rows.Add("p_idUsuario", null, idUsuario);
            database.EjecutarSelect();
            if (!string.IsNullOrEmpty(database.ErrorDB)) throw new Exception(database.ErrorDB);
            return database.DsResults.Tables["TablaVentasVendedor"];
        }
    }
}