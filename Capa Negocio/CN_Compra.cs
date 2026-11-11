using System;
using System.Collections.Generic;
using Capa_Entidad;
using Capa_Datos;

namespace Capa_Negocio
{
    public class CN_Compra
    {
        private CD_Compra objcd_compra = new CD_Compra();

        public bool Registrar(CompraProveedor objCompra, List<DetalleCompra> listaDetalle, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (objCompra == null)
            {
                Mensaje = "Los datos de la compra son inválidos.";
                return false;
            }

            if (objCompra.Fecha_compra.Date > DateTime.Today)
            {
                Mensaje = "La fecha de compra no puede ser posterior a la fecha actual.";
                return false;
            }

            if (objCompra.IdProveedor == null || objCompra.IdProveedor.Id_proveedor <= 0)
            {
                Mensaje = "Debe seleccionar un proveedor para la compra.";
                return false;
            }

            if (listaDetalle == null || listaDetalle.Count == 0)
            {
                Mensaje = "Debe agregar al menos un producto al detalle de la compra.";
                return false;
            }

            if (objCompra.IdUsuario == null || objCompra.IdUsuario.idUsuario <= 0)
            {
                Mensaje = "No se pudo identificar al usuario que registra la compra.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(objCompra.Tipo_comprobante))
            {
                Mensaje = "Debe indicar el tipo de comprobante.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(objCompra.Numero_comprobante))
            {
                Mensaje = "Debe ingresar el número de comprobante.";
                return false;
            }


            decimal sumaDetalle = 0;

            foreach (DetalleCompra d in listaDetalle)
            {
                if (d == null || d.IdProducto == null ||
                    d.IdProducto.IdProducto <= 0 || d.Cantidad <= 0)
                {
                    Mensaje = "Todos los productos deben ser válidos y tener una cantidad mayor a cero.";
                    return false;
                }

                if (d.Precio_unitario <= 0)
                {
                    Mensaje = "Todos los productos deben tener un precio de compra mayor a cero.";
                    return false;
                }

                if (d.Subtotal_compra < 0)
                {
                    Mensaje = "El subtotal de los productos no puede ser negativo.";
                    return false;
                }

                decimal subtotalCalculado = Math.Round(
                    d.Cantidad * d.Precio_unitario, 2,
                    MidpointRounding.AwayFromZero);

                if (d.Subtotal_compra != subtotalCalculado)
                {
                    Mensaje = "El subtotal de un producto no coincide con su cantidad y precio.";
                    return false;
                }

                sumaDetalle += d.Subtotal_compra;
            }

            if (Math.Abs(sumaDetalle - objCompra.Monto_compra) > 0.01m)
            {
                Mensaje = "El total de la compra no coincide con el detalle.";
                return false;
            }

            return objcd_compra.Registrar(objCompra, listaDetalle, out Mensaje);


         
        }
    }
}
