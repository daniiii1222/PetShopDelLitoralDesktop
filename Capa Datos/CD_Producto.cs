using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using Capa_Entidad;

namespace CapaDatos
{
    public class CD_Producto
    {
        private readonly string cadenaConexion = "Server=localhost; Database=petshopdellitoral; Uid=root; Pwd=;";

        public List<Producto> Listar()
        {
            List<Producto> lista = new List<Producto>();

            using (MySqlConnection oconexion = new MySqlConnection(cadenaConexion))
            {
                try
                {
                    string query = @"SELECT p.id_producto, p.nombre_producto, 
                                            p.descripcion, p.precio_venta, p.stock_producto, 
                                            p.stock_minimo, p.id_categoria, c.nombre_categoria, p.estado
                                     FROM producto p
                                     INNER JOIN categoria c ON p.id_categoria = c.id_categoria";

                    MySqlCommand cmd = new MySqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Producto()
                            {
                                IdProducto = Convert.ToInt32(dr["id_producto"]),
                                Nombre_producto = dr["nombre_producto"].ToString(),
                                Descripcion_producto = dr["descripcion"].ToString(),
                                Precio_producto = Convert.ToDecimal(dr["precio_venta"]),
                                Stock_producto = Convert.ToInt32(dr["stock_producto"]),
                                Stock_minimo = Convert.ToInt32(dr["stock_minimo"]),
                                IdCategoria = new Categoria()
                                {
                                    IdCategoria = Convert.ToInt32(dr["id_categoria"]),
                                    Nombre_categoria = dr["nombre_categoria"].ToString()
                                },
                                Estado_producto = Convert.ToBoolean(dr["estado"])
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    lista = new List<Producto>();
                }
            }

            return lista;
        }

        public int Registrar(Producto obj, out string Mensaje)
        {
            int idGenerado = 0;
            Mensaje = string.Empty;

            using (MySqlConnection oconexion = new MySqlConnection(cadenaConexion))
            {
                try
                {
                    string query = @"INSERT INTO producto(nombre_producto, descripcion, precio_venta, stock_producto, stock_minimo, id_categoria, estado) 
                                     VALUES(@nombre, @descripcion, @precio, @stock, @stock_min, @id_categoria, @estado);
                                     SELECT LAST_INSERT_ID();";

                    MySqlCommand cmd = new MySqlCommand(query, oconexion);
                    cmd.Parameters.AddWithValue("@nombre", obj.Nombre_producto);
                    cmd.Parameters.AddWithValue("@descripcion", obj.Descripcion_producto);
                    cmd.Parameters.AddWithValue("@precio", obj.Precio_producto);
                    cmd.Parameters.AddWithValue("@stock", obj.Stock_producto);
                    cmd.Parameters.AddWithValue("@stock_min", obj.Stock_minimo);
                    cmd.Parameters.AddWithValue("@id_categoria", obj.IdCategoria.IdCategoria);
                    cmd.Parameters.AddWithValue("@estado", obj.Estado_producto);

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

        public bool Editar(Producto obj, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = string.Empty;

            using (MySqlConnection oconexion = new MySqlConnection(cadenaConexion))
            {
                try
                {
                    string query = @"UPDATE producto SET 
                                     nombre_producto = @nombre,
                                     descripcion = @descripcion,
                                     precio_venta = @precio,
                                     stock_minimo = @stock_min,
                                     id_categoria = @id_categoria,
                                     estado = @estado
                                     WHERE id_producto = @id;";

                    MySqlCommand cmd = new MySqlCommand(query, oconexion);
                    cmd.Parameters.AddWithValue("@id", obj.IdProducto);
                    cmd.Parameters.AddWithValue("@nombre", obj.Nombre_producto);
                    cmd.Parameters.AddWithValue("@descripcion", obj.Descripcion_producto);
                    cmd.Parameters.AddWithValue("@precio", obj.Precio_producto);
                    cmd.Parameters.AddWithValue("@stock_min", obj.Stock_minimo);
                    cmd.Parameters.AddWithValue("@id_categoria", obj.IdCategoria.IdCategoria);
                    cmd.Parameters.AddWithValue("@estado", obj.Estado_producto);

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