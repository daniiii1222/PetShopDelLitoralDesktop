using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using Capa_Entidad;

namespace CapaDatos
{
    public class CD_Proveedor
    {
        private readonly string cadenaConexion = "Server=localhost; Database=petshopdellitoral; Uid=root; Pwd=;";

        public List<Proveedor> Listar()
        {
            List<Proveedor> lista = new List<Proveedor>();

            using (MySqlConnection oconexion = new MySqlConnection(cadenaConexion))
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

            using (MySqlConnection oconexion = new MySqlConnection(cadenaConexion))
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

            using (MySqlConnection oconexion = new MySqlConnection(cadenaConexion))
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
    }
}