using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using Capa_Entidad;
using System.Globalization;
using System.Text;
namespace Capa_Datos
{
    public class CD_Compra
    {


        public bool Registrar(CompraProveedor objCompra,List<DetalleCompra> listaDetalle,out string Mensaje)
        {
            Mensaje = string.Empty;

            try
            {
                // Preparar los detalles en formato JSON
                StringBuilder json = new StringBuilder();
                json.Append("[");

                for (int i = 0; i < listaDetalle.Count; i++)
                {
                    DetalleCompra detalle = listaDetalle[i];

                    if (i > 0)
                        json.Append(",");

                    json.Append("{");
                    json.Append("\"idProducto\":")
                        .Append(detalle.IdProducto.IdProducto);

                    json.Append(",\"cantidad\":")
                        .Append(detalle.Cantidad);

                    json.Append(",\"precio_unitario\":")
                        .Append(detalle.Precio_unitario.ToString(
                            CultureInfo.InvariantCulture));

                    json.Append(",\"subtotal_compra\":")
                        .Append(detalle.Subtotal_compra.ToString(
                            CultureInfo.InvariantCulture));

                    json.Append("}");
                }

                json.Append("]");

                // Ejecutar procedimiento almacenado
                using (MySqlConnection conexion = ClsDataBase.CrearConexion())
                {
                    conexion.Open();

                    using (MySqlCommand cmd = new MySqlCommand(
                        "sp_RegistrarCompraCompleta", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue(
                            "@p_monto", objCompra.Monto_compra);

                        cmd.Parameters.AddWithValue(
                            "@p_fecha", objCompra.Fecha_compra.Date);

                        cmd.Parameters.AddWithValue(
                            "@p_idUsuario", objCompra.IdUsuario.idUsuario);

                        cmd.Parameters.AddWithValue(
                            "@p_idProveedor", objCompra.IdProveedor.Id_proveedor);

                        cmd.Parameters.AddWithValue(
                            "@p_tipo", objCompra.Tipo_comprobante);

                        cmd.Parameters.AddWithValue(
                            "@p_numero", objCompra.Numero_comprobante);

                        cmd.Parameters.AddWithValue(
                            "@p_observaciones",
                            objCompra.Observaciones ?? string.Empty);

                        cmd.Parameters.AddWithValue(
                            "@p_detalles", json.ToString());

                        cmd.ExecuteNonQuery();
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                Mensaje = "Error al registrar la compra: " + ex.Message;
                return false;
            }
        }

    }
}
