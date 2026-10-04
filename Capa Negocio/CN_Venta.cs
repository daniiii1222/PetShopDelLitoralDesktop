using Capa_Datos;
using Capa_Entidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Negocio
{
    public class CN_Venta
    {

        private CD_Venta objVentaDatos = new CD_Venta();

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

            return objVentaDatos.RegistrarVenta(objVenta, detalles, out idVenta, out mensaje);
        }
    }
}