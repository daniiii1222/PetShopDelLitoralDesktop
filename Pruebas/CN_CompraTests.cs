using System.Collections.Generic;
using Capa_Entidad;
using Capa_Negocio;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Pruebas
{
    [TestClass]
    public class CN_CompraTests
    {
        private static CompraProveedor CompraValida()
        {
            return new CompraProveedor
            {
                IdUsuario = new Usuario { idUsuario = 1 },
                IdProveedor = new Proveedor { Id_proveedor = 1 },
                Tipo_comprobante = "Factura A",
                Numero_comprobante = "0001-00000001",
                Monto_compra = 2000m
            };
        }

        private static List<DetalleCompracs> DetalleValido()
        {
            return new List<DetalleCompracs>
            {
                new DetalleCompracs
                {
                    IdProducto = new Producto { IdProducto = 1 },
                    Cantidad = 2,
                    Precio_unitario = 1000m,
                    Subtotal_compra = 2000m
                }
            };
        }

        [TestMethod]
        public void Registrar_CompraNula_SeRechaza()
        {
            string mensaje;
            bool ok = new CN_Compra().Registrar(null, DetalleValido(), out mensaje);

            Assert.IsFalse(ok);
            Assert.IsFalse(string.IsNullOrEmpty(mensaje));
        }

        [TestMethod]
        public void Registrar_SinUsuario_SeRechaza()
        {
            string mensaje;
            CompraProveedor compra = CompraValida();
            compra.IdUsuario = null;

            bool ok = new CN_Compra().Registrar(compra, DetalleValido(), out mensaje);

            Assert.IsFalse(ok);
            StringAssert.Contains(mensaje, "usuario");
        }

        [TestMethod]
        public void Registrar_SinProveedor_SeRechaza()
        {
            string mensaje;
            CompraProveedor compra = CompraValida();
            compra.IdProveedor = null;

            bool ok = new CN_Compra().Registrar(compra, DetalleValido(), out mensaje);

            Assert.IsFalse(ok);
            StringAssert.Contains(mensaje, "proveedor");
        }

        [TestMethod]
        public void Registrar_SinComprobante_SeRechaza()
        {
            string mensaje;
            CompraProveedor compra = CompraValida();
            compra.Numero_comprobante = "   ";

            bool ok = new CN_Compra().Registrar(compra, DetalleValido(), out mensaje);

            Assert.IsFalse(ok);
            StringAssert.Contains(mensaje, "comprobante");
        }

        [TestMethod]
        public void Registrar_DetalleVacio_SeRechaza()
        {
            string mensaje;
            bool ok = new CN_Compra().Registrar(CompraValida(), new List<DetalleCompracs>(), out mensaje);

            Assert.IsFalse(ok);
            StringAssert.Contains(mensaje, "producto");
        }

        [TestMethod]
        public void Registrar_CantidadCero_SeRechaza()
        {
            string mensaje;
            List<DetalleCompracs> detalle = DetalleValido();
            detalle[0].Cantidad = 0;

            bool ok = new CN_Compra().Registrar(CompraValida(), detalle, out mensaje);

            Assert.IsFalse(ok);
            StringAssert.Contains(mensaje, "cantidad");
        }

        [TestMethod]
        public void Registrar_PrecioCero_SeRechaza()
        {
            string mensaje;
            List<DetalleCompracs> detalle = DetalleValido();
            detalle[0].Precio_unitario = 0m;

            bool ok = new CN_Compra().Registrar(CompraValida(), detalle, out mensaje);

            Assert.IsFalse(ok);
            StringAssert.Contains(mensaje, "precio");
        }

        [TestMethod]
        public void Registrar_TotalNoCoincide_SeRechaza()
        {
            string mensaje;
            CompraProveedor compra = CompraValida();
            compra.Monto_compra = 9999m;

            bool ok = new CN_Compra().Registrar(compra, DetalleValido(), out mensaje);

            Assert.IsFalse(ok);
            StringAssert.Contains(mensaje, "total");
        }
    }
}
