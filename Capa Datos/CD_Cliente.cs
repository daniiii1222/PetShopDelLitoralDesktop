using Capa_Entidad;
using CapaDatos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Datos
{
    public class CD_Cliente
    {
        public DataSet ListarClientes()
        {
            ClsDataBase database = new ClsDataBase();

            database.NameSP = "sp_ListarClientes";
            database.TableName = "TablaClientes";

            database.EjecutarSelect();

            if (!string.IsNullOrEmpty(database.ErrorDB))
            {
                throw new Exception(database.ErrorDB);
            }

            return database.DsResults;
        }

        // No recibe un objeto Cliente porque, a diferencia de Usuario,
        // Cliente no tiene datos propios para completar (sin contraseña, sin rol):
        // todo lo que hace falta ya está en Persona.
        public bool RegistrarCliente(Persona objPersona, out string mensajeError)
        {
            mensajeError = string.Empty;
            ClsDataBase database = new ClsDataBase();

            try
            {
                database.NameSP = "sp_RegistrarClienteCompleto";

                database.DtParameters.Rows.Add("@p_nombre", null, objPersona.nombre_persona);
                database.DtParameters.Rows.Add("@p_apellido", null, objPersona.apellido_persona);
                database.DtParameters.Rows.Add("@p_dni", null, objPersona.dni_persona);
                database.DtParameters.Rows.Add("@p_correo", null, objPersona.correo_persona);
                database.DtParameters.Rows.Add("@p_telefono", null, objPersona.telefono_persona);
                database.DtParameters.Rows.Add("@p_direccion", null, objPersona.direccion_persona);

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

        public bool ModificarCliente(Persona objPersona, Cliente objCliente, out string mensajeError)
        {
            mensajeError = string.Empty;
            ClsDataBase database = new ClsDataBase();

            try
            {
                database.NameSP = "sp_ModificarClienteCompleto";

                database.DtParameters.Rows.Add("@p_idCliente", null, objCliente.IdCliente);
                database.DtParameters.Rows.Add("@p_nombre", null, objPersona.nombre_persona);
                database.DtParameters.Rows.Add("@p_apellido", null, objPersona.apellido_persona);
                database.DtParameters.Rows.Add("@p_dni", null, objPersona.dni_persona);
                database.DtParameters.Rows.Add("@p_correo", null, objPersona.correo_persona);
                database.DtParameters.Rows.Add("@p_telefono", null, objPersona.telefono_persona);
                database.DtParameters.Rows.Add("@p_direccion", null, objPersona.direccion_persona);
                database.DtParameters.Rows.Add("@p_estado", null, objCliente.Estado_cliente ? 1 : 0);

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

        public bool EliminarCliente(int idCliente, out string mensajeError)
        {
            mensajeError = string.Empty;
            ClsDataBase database = new ClsDataBase();

            try
            {
                database.NameSP = "sp_EliminarCliente";
                database.DtParameters.Rows.Add("@p_idCliente", null, idCliente);

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

        // Reusa el mismo SP que ya usa Usuario (sp_ObtenerPersonaPorDni) porque
        // es una consulta a nivel Persona, no algo específico de Cliente.
        public DataTable ObtenerPersonaPorDni(string dni)
        {
            ClsDataBase db = new ClsDataBase();
            DataTable tablaPersona = new DataTable();

            try
            {
                db.NameSP = "sp_ObtenerPersonaPorDni";
                db.TableName = "PersonaEncontrada";

                db.DtParameters.Rows.Add("_dni_persona", "Varchar", dni);

                db.EjecutarSelect();

                if (!string.IsNullOrEmpty(db.ErrorDB))
                {
                    throw new Exception(db.ErrorDB);
                }

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
