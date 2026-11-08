using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using Capa_Entidad;

namespace Capa_Datos
{
    public class CD_Compra
    {

        public bool Registrar(CompraProveedor objCompra, List<DetalleCompracs> listaDetalle, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = string.Empty;

            using (MySqlConnection oconexion = ClsDataBase.CrearConexion())
            {
                try
                {
                    oconexion.Open();

                    using (MySqlTransaction transaccion = oconexion.BeginTransaction())
                    {
                        try
                        {
                            // 1. Insertar cabecera de compra
                            string queryCompra = @"INSERT INTO compra_proveedor(
                                                    monto_compra, estado_compra, fecha_compra, 
                                                    idUsuario, idProveedor, tipo_comprobante, 
                                                    numero_comprobante, observaciones, fechaCreacion_compra)
                                                   VALUES(
                                                    @monto, 1, @fecha, 
                                                    @idUsuario, @idProveedor, @tipo, 
                                                    @numero, @observaciones, NOW());
                                                   SELECT LAST_INSERT_ID();";

                            MySqlCommand cmdCompra = new MySqlCommand(queryCompra, oconexion, transaccion);
                            cmdCompra.Parameters.AddWithValue("@monto", objCompra.Monto_compra);
                            cmdCompra.Parameters.AddWithValue("@fecha", objCompra.Fecha_compra.ToString("yyyy-MM-dd"));
                            cmdCompra.Parameters.AddWithValue("@idUsuario", objCompra.IdUsuario.idUsuario);
                            cmdCompra.Parameters.AddWithValue("@idProveedor", objCompra.IdProveedor.Id_proveedor);
                            cmdCompra.Parameters.AddWithValue("@tipo", objCompra.Tipo_comprobante);
                            cmdCompra.Parameters.AddWithValue("@numero", objCompra.Numero_comprobante);
                            cmdCompra.Parameters.AddWithValue("@observaciones", objCompra.Observaciones ?? string.Empty);

                            int idCompraGenerada = Convert.ToInt32(cmdCompra.ExecuteScalar());

                            // 2. Insertar cada detalle de compra y actualizar el stock del producto
                            foreach (DetalleCompracs detalle in listaDetalle)
                            {
                                string queryDetalle = @"INSERT INTO detalle_compra(
                                                            cantidad, precio_unitario, subtotal_compra, 
                                                            idProducto, idCompra, fechaCreacion_detalleC)
                                                        VALUES(
                                                            @cantidad, @precio, @subtotal, 
                                                            @idProducto, @idCompra, NOW());";

                                MySqlCommand cmdDetalle = new MySqlCommand(queryDetalle, oconexion, transaccion);
                                cmdDetalle.Parameters.AddWithValue("@cantidad", detalle.Cantidad);
                                cmdDetalle.Parameters.AddWithValue("@precio", detalle.Precio_unitario);
                                cmdDetalle.Parameters.AddWithValue("@subtotal", detalle.Subtotal_compra);
                                cmdDetalle.Parameters.AddWithValue("@idProducto", detalle.IdProducto.IdProducto);
                                cmdDetalle.Parameters.AddWithValue("@idCompra", idCompraGenerada);
                                cmdDetalle.ExecuteNonQuery();

                                // Actualizar stock y precio de costo en producto
                                string queryStock = @"UPDATE producto SET 
                                                        stock_producto = stock_producto + @cantidadComprada,
                                                        precioUnitario_producto = @nuevoCosto
                                                      WHERE idProducto = @idProd;";

                                MySqlCommand cmdStock = new MySqlCommand(queryStock, oconexion, transaccion);
                                cmdStock.Parameters.AddWithValue("@cantidadComprada", detalle.Cantidad);
                                cmdStock.Parameters.AddWithValue("@nuevoCosto", detalle.Precio_unitario);
                                cmdStock.Parameters.AddWithValue("@idProd", detalle.IdProducto.IdProducto);
                                cmdStock.ExecuteNonQuery();
                            }

                            // Confirmar la transacción
                            transaccion.Commit();
                            respuesta = true;
                        }
                        catch (Exception exTrans)
                        {
                            transaccion.Rollback();
                            respuesta = false;
                            Mensaje = "Error en la transacción: " + exTrans.Message;
                        }
                    }
                }
                catch (Exception ex)
                {
                    respuesta = false;
                    Mensaje = "Error de conexión: " + ex.Message;
                }
            }

            return respuesta;
        }
    }
}
