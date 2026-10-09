using Capa_Datos;
using CapaDatos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Datos; // Asegurate de que este using coincida con el nombre de tu capa
using System;
using System.Data;

namespace Capa_Negocio
    {
        public class CN_Reporte
        {
            // Esto soluciona el error "objReporteDatos no existe"
            private CD_Reporte objReporteDatos = new CD_Reporte();

            // Esto soluciona el error "no contiene una definición para ObtenerReporteVentas"
            public DataTable ObtenerReporteVentas(DateTime fechaInicio, DateTime fechaFin, int idVendedor)
            {
                try
                {
                    return objReporteDatos.ObtenerReporteVentas(fechaInicio, fechaFin, idVendedor);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al generar el reporte de ventas: " + ex.Message);
                }
            }

            public DataTable ObtenerDetalleVenta(int idVenta)
            {
                try
                {
                    return objReporteDatos.ObtenerDetalleVenta(idVenta);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al cargar el detalle: " + ex.Message);
                }
            }
        }
 }
  
