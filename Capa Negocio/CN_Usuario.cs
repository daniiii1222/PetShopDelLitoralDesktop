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
    public class CN_Usuario
    {

        // Instanciamos la clase de datos que acabamos de crear
        private CD_Usuario objUsuarioDatos = new CD_Usuario();

        // Método para listar usuarios (con validaciones de negocio si hicieran falta)
        public DataSet ListarUsuarios()
        {
            try
            {
                // Aquí podrías agregar reglas de negocio antes de ir a la BD si lo requieres
                return objUsuarioDatos.ListarUsuarios();
            }
            catch (Exception ex)
            {
                // Capturamos el error y lo propagamos con un mensaje claro para la interfaz
                throw new Exception("Error en la Capa de Negocio al listar usuarios: " + ex.Message, ex);
            }
        }

        public bool RegistrarUsuario(Persona objPersona, Usuario objUsuario, out string mensaje)
        {
            mensaje = string.Empty;

            if (string.IsNullOrEmpty(objPersona.nombre_persona) || string.IsNullOrEmpty(objPersona.apellido_persona))
            {
                mensaje = "El nombre y apellido son obligatorios.";
                return false;
            }

            if (string.IsNullOrEmpty(objUsuario.contrasenia_usuario))
            {
                mensaje = "La contraseña del usuario no puede estar vacía.";
                return false;
            }

            if (objUsuario.idRol == null || objUsuario.idRol.idRol <= 0)
            {
                mensaje = "Debe seleccionar un rol válido para el usuario.";
                return false;
            }

            return objUsuarioDatos.RegistrarUsuario(objPersona, objUsuario, out mensaje);
        }





        public bool ValidarLogin(string dni, string contrasenia, out Usuario objUsuarioLogueado, out string mensaje)
        {
            mensaje = string.Empty;
            objUsuarioLogueado = null;

            if (string.IsNullOrEmpty(dni) || string.IsNullOrEmpty(contrasenia))
            {
                mensaje = "Debe ingresar el número de documento y la contraseña.";
                return false;
            }

            try
            {
                DataSet ds = objUsuarioDatos.ValidarUsuario(dni);

                if (ds == null || ds.Tables["TablaLogin"].Rows.Count == 0)
                {
                    mensaje = "El documento ingresado no corresponde a ningún usuario.";
                    return false;
                }

                DataRow row = ds.Tables["TablaLogin"].Rows[0];

                string contraseniaBD = row["contrasenia_usuario"].ToString();
                bool estado = Convert.ToBoolean(row["estado_usuario"]);

                if (!estado)
                {
                    mensaje = "El usuario se encuentra inactivo. Contacte al administrador.";
                    return false;
                }

                if (contraseniaBD != contrasenia)
                {
                    mensaje = "La contraseña ingresada es incorrecta.";
                    return false;
                }

                objUsuarioLogueado = new Usuario()
                {
                    idUsuario = Convert.ToInt32(row["idUsuario"]),
                    estado_usuario = estado,
                    idPersona = new Persona()
                    {
                        idPersona = Convert.ToInt32(row["idPersona"]),
                        nombre_persona = row["nombre_persona"].ToString(),
                        apellido_persona = row["apellido_persona"].ToString()
                    },
                    idRol = new Rol()
                    {
                        idRol = Convert.ToInt32(row["idRol"]),
                        descripcion_rol = row["descripcion_rol"].ToString()
                    }
                };

                return true;
            }
            catch (Exception ex)
            {
                mensaje = "Error al validar el usuario: " + ex.Message;
                return false;
            }
        }


        // AUTORIZACIÓN PARA RESTAURAR LA BASE DE DATOS (doble autorización)
     
        public static bool TieneRol(Usuario u, params string[] roles)
        {
            if (u == null || u.idRol == null) return false;
            string rol = (u.idRol.descripcion_rol ?? "").Trim();
            foreach (string r in roles)
                if (rol.Equals(r, StringComparison.OrdinalIgnoreCase)) return true;
            return false;
        }

        public static bool EsAdministrador(Usuario u)
        {
            return TieneRol(u, "Administrador", "Admin");
        }

        public static bool EsSupervisor(Usuario u)
        {
            return TieneRol(u, "Supervisor");
        }

        // Paso 1: el propio administrador que inició sesión confirma su identidad
        public bool AutorizarComoAdministrador(Usuario adminLogueado, string dni, string contrasenia,
                                               out Usuario administrador, out string mensaje)
        {
            administrador = null;

            if (!EsAdministrador(adminLogueado))
            {
                mensaje = "Solo el administrador puede restaurar la base de datos.";
                return false;
            }

            Usuario u;
            if (!ValidarLogin(dni, contrasenia, out u, out mensaje)) return false;

            if (u.idUsuario != adminLogueado.idUsuario)
            {
                mensaje = "Las credenciales no corresponden al administrador que inició sesión.";
                return false;
            }

            administrador = u;
            mensaje = string.Empty;
            return true;
        }

        // Paso 2: un supervisor (otra persona, distinta del administrador) autoriza
        public bool AutorizarComoSupervisor(Usuario adminLogueado, string dni, string contrasenia,
                                            out Usuario supervisor, out string mensaje)
        {
            supervisor = null;

            Usuario u;
            if (!ValidarLogin(dni, contrasenia, out u, out mensaje)) return false;

            if (adminLogueado != null && u.idUsuario == adminLogueado.idUsuario)
            {
                mensaje = "La autorización debe darla otra persona, no el administrador.";
                return false;
            }

            if (!EsSupervisor(u))
            {
                mensaje = "El usuario ingresado no es supervisor.";
                return false;
            }

            supervisor = u;
            mensaje = string.Empty;
            return true;
        }

        public bool ModificarUsuario(Persona objPersona, Usuario objUsuario, out string mensaje)
        {
            mensaje = string.Empty;

            // Mismas validaciones que al registrar
            if (string.IsNullOrEmpty(objPersona.nombre_persona) || string.IsNullOrEmpty(objPersona.apellido_persona))
            {
                mensaje = "El nombre y apellido son obligatorios.";
                return false;
            }

            if (objUsuario.idRol == null || objUsuario.idRol.idRol <= 0)
            {
                mensaje = "Debe seleccionar un rol válido para el usuario.";
                return false;
            }

            // Llamamos a la Capa de Datos para que ejecute el UPDATE
            return objUsuarioDatos.ModificarUsuario(objPersona, objUsuario, out mensaje);
        }

        public bool EliminarUsuario(int idUsuario, out string mensaje)
        {
            mensaje = string.Empty;

            if (idUsuario <= 0)
            {
                mensaje = "ID de usuario no válido.";
                return false;
            }

            // Llamamos a la Capa de Datos para que haga la baja lógica
            return objUsuarioDatos.EliminarUsuario(idUsuario, out mensaje);
        }


        public DataTable ObtenerPersonaPorDni(string dni)
        {
            CD_Usuario objCapaDato = new CD_Usuario();
            return objCapaDato.ObtenerPersonaPorDni(dni);
        }

        public bool ExisteUsuarioConDni(string dni, int idUsuarioActual, out string mensaje)
        {
            mensaje = string.Empty;

            try
            {
                DataSet ds = objUsuarioDatos.ValidarUsuario(dni);

                if (ds != null && ds.Tables["TablaLogin"].Rows.Count > 0)
                {
                    int idUsuarioEncontrado = Convert.ToInt32(ds.Tables["TablaLogin"].Rows[0]["idUsuario"]);

                    // Si es el mismo usuario que estoy editando, no es un duplicado real
                    if (idUsuarioEncontrado != idUsuarioActual)
                    {
                        mensaje = "Ya existe un usuario registrado con ese DNI.";
                        return true; // Hay duplicado
                    }
                }

                return false; // No hay duplicado
            }
            catch (Exception ex)
            {
                mensaje = "Error al validar el DNI: " + ex.Message;
                return false;
            }
        }
    }

}