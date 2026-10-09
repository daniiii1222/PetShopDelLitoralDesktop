using Capa_Datos;
using Capa_Entidad;
using System;
using System.Collections.Generic;
using System.Data;

namespace Capa_Negocio
{
    public class CN_Venta
    {
        private CD_Venta objVentaDatos = new CD_Venta();
        private CD_Reporte objReporteDatos = new CD_Reporte();
        public DataSet ListarMetodosPago()
        {
            try { return objVentaDatos.ListarMetodosPago(); }
            catch (Exception ex) { throw new Exception("Error al listar métodos de pago: " + ex.Message); }
        }

        public DataTable BuscarProductos(string texto)
        {
            try { return objVentaDatos.BuscarProductos((texto ?? "").Trim()); }
            catch (Exception ex) { throw new Exception("Error al buscar productos: " + ex.Message); }
        }

        public DataTable ObtenerClientePorDni(string dni)
        {
            if (string.IsNullOrWhiteSpace(dni)) return new DataTable();
            try { return objVentaDatos.ObtenerClientePorDni(dni.Trim()); }
            catch (Exception ex) { throw new Exception("Error al buscar el cliente: " + ex.Message); }
        }

        public bool RegistrarVenta(Venta objVenta, List<DetalleVenta> detalles, out int idVenta, out string mensaje)
        {
            idVenta = 0;
            mensaje = string.Empty;

            // Validaciones básicas antes de ir a la base
            if (objVenta == null || objVenta.IdUsuario == null || objVenta.IdUsuario.idUsuario <= 0)
            {
                mensaje = "No hay un vendedor con sesión iniciada.";
                return false;
            }

            if (objVenta.IdCliente == null || objVenta.IdCliente.IdCliente <= 0)
            {
                mensaje = "Seleccioná un cliente.";
                return false;
            }

            if (objVenta.IdMetodoPago == null || objVenta.IdMetodoPago.IdMetodoPago <= 0)
            {
                mensaje = "Seleccioná un método de pago.";
                return false;
            }

            if (objVenta.Descuento_venta < 0 || objVenta.Descuento_venta > 100)
            {
                mensaje = "El descuento de la compra debe estar entre 0 y 100%.";
                return false;
            }

            if (detalles == null || detalles.Count == 0)
            {
                mensaje = "Agregá al menos un producto a la venta.";
                return false;
            }

            foreach (DetalleVenta d in detalles)
            {
                if (d.Cantidad <= 0)
                {
                    mensaje = "Todas las cantidades deben ser mayores a cero.";
                    return false;
                }

                if (d.Descuento_detalle < 0 || d.Descuento_detalle > 100)
                {
                    mensaje = "El descuento de cada producto debe estar entre 0 y 100%.";
                    return false;
                }
            }

            // La validación de stock/precio, el cálculo de descuentos y el guardado transaccional
            // quedan todos dentro de CD_Venta.RegistrarVentaTransaccional
            try
            {
                idVenta = objVentaDatos.RegistrarVentaTransaccional(objVenta, detalles);
                return true;
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
                return false;
            }
        }
       /* public DataTable ObtenerVentasRecientes()
        {
            try
            {
                return objVentaDatos.ObtenerVentasRecientes();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener ventas recientes: " + ex.Message);
            }
        }
        */
        public DataTable ObtenerVentasPorVendedor(int idUsuario)
        {
            try
            {
                return objVentaDatos.ObtenerVentasPorVendedor(idUsuario);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener las ventas del vendedor: " + ex.Message);
            }
        }

        public DataTable ObtenerReporteVentas(DateTime fechaInicio, DateTime fechaFin, int idVendedor)
        {
            try
            {
                // Acá cambiamos objVentaDatos por objReporteDatos
                return objReporteDatos.ObtenerReporteVentas(fechaInicio, fechaFin, idVendedor);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al generar el reporte de ventas: " + ex.Message);
            }
        }
    }
}