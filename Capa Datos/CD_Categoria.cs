using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using Capa_Entidad;

namespace CapaDatos
{
    public class CD_Categoria
    {
        private readonly string cadenaConexion = "Server=localhost; Database=petshopdellitoral; Uid=root; Pwd=;";

        public List<Categoria> Listar()
        {
            List<Categoria> lista = new List<Categoria>();

            using (MySqlConnection oconexion = new MySqlConnection(cadenaConexion))
            {
                try
                {
                    string query = "SELECT id_categoria, descripcion, estado FROM categoria";
                    MySqlCommand cmd = new MySqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Categoria()
                            {
                                IdCategoria = Convert.ToInt32(dr["id_categoria"]),
                                Nombre_categoria = dr["descripcion"].ToString(),
                                Estado_categoria = Convert.ToBoolean(dr["estado"])
                            });
                        }
                    }
                }
                catch (Exception)
                {
                    lista = new List<Categoria>();
                }
            }

            return lista;
        }

        public int Registrar(Categoria obj, out string Mensaje)
        {
            int idGenerado = 0;
            Mensaje = string.Empty;

            using (MySqlConnection oconexion = new MySqlConnection(cadenaConexion))
            {
                try
                {
                    string query = "INSERT INTO categoria(descripcion, estado) VALUES(@descripcion, @estado); SELECT LAST_INSERT_ID();";

                    MySqlCommand cmd = new MySqlCommand(query, oconexion);
                    cmd.Parameters.AddWithValue("@descripcion", obj.Nombre_categoria);
                    cmd.Parameters.AddWithValue("@estado", obj.Estado_categoria);

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

        public bool Editar(Categoria obj, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = string.Empty;

            using (MySqlConnection oconexion = new MySqlConnection(cadenaConexion))
            {
                try
                {
                    string query = "UPDATE categoria SET descripcion = @descripcion, estado = @estado WHERE id_categoria = @id";

                    MySqlCommand cmd = new MySqlCommand(query, oconexion);
                    cmd.Parameters.AddWithValue("@id", obj.IdCategoria);
                    cmd.Parameters.AddWithValue("@descripcion", obj.Nombre_categoria);
                    cmd.Parameters.AddWithValue("@estado", obj.Estado_categoria);

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