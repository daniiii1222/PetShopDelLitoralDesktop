using Capa_Datos;
using System;
using System.Data;

namespace Capa_Negocio
{
    public class CN_Reporte
    {
        private CD_Reporte objReporteDatos = new CD_Reporte();

        // idVendedor = 0 trae las ventas de todos los vendedores
        public DataTable ObtenerReporteVentas(DateTime fechaInicio, DateTime fechaFin, int idVendedor)
        {
            ValidarRangoFechas(fechaInicio, fechaFin);

            try
            {
                return objReporteDatos.ObtenerReporteVentas(fechaInicio, fechaFin, idVendedor);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al generar el reporte de ventas: " + ex.Message, ex);
            }
        }

        public DataTable ObtenerDetalleVenta(int idVenta)
        {
            if (idVenta <= 0)
                throw new ArgumentException("El número de venta no es válido.");

            try
            {
                return objReporteDatos.ObtenerDetalleVenta(idVenta);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al cargar el detalle: " + ex.Message, ex);
            }
        }

        public DataTable ObtenerProductosMasVendidos(DateTime desde, DateTime hasta, int limite)
        {
            ValidarRangoFechas(desde, hasta);

            if (limite <= 0)
                throw new ArgumentException("La cantidad de productos a mostrar debe ser mayor a cero.");

            try
            {
                return objReporteDatos.ObtenerProductosMasVendidos(desde, hasta, limite);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los productos más vendidos: " + ex.Message, ex);
            }
        }

      
        private static void ValidarRangoFechas(DateTime desde, DateTime hasta)
        {
            if (desde.Date > hasta.Date)
                throw new ArgumentException("La fecha \"desde\" no puede ser posterior a la fecha \"hasta\".");
        }
    }
}