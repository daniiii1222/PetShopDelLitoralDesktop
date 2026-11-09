using System;
using System.Collections.Generic;
using System.Data;
using Capa_Entidad;

namespace Capa_Datos
{
    public class CD_Proveedor
    {

        // Crea la persona (o reutiliza la que ya tenga ese DNI) y el proveedor, todo en una transacción
        public bool RegistrarProveedor(Persona objPersona, out string mensajeError)
        {
            mensajeError = string.Empty;
            ClsDataBase database = new ClsDataBase();

            try
            {
                database.NameSP = "sp_RegistrarProveedorCompleto";

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

        // Actualiza los datos de la persona y el estado del proveedor en una transacción
        public bool ModificarProveedor(Persona objPersona, Proveedor objProveedor, out string mensajeError)
        {
            mensajeError = string.Empty;
            ClsDataBase database = new ClsDataBase();

            try
            {
                database.NameSP = "sp_ModificarProveedorCompleto";

                database.DtParameters.Rows.Add("@p_idProveedor", null, objProveedor.Id_proveedor);
                database.DtParameters.Rows.Add("@p_nombre", null, objPersona.nombre_persona);
                database.DtParameters.Rows.Add("@p_apellido", null, objPersona.apellido_persona);
                database.DtParameters.Rows.Add("@p_dni", null, objPersona.dni_persona);
                database.DtParameters.Rows.Add("@p_correo", null, objPersona.correo_persona);
                database.DtParameters.Rows.Add("@p_telefono", null, objPersona.telefono_persona);
                database.DtParameters.Rows.Add("@p_direccion", null, objPersona.direccion_persona);
                database.DtParameters.Rows.Add("@p_estado", null, objProveedor.Estado_proveedor ? 1 : 0);

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

        // Baja lógica: el proveedor queda inactivo, no se borra (sus compras siguen existiendo)
        public bool EliminarProveedor(int idProveedor, out string mensajeError)
        {
            mensajeError = string.Empty;
            ClsDataBase database = new ClsDataBase();

            try
            {
                database.NameSP = "sp_EliminarProveedor";
                database.DtParameters.Rows.Add("@p_idProveedor", null, idProveedor);

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

        public List<Proveedor> Listar()
        {
            List<Proveedor> lista = new List<Proveedor>();
            ClsDataBase database = new ClsDataBase();

            database.NameSP = "sp_ListarProveedores";
            database.TableName = "TablaProveedores";

            database.EjecutarSelect();

            if (!string.IsNullOrEmpty(database.ErrorDB))
                throw new Exception(database.ErrorDB);

            DataTable tabla = database.DsResults.Tables["TablaProveedores"];

            foreach (DataRow fila in tabla.Rows)
            {
                lista.Add(new Proveedor()
                {
                    Id_proveedor = Convert.ToInt32(fila["idProveedor"]),
                    Estado_proveedor = Convert.ToBoolean(fila["estado_proveedor"]),

                    Id_persona = new Persona()
                    {
                        idPersona = Convert.ToInt32(fila["idPersona"]),
                        dni_persona = fila["dni_persona"].ToString(),
                        nombre_persona = fila["nombre_persona"].ToString(),
                        apellido_persona = fila["apellido_persona"].ToString(),
                        correo_persona = fila["correo_persona"].ToString(),
                        telefono_persona = fila["telefono_persona"].ToString(),
                        direccion_persona = fila["direccion_persona"].ToString()
                    }
                });
            }

            return lista;
        }
    }
}