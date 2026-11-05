using Capa_Entidad;
using Capa_Negocio;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace Pruebas
{
    [TestClass]
    public class CN_ProductoTests
    {
        [TestMethod]
        public void Registrar_SinCodigo_SeRechaza()
        {
            string mensaje;
            Producto p = new Producto { Codigo_producto = "  ", Nombre_producto = "Alimento" };

            int id = new CN_Producto().Registrar(p, out mensaje);

            Assert.AreEqual(0, id);
            StringAssert.Contains(mensaje, "digo");
        }

        [TestMethod]
        public void Registrar_SinNombre_SeRechaza()
        {
            string mensaje;
            Producto p = new Producto { Codigo_producto = "PROD2090", Nombre_producto = "" };

            int id = new CN_Producto().Registrar(p, out mensaje);

            Assert.AreEqual(0, id);
            StringAssert.Contains(mensaje, "nombre");
        }

        [TestMethod]
        public void Registrar_PrecioNegativo_SeRechaza()
        {
            string mensaje;
            Producto p = new Producto { Codigo_producto = "PROD2090", Nombre_producto = "Alimento", Precio_producto = -10 };

            int id = new CN_Producto().Registrar(p, out mensaje);

            Assert.AreEqual(0, id);
            StringAssert.Contains(mensaje, "precio de venta");
        }

        [TestMethod]
        public void Registrar_CostoNegativo_SeRechaza()
        {
            string mensaje;
            Producto p = new Producto { Codigo_producto = "PROD2090", Nombre_producto = "Alimento", Precio_producto = 100, PrecioUnitario_producto = -5 };

            int id = new CN_Producto().Registrar(p, out mensaje);

            Assert.AreEqual(0, id);
            StringAssert.Contains(mensaje, "precio de costo");
        }

        [TestMethod]
        public void Registrar_StockNegativo_SeRechaza()
        {
            string mensaje;
            Producto p = new Producto { Codigo_producto = "PROD2090", Nombre_producto = "Alimento", Precio_producto = 100, Stock_producto = -1 };

            int id = new CN_Producto().Registrar(p, out mensaje);

            Assert.AreEqual(0, id);
            StringAssert.Contains(mensaje, "stock");
        }

        [TestMethod]
        public void Editar_SinCodigo_SeRechaza()
        {
            string mensaje;
            Producto p = new Producto { IdProducto = 5, Codigo_producto = "", Nombre_producto = "Alimento" };

            Assert.IsFalse(new CN_Producto().Editar(p, out mensaje));
            Assert.IsFalse(string.IsNullOrEmpty(mensaje));
        }

        [TestMethod]
        public void Editar_IdInvalido_SeRechaza()
        {
            string mensaje;
            Producto p = new Producto { IdProducto = 0, Codigo_producto = "PROD1", Nombre_producto = "Alimento" };

            Assert.IsFalse(new CN_Producto().Editar(p, out mensaje));
            StringAssert.Contains(mensaje, "no válido");
        }

        [TestMethod]
        public void Editar_PrecioExcesivo_SeRechaza()
        {
            string mensaje;
            Producto p = new Producto { IdProducto = 1, Codigo_producto = "PROD1", Nombre_producto = "Alimento", Precio_producto = 100000000m };

            Assert.IsFalse(new CN_Producto().Editar(p, out mensaje));
            StringAssert.Contains(mensaje, "máximo permitido");
        }

        [TestMethod]
        public void Eliminar_ConIdInvalido_SeRechaza()
        {
            string mensaje;

            Assert.IsFalse(new CN_Producto().Eliminar(0, out mensaje));
            Assert.IsFalse(string.IsNullOrEmpty(mensaje));
        }
    }

    [TestClass]
    public class CN_BackupTests
    {
        private string archivoTemporal;

        [TestInitialize]
        public void Preparar()
        {
            archivoTemporal = Path.Combine(Path.GetTempPath(), "pruebas_backup_" + Guid.NewGuid().ToString("N") + ".sql");
        }

        [TestCleanup]
        public void Limpiar()
        {
            if (File.Exists(archivoTemporal)) File.Delete(archivoTemporal);
        }

        [TestMethod]
        public void LeerRespaldo_ArchivoInexistente_DevuelveFalso()
        {
            string sql, mensaje;

            bool ok = new CN_Backup().LeerRespaldo(archivoTemporal, out sql, out mensaje);

            Assert.IsFalse(ok);
            Assert.IsNull(sql);
            Assert.IsFalse(string.IsNullOrEmpty(mensaje));
        }

        [TestMethod]
        public void LeerRespaldo_ArchivoQueNoEsUnaCopia_DevuelveFalso()
        {
            File.WriteAllText(archivoTemporal, "SELECT 1;");
            string sql, mensaje;

            bool ok = new CN_Backup().LeerRespaldo(archivoTemporal, out sql, out mensaje);

            Assert.IsFalse(ok);
            Assert.IsNull(sql);
        }

        [TestMethod]
        public void LeerRespaldo_CopiaValida_DevuelveElScript()
        {
            string contenido = "-- Backup 01/01/2026" + Environment.NewLine + "CREATE TABLE `x` (id INT);";
            File.WriteAllText(archivoTemporal, contenido);
            string sql, mensaje;

            bool ok = new CN_Backup().LeerRespaldo(archivoTemporal, out sql, out mensaje);

            Assert.IsTrue(ok, mensaje);
            Assert.AreEqual(contenido, sql);
        }

        [TestMethod]
        public void RestaurarRespaldo_ScriptVacio_DevuelveFalsoSinTocarLaBase()
        {
            string mensaje;

            Assert.IsFalse(new CN_Backup().RestaurarRespaldo("   ", out mensaje));
            Assert.IsFalse(string.IsNullOrEmpty(mensaje));
        }
    }
}
