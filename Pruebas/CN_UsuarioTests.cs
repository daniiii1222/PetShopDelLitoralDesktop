using Capa_Entidad;
using Capa_Negocio;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Pruebas
{
    // Pruebas de las reglas de negocio que NO necesitan base de datos:
    // las validaciones corren antes de llegar a la capa de datos.
    [TestClass]
    public class CN_UsuarioTests
    {
        private static Usuario UsuarioConRol(string rol)
        {
            return new Usuario { idUsuario = 1, idRol = new Rol { idRol = 1, descripcion_rol = rol } };
        }

        [TestMethod]
        public void EsAdministrador_ConRolAdministrador_EsVerdadero()
        {
            Assert.IsTrue(CN_Usuario.EsAdministrador(UsuarioConRol("Administrador")));
            Assert.IsTrue(CN_Usuario.EsAdministrador(UsuarioConRol("  administrador ")));
        }

        [TestMethod]
        public void EsAdministrador_ConOtroRolOUsuarioNulo_EsFalso()
        {
            Assert.IsFalse(CN_Usuario.EsAdministrador(UsuarioConRol("Vendedor")));
            Assert.IsFalse(CN_Usuario.EsAdministrador(null));
            Assert.IsFalse(CN_Usuario.EsAdministrador(new Usuario()));
        }

        [TestMethod]
        public void EsSupervisor_SoloParaRolSupervisor()
        {
            Assert.IsTrue(CN_Usuario.EsSupervisor(UsuarioConRol("Supervisor")));
            Assert.IsFalse(CN_Usuario.EsSupervisor(UsuarioConRol("Administrador")));
        }

        [TestMethod]
        public void ValidarLogin_SinDniOSinContrasenia_NoEntra()
        {
            Usuario logueado;
            string mensaje;

            bool ok = new CN_Usuario().ValidarLogin("", "1234", out logueado, out mensaje);

            Assert.IsFalse(ok);
            Assert.IsNull(logueado);
            Assert.IsFalse(string.IsNullOrEmpty(mensaje));
        }

        [TestMethod]
        public void RegistrarUsuario_SinNombre_SeRechaza()
        {
            string mensaje;
            Persona persona = new Persona { nombre_persona = "", apellido_persona = "Perez" };
            Usuario usuario = new Usuario { contrasenia_usuario = "abc", idRol = new Rol { idRol = 2 } };

            Assert.IsFalse(new CN_Usuario().RegistrarUsuario(persona, usuario, out mensaje));
            StringAssert.Contains(mensaje, "obligatorios");
        }

        [TestMethod]
        public void RegistrarUsuario_SinContrasenia_SeRechaza()
        {
            string mensaje;
            Persona persona = new Persona { nombre_persona = "Ana", apellido_persona = "Perez" };
            Usuario usuario = new Usuario { contrasenia_usuario = "", idRol = new Rol { idRol = 2 } };

            Assert.IsFalse(new CN_Usuario().RegistrarUsuario(persona, usuario, out mensaje));
            StringAssert.Contains(mensaje, "contrase");
        }

        [TestMethod]
        public void RegistrarUsuario_SinRolValido_SeRechaza()
        {
            string mensaje;
            Persona persona = new Persona { nombre_persona = "Ana", apellido_persona = "Perez" };
            Usuario usuario = new Usuario { contrasenia_usuario = "abc", idRol = new Rol { idRol = 0 } };

            Assert.IsFalse(new CN_Usuario().RegistrarUsuario(persona, usuario, out mensaje));
            StringAssert.Contains(mensaje, "rol");
        }

        [TestMethod]
        public void EliminarUsuario_ConIdInvalido_SeRechaza()
        {
            string mensaje;

            Assert.IsFalse(new CN_Usuario().EliminarUsuario(0, out mensaje));
            Assert.IsFalse(string.IsNullOrEmpty(mensaje));
        }
    }
}
