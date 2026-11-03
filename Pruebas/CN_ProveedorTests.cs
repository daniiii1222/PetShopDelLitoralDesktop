using Capa_Entidad;
using Capa_Negocio;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Pruebas
{
    // Validaciones de proveedores que corren ANTES de llegar a la base (no necesitan MySQL)
    [TestClass]
    public class CN_ProveedorTests
    {
        private static Persona PersonaValida()
        {
            return new Persona { nombre_persona = "Distribuidora", apellido_persona = "Litoral", dni_persona = "30112233445" };
        }

        [TestMethod]
        public void RegistrarProveedor_SinNombreOApellido_SeRechaza()
        {
            string mensaje;
            Persona p = PersonaValida();
            p.nombre_persona = " ";

            Assert.IsFalse(new CN_Proveedor().RegistrarProveedor(p, out mensaje));
            StringAssert.Contains(mensaje, "obligatorios");
        }

        [TestMethod]
        public void RegistrarProveedor_SinDni_SeRechaza()
        {
            string mensaje;
            Persona p = PersonaValida();
            p.dni_persona = "";

            Assert.IsFalse(new CN_Proveedor().RegistrarProveedor(p, out mensaje));
            StringAssert.Contains(mensaje, "DNI");
        }

        [TestMethod]
        public void RegistrarProveedor_DniConLetras_SeRechaza()
        {
            string mensaje;
            Persona p = PersonaValida();
            p.dni_persona = "ABC123";

            Assert.IsFalse(new CN_Proveedor().RegistrarProveedor(p, out mensaje));
            StringAssert.Contains(mensaje, "DNI");
        }

        [TestMethod]
        public void RegistrarProveedor_CorreoInvalido_SeRechaza()
        {
            string mensaje;
            Persona p = PersonaValida();
            p.correo_persona = "esto-no-es-un-correo";

            Assert.IsFalse(new CN_Proveedor().RegistrarProveedor(p, out mensaje));
            StringAssert.Contains(mensaje, "correo");
        }

        [TestMethod]
        public void ModificarProveedor_ConIdInvalido_SeRechaza()
        {
            string mensaje;

            Assert.IsFalse(new CN_Proveedor().ModificarProveedor(PersonaValida(), new Proveedor { Id_proveedor = 0 }, out mensaje));
            Assert.IsFalse(string.IsNullOrEmpty(mensaje));
        }

        [TestMethod]
        public void EliminarProveedor_ConIdInvalido_SeRechaza()
        {
            string mensaje;

            Assert.IsFalse(new CN_Proveedor().EliminarProveedor(0, out mensaje));
            Assert.IsFalse(string.IsNullOrEmpty(mensaje));
        }
    }
}
