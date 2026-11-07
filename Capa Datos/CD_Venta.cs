using Capa_Entidad;
using Capa_Datos;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Text;

namespace Capa_Datos
{
    public class CD_Venta
    {
        public DataSet ListarMetodosPago()
        {
            ClsDataBase database = new ClsDataBase();
            database.NameSP = "sp_ListarMetodosPago";
            database.TableName = "TablaMetodosPago";
            database.EjecutarSelect();
            if (!string.IsNullOrEmpty(database.ErrorDB)) throw new Exception(database.ErrorDB, new Exception(database.ErrorDB));
            return database.DsResults;
        }

        public DataTable BuscarProductos(string texto)
        {
            ClsDataBase database = new ClsDataBase();
            database.NameSP = "sp_BuscarProductosVenta";
            database.TableName = "TablaProductosVenta";
            database.DtParameters.Rows.Add("p_texto", null, texto);
            database.EjecutarSelect();
            if (!string.IsNullOrEmpty(database.ErrorDB)) throw new Exception(database.ErrorDB, new Exception(database.ErrorDB));
            return database.DsResults.Tables["TablaProductosVenta"];
        }

        public DataTable ObtenerClientePorDni(string dni)
        {
            ClsDataBase database = new ClsDataBase();
            database.NameSP = "sp_ObtenerClientePorDni";
            database.TableName = "TablaClienteDni";
            database.DtParameters.Rows.Add("p_dni", null, dni);
            database.EjecutarSelect();
            if (!string.IsNullOrEmpty(database.ErrorDB)) throw new Exception(database.ErrorDB, new Exception(database.ErrorDB));
            return database.DsResults.Tables["TablaClienteDni"];
        }

        // Registra la venta COMPLETA en UNA transacción que vive en la base de datos:
        // el procedimiento sp_RegistrarVentaCompleta (Scripts/Ventas.sql) valida el stock, toma el precio real
        // de cada producto, calcula descuentos y total, guarda cabecera + detalle, descuenta el stock y hace
        // COMMIT. Si algo falla hace ROLLBACK, y el error vuelve acá como excepción.
        public int RegistrarVentaTransaccional(Venta objVenta, List<DetalleVenta> detalles)
        {
            // El detalle viaja como JSON: [{"idProducto":1,"cantidad":2,"descuento":10},...]
            // (con punto decimal, sin depender de la configuración regional de la PC)
            StringBuilder json = new StringBuilder("[");
            for (int i = 0; i < detalles.Count; i++)
            {
                DetalleVenta d = detalles[i];
                if (i > 0) json.Append(",");
                json.Append("{\"idProducto\":").Append(d.IdProducto.IdProducto.ToString(CultureInfo.InvariantCulture))
                    .Append(",\"cantidad\":").Append(d.Cantidad.ToString(CultureInfo.InvariantCulture))
                    .Append(",\"descuento\":").Append(d.Descuento_detalle.ToString(CultureInfo.InvariantCulture))
                    .Append("}");
            }
            json.Append("]");

            using (MySqlConnection conexion = ClsDataBase.CrearConexion())
            {
                conexion.Open();

                using (MySqlCommand cmd = new MySqlCommand("sp_RegistrarVentaCompleta", conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_idUsuario", objVenta.IdUsuario.idUsuario);
                    cmd.Parameters.AddWithValue("p_idCliente", objVenta.IdCliente.IdCliente);
                    cmd.Parameters.AddWithValue("p_idMetodoPago", objVenta.IdMetodoPago.IdMetodoPago);
                    cmd.Parameters.AddWithValue("p_fecha", objVenta.Fecha_venta.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("p_descuento", objVenta.Descuento_venta);
                    cmd.Parameters.AddWithValue("p_detalle", json.ToString());

                    // El procedimiento termina con SELECT v_idVenta AS idVenta
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        // Últimas ventas para la "Actividad reciente" del dashboard
        public DataTable ObtenerVentasRecientes()
        {
            ClsDataBase database = new ClsDataBase();
            database.NameSP = "sp_ObtenerVentasRecientes";
            database.TableName = "TablaVentasRecientes";
            database.EjecutarSelect();
            if (!string.IsNullOrEmpty(database.ErrorDB)) throw new Exception(database.ErrorDB, new Exception(database.ErrorDB));
            return database.DsResults.Tables["TablaVentasRecientes"];
        }

        // Una fila con: ventas_hoy, total_hoy, clientes, stock_bajo
        public DataTable ObtenerResumenDashboard()
        {
            ClsDataBase database = new ClsDataBase();
            database.NameSP = "sp_ObtenerResumenDashboard";
            database.TableName = "TablaResumenDashboard";
            database.EjecutarSelect();
            if (!string.IsNullOrEmpty(database.ErrorDB)) throw new Exception(database.ErrorDB, new Exception(database.ErrorDB));
            return database.DsResults.Tables["TablaResumenDashboard"];
        }

        public DataTable ObtenerVentasPorVendedor(int idUsuario)
        {
            ClsDataBase database = new ClsDataBase();
            database.NameSP = "sp_ObtenerVentasPorVendedor";
            database.TableName = "TablaVentasVendedor";
            database.DtParameters.Rows.Add("p_idUsuario", null, idUsuario);
            database.EjecutarSelect();
            if (!string.IsNullOrEmpty(database.ErrorDB)) throw new Exception(database.ErrorDB, new Exception(database.ErrorDB));
            return database.DsResults.Tables["TablaVentasVendedor"];
        }
    }
}