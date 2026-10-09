using CapaDatos;
using System;
using System.Data;


// Ajustá esto si tu clase ClsDataBase está en otro namespace

namespace Capa_Datos
{
    public class CD_Reporte
    {
        // Método para el historial de ventas filtrado
        public DataTable ObtenerReporteVentas(DateTime fechaInicio, DateTime fechaFin, int idVendedor)
        {
            ClsDataBase database = new ClsDataBase();

            // Usamos el nombre exacto que pusiste en DBeaver
            database.NameSP = "sp_Reporte_Ventas";
            database.TableName = "TablaReporteVentas";

            database.DtParameters.Rows.Add("p_fechaInicio", null, fechaInicio.ToString("yyyy-MM-dd"));
            database.DtParameters.Rows.Add("p_fechaFin", null, fechaFin.ToString("yyyy-MM-dd"));
            database.DtParameters.Rows.Add("p_idVendedor", null, idVendedor);

            database.EjecutarSelect();

            if (!string.IsNullOrEmpty(database.ErrorDB))
                throw new Exception(database.ErrorDB);

            return database.DsResults.Tables["TablaReporteVentas"];
        }

        // Método para ver los productos dentro de un ticket específico
        public DataTable ObtenerDetalleVenta(int idVenta)
        {
            ClsDataBase database = new ClsDataBase();

            database.NameSP = "sp_ObtenerDetalleVenta";
            database.TableName = "TablaDetalle";

            database.DtParameters.Rows.Add("p_idVenta", null, idVenta);

            database.EjecutarSelect();

            if (!string.IsNullOrEmpty(database.ErrorDB))
                throw new Exception(database.ErrorDB);

            return database.DsResults.Tables["TablaDetalle"];
        }
    }
}