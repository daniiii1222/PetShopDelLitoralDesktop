using Capa_Datos;
using Capa_Entidad;
using MySql.Data.MySqlClient;
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
                throw new Exception("Error en la Capa de Negocio al listar usuarios: " + ex.Message);
            }
        }

        // Aquí más adelante agregaremos los métodos de Insertar, Modificar y Eliminar 
        // aplicando las validaciones de datos (ej: verificar que las contraseñas no estén vacías, 
        // que el DNI no esté duplicado, etc.) antes de enviarlos a la Capa de Datos.



        // (Aquí ya tienes tu ListarUsuarios)

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
    }

}


