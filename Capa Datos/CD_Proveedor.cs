using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using Capa_Entidad;

namespace Capa_Datos
{
    public class CD_Proveedor
    {

        // =====================================================================
        // ALTA / MODIFICACIÓN / BAJA de proveedores (mismo esquema que CD_Cliente).
        // Cada operación es UN procedimiento almacenado (Scripts/Proveedores.sql) que guarda
        // Persona + Proveedor dentro de una sola transacción en la base de datos.
        // =====================================================================

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

            using (MySqlConnection oconexion = ClsDataBase.CrearConexion())
            {
                try
                {
                    string query = @"SELECT p.idProveedor, p.estado_proveedor, 
                                            per.idPersona, per.dni_persona, per.nombre_persona, per.apellido_persona, 
                                            per.correo_persona, per.telefono_persona, per.direccion_persona
                                     FROM proveedor p
                                     INNER JOIN persona per ON p.id_persona = per.idPersona";

                    MySqlCommand cmd = new MySqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Proveedor()
                            {
                                Id_proveedor = Convert.ToInt32(dr["idProveedor"]),
                                Estado_proveedor = Convert.ToBoolean(dr["estado_proveedor"]),
                                Id_persona = new Persona()
                                {
                                    idPersona = Convert.ToInt32(dr["idPersona"]),
                                    dni_persona = dr["dni_persona"].ToString(),
                                    nombre_persona = dr["nombre_persona"].ToString(),
                                    apellido_persona = dr["apellido_persona"].ToString(),
                                    correo_persona = dr["correo_persona"].ToString(),
                                    telefono_persona = dr["telefono_persona"].ToString(),
                                    direccion_persona = dr["direccion_persona"].ToString()
                                }
                            });
                        }
                    }
                }
                catch (Exception)
                {
                    lista = new List<Proveedor>();
                }
            }

            return lista;
        }

        public int Registrar(Proveedor obj, out string Mensaje)
        {
            int idGenerado = 0;
            Mensaje = string.Empty;

            using (MySqlConnection oconexion = ClsDataBase.CrearConexion())
            {
                try
                {
                    string query = "INSERT INTO proveedor(id_persona, estado_proveedor) VALUES(@id_persona, @estado_proveedor); SELECT LAST_INSERT_ID();";

                    MySqlCommand cmd = new MySqlCommand(query, oconexion);
                    cmd.Parameters.AddWithValue("@id_persona", obj.Id_persona.idPersona);
                    cmd.Parameters.AddWithValue("@estado_proveedor", obj.Estado_proveedor);

                    oconexion.Open();
                    idGenerado = Convert.ToInt32(cmd.ExecuteScalar());
                }
                catch (Exception ex)
                {
                    idGenerado = 0;
                    Mensaje = ex.Message;
                }
            }

            return idGenerado;
        }

        public bool Editar(Proveedor obj, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = string.Empty;

            using (MySqlConnection oconexion = ClsDataBase.CrearConexion())
            {
                try
                {
                    string query = "UPDATE proveedor SET id_persona = @id_persona, estado_proveedor = @estado_proveedor WHERE idProveedor = @id_proveedor";

                    MySqlCommand cmd = new MySqlCommand(query, oconexion);
                    cmd.Parameters.AddWithValue("@id_proveedor", obj.Id_proveedor);
                    cmd.Parameters.AddWithValue("@id_persona", obj.Id_persona.idPersona);
                    cmd.Parameters.AddWithValue("@estado_proveedor", obj.Estado_proveedor);

                    oconexion.Open();
                    respuesta = cmd.ExecuteNonQuery() > 0;
                }
                catch (Exception ex)
                {
                    respuesta = false;
                    Mensaje = ex.Message;
                }
            }

            return respuesta;
        }

        // Carga proveedores de ejemplo (con sus personas) cuando la tabla está vacía
        public bool CrearProveedoresIniciales(out string Mensaje)
        {
            Mensaje = string.Empty;

            using (MySqlConnection oconexion = ClsDataBase.CrearConexion())
            {
                try
                {
                    string query = @"
                INSERT INTO persona(nombre_persona, apellido_persona, dni_persona, correo_persona, telefono_persona, direccion_persona, estado_persona, fechaCreacion_persona)
                VALUES('Distribuidora PetFood', 'Litoral', '30-11223344-5', 'ventas@petfood.com', '3794112233', 'Av. Libertad 1234', 1, NOW()),
                      ('Laboratorio VetSalud', 'Argentina', '30-55667788-9', 'contacto@vetsalud.com', '3794556677', 'Ruta 12 Km 5', 1, NOW());

                INSERT INTO proveedor(id_persona, estado_proveedor, fechaCreacion_proveedor)
                SELECT idPersona, 1, NOW() FROM persona WHERE dni_persona IN ('30-11223344-5', '30-55667788-9')
                AND idPersona NOT IN (SELECT id_persona FROM proveedor);";

                    MySqlCommand cmd = new MySqlCommand(query, oconexion);
                    oconexion.Open();
                    cmd.ExecuteNonQuery();
                    return true;
                }
                catch (Exception ex)
                {
                    Mensaje = ex.Message;
                    return false;
                }
            }
        }
    }
}