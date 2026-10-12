using System;
using System.Collections.Generic;
using Capa_Entidad;
using CapaDatos;

namespace CapaNegocio
{
    public class CN_Compra
    {
        private CD_Compra objcd_compra = new CD_Compra();

        public bool Registrar(CompraProveedor objCompra, List<DetalleCompracs> listaDetalle, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (objCompra == null)
            {
                Mensaje = "Los datos de la compra son inválidos.";
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

            return objcd_compra.Registrar(objCompra, listaDetalle, out Mensaje);
        }
    }
}
