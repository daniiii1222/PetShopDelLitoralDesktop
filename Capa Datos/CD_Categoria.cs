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
                    string query = "SELECT idCategoria, nombre_categoria, estado_categoria FROM categoria";
                    MySqlCommand cmd = new MySqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Categoria()
                            {
                                IdCategoria = Convert.ToInt32(dr["idCategoria"]),
                                Nombre_categoria = dr["nombre_categoria"].ToString(),
                                Estado_categoria = Convert.ToBoolean(dr["estado_categoria"])
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al listar categorías: " + ex.Message, ex);
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
                    string query = "INSERT INTO categoria(nombre_categoria, estado_categoria) VALUES(@nombre, @estado); SELECT LAST_INSERT_ID();";

                    MySqlCommand cmd = new MySqlCommand(query, oconexion);
                    cmd.Parameters.AddWithValue("@nombre", obj.Nombre_categoria);
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
                    string query = "UPDATE categoria SET nombre_categoria = @nombre, estado_categoria = @estado WHERE idCategoria = @id";

                    MySqlCommand cmd = new MySqlCommand(query, oconexion);
                    cmd.Parameters.AddWithValue("@id", obj.IdCategoria);
                    cmd.Parameters.AddWithValue("@nombre", obj.Nombre_categoria);
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