using Capa_Entidad;
using CapaDatos;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Capa_Datos
{
    public class CD_Usuario
    {

        public DataSet ListarUsuarios()
        {
            ClsDataBase database = new ClsDataBase();

            // 1. Configuramos el nombre del SP y la tabla resultante
            database.NameSP = "sp_ListarUsuarios";
            database.TableName = "TablaUsuarios";

            // 2. Ejecutamos usando nuestro nuevo método público puente
            database.EjecutarSelect();

            // 3. Verificamos si hubo algún error en la BD
            if (!string.IsNullOrEmpty(database.ErrorDB))
            {
                // Aquí podrías manejar el error si lo deseas
                throw new Exception(database.ErrorDB);
            }

            // 4. Retornamos el DataSet con los datos listos
            return database.DsResults;
        }

        public bool RegistrarUsuario(Persona objPersona, Usuario objUsuario, out string mensajeError)
        {
            mensajeError = string.Empty;
            ClsDataBase database = new ClsDataBase();

            try
            {
                database.NameSP = "sp_RegistrarUsuarioCompleto";

                database.DtParameters.Rows.Add("@p_nombre", null, objPersona.nombre_persona);
                database.DtParameters.Rows.Add("@p_apellido", null, objPersona.apellido_persona);
                database.DtParameters.Rows.Add("@p_dni", null, objPersona.dni_persona);
                database.DtParameters.Rows.Add("@p_correo", null, objPersona.correo_persona);
                database.DtParameters.Rows.Add("@p_telefono", null, objPersona.telefono_persona);
                database.DtParameters.Rows.Add("@p_direccion", null, objPersona.direccion_persona);
                database.DtParameters.Rows.Add("@p_contrasena", null, objUsuario.contrasenia_usuario);
                database.DtParameters.Rows.Add("@p_idRol", null, objUsuario.idRol.idRol);

                database.EjecutarAccion();

                if (!string.IsNullOrEmpty(database.ErrorDB))
                {
                    mensajeError = database.ErrorDB;
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                mensajeError = ex.Message;
                return false;
            }
        }


        public DataSet ValidarUsuario(string dni)
        {
            ClsDataBase database = new ClsDataBase();

            database.NameSP = "sp_LoginUsuario";
            database.TableName = "TablaLogin";

            database.DtParameters.Rows.Add("@p_dni", null, dni);

            database.EjecutarSelect();

            if (!string.IsNullOrEmpty(database.ErrorDB))
            {
                throw new Exception(database.ErrorDB);
            }

            return database.DsResults;
        }


        public bool EliminarUsuario(int idUsuario, out string mensajeError)
        {
            mensajeError = string.Empty;
            ClsDataBase database = new ClsDataBase();

            try
            {
                database.NameSP = "sp_EliminarUsuario";

                // Agregamos el parámetro usando la estructura de tu clase ClsDataBase
                database.DtParameters.Rows.Add("@p_idUsuario", null, idUsuario);

                database.EjecutarAccion();

                if (!string.IsNullOrEmpty(database.ErrorDB))
                {
                    mensajeError = database.ErrorDB;
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                mensajeError = ex.Message;
                return false;
            }
        }

        public bool ModificarUsuario(Persona objPersona, Usuario objUsuario, out string mensajeError)
        {
            mensajeError = string.Empty;
            ClsDataBase database = new ClsDataBase();

            try
            {
                database.NameSP = "sp_ModificarUsuarioCompleto";

                // Pasamos todos los parámetros requeridos por el Stored Procedure sp_ModificarUsuarioCompleto
                database.DtParameters.Rows.Add("@p_idUsuario", null, objUsuario.idUsuario);
                database.DtParameters.Rows.Add("@p_nombre", null, objPersona.nombre_persona);
                database.DtParameters.Rows.Add("@p_apellido", null, objPersona.apellido_persona);
                database.DtParameters.Rows.Add("@p_dni", null, objPersona.dni_persona);
                database.DtParameters.Rows.Add("@p_correo", null, objPersona.correo_persona);
                database.DtParameters.Rows.Add("@p_telefono", null, objPersona.telefono_persona);
                database.DtParameters.Rows.Add("@p_direccion", null, objPersona.direccion_persona);
                database.DtParameters.Rows.Add("@p_idRol", null, objUsuario.idRol.idRol);
                database.DtParameters.Rows.Add("@p_contrasena", null, objUsuario.contrasenia_usuario);
                database.DtParameters.Rows.Add("@p_estado", null, objUsuario.estado_usuario ? 1 : 0);
                database.EjecutarAccion();

                if (!string.IsNullOrEmpty(database.ErrorDB))
                {
                    mensajeError = database.ErrorDB;
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                mensajeError = ex.Message;
                return false;
            }
        }



        public DataTable ObtenerPersonaPorDni(string dni)
        {
            ClsDataBase db = new ClsDataBase();
            DataTable tablaPersona = new DataTable();

            try
            {
                // 1. Indicamos el nombre del Stored Procedure que creamos en MySQL
                db.NameSP = "sp_ObtenerPersonaPorDni";
                db.TableName = "PersonaEncontrada";

                // 2. Agregamos el parámetro que exige tu procedimiento almacenado
                db.DtParameters.Rows.Add("_dni_persona", "Varchar", dni);

                // 3. Ejecutamos la consulta de selección (SELECT)
                db.EjecutarSelect();

                // 4. Verificamos si hubo algún error en la base de datos
                if (!string.IsNullOrEmpty(db.ErrorDB))
                {
                    throw new Exception(db.ErrorDB);
                }

                // 5. Extraemos la tabla del DataSet que nos devolvió ClsDataBase
                if (db.DsResults != null && db.DsResults.Tables.Contains("PersonaEncontrada"))
                {
                    tablaPersona = db.DsResults.Tables["PersonaEncontrada"];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return tablaPersona;
        }



    }


    
}

