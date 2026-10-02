using Capa_Entidad;
using CapaDatos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            if (!string.IsNullOrEmpty(database.ErrorDB))
            {
                throw new Exception(database.ErrorDB);
            }

            return database.DsResults;
        }

        public DataTable BuscarProductos(string texto)
        {
            ClsDataBase database = new ClsDataBase();

            database.NameSP = "sp_BuscarProductosVenta";
            database.TableName = "TablaProductosVenta";
            database.DtParameters.Rows.Add("p_texto", null, texto);

            database.EjecutarSelect();

            if (!string.IsNullOrEmpty(database.ErrorDB))
            {
                throw new Exception(database.ErrorDB);
            }

            return database.DsResults.Tables["TablaProductosVenta"];
        }

        public DataTable ObtenerClientePorDni(string dni)
        {
            ClsDataBase database = new ClsDataBase();

            database.NameSP = "sp_ObtenerClientePorDni";
            database.TableName = "TablaClienteDni";
            database.DtParameters.Rows.Add("p_dni", null, dni);

            database.EjecutarSelect();

            if (!string.IsNullOrEmpty(database.ErrorDB))
            {
                throw new Exception(database.ErrorDB);
            }

            return database.DsResults.Tables["TablaClienteDni"];
        }

        // Manda la venta completa (cabecera + detalles) en UNA sola llamada,
        // para que el SP la guarde dentro de una transacción.
        public bool RegistrarVenta(Venta objVenta, List<DetalleVenta> detalles, out int idVenta, out string mensajeError)
        {
            idVenta = 0;
            mensajeError = string.Empty;
            ClsDataBase database = new ClsDataBase();

            try
            {
                database.NameSP = "sp_RegistrarVentaCompleta";
                database.Scalar = true; // el SP termina con un SELECT idVenta

                database.DtParameters.Rows.Add("p_idUsuario", null, objVenta.IdUsuario.idUsuario);
                database.DtParameters.Rows.Add("p_idCliente", null, objVenta.IdCliente.IdCliente);
                database.DtParameters.Rows.Add("p_idMetodoPago", null, objVenta.IdMetodoPago.IdMetodoPago);
                // ClsDataBase guarda los valores como texto, por eso la fecha va en formato MySQL
                database.DtParameters.Rows.Add("p_fecha", null, objVenta.Fecha_venta.ToString("yyyy-MM-dd"));
                // ClsDataBase manda todo como texto: el decimal va con punto (cultura invariante), si no "10,5" se rompe
                database.DtParameters.Rows.Add("p_descuento", null, objVenta.Descuento_venta.ToString(CultureInfo.InvariantCulture));
                database.DtParameters.Rows.Add("p_detalle", null, ConstruirJsonDetalle(detalles));

                database.EjecutarAccion();

                if (!string.IsNullOrEmpty(database.ErrorDB))
                {
                    mensajeError = database.ErrorDB;
                    return false;
                }

                int.TryParse(database.ScalarValue, out idVenta);
                return true;
            }
            catch (Exception ex)
            {
                mensajeError = ex.Message;
                return false;
            }
        }

        // Arma [{"idProducto":1,"cantidad":2,"descuento":10},...]. Solo lleva números, así que no hay nada que escapar.
        private string ConstruirJsonDetalle(List<DetalleVenta> detalles)
        {
            StringBuilder sb = new StringBuilder("[");
            for (int i = 0; i < detalles.Count; i++)
            {
                if (i > 0) sb.Append(",");
                sb.Append("{\"idProducto\":").Append(detalles[i].IdProducto.IdProducto)
                  .Append(",\"cantidad\":").Append(detalles[i].Cantidad)
                  .Append(",\"descuento\":").Append(detalles[i].Descuento_detalle.ToString(CultureInfo.InvariantCulture)).Append("}");
            }
            sb.Append("]");
            return sb.ToString();
        }
    }
}