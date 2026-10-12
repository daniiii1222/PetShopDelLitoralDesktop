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
                    string query = @"SELECT p.idProducto, p.nombre_producto, 
                                            p.descripcion_producto, p.precio_producto, p.stock_producto, 
                                            p.stock_minimo, p.idCategoria, c.nombre_categoria, p.estado_producto, p.precioUnitario_producto
                                     FROM producto p
                                     INNER JOIN categoria c ON p.idCategoria = c.idCategoria";

                    MySqlCommand cmd = new MySqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Producto()
                            {
                                IdProducto = Convert.ToInt32(dr["idProducto"]),
                                Nombre_producto = dr["nombre_producto"].ToString(),
                                Descripcion_producto = dr["descripcion_producto"] != DBNull.Value ? dr["descripcion_producto"].ToString() : "",
                                Precio_producto = Convert.ToDecimal(dr["precio_producto"]),
                                PrecioUnitario_producto = dr["precioUnitario_producto"] != DBNull.Value ? Convert.ToDecimal(dr["precioUnitario_producto"]) : 0,
                                Stock_producto = Convert.ToInt32(dr["stock_producto"]),
                                Stock_minimo = dr["stock_minimo"] != DBNull.Value ? Convert.ToInt32(dr["stock_minimo"]) : 0,
                                IdCategoria = new Categoria()
                                {
                                    IdCategoria = Convert.ToInt32(dr["idCategoria"]),
                                    Nombre_categoria = dr["nombre_categoria"].ToString()
                                },
                                Estado_producto = Convert.ToBoolean(dr["estado_producto"])
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al listar productos: " + ex.Message, ex);
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
                    string query = @"INSERT INTO producto(nombre_producto, descripcion_producto, estado_producto, precio_producto, stock_producto, idCategoria, precioUnitario_producto, stock_minimo) 
                                     VALUES(@nombre, @descripcion, @estado, @precio, @stock, @idCategoria, @precioUnitario, @stock_min);
                                     SELECT LAST_INSERT_ID();";

                    MySqlCommand cmd = new MySqlCommand(query, oconexion);
                    cmd.Parameters.AddWithValue("@nombre", obj.Nombre_producto);
                    cmd.Parameters.AddWithValue("@descripcion", string.IsNullOrEmpty(obj.Descripcion_producto) ? obj.Nombre_producto : obj.Descripcion_producto);
                    cmd.Parameters.AddWithValue("@estado", obj.Estado_producto);
                    cmd.Parameters.AddWithValue("@precio", obj.Precio_producto);
                    cmd.Parameters.AddWithValue("@stock", obj.Stock_producto);
                    cmd.Parameters.AddWithValue("@idCategoria", obj.IdCategoria.IdCategoria);
                    cmd.Parameters.AddWithValue("@precioUnitario", obj.PrecioUnitario_producto);
                    cmd.Parameters.AddWithValue("@stock_min", obj.Stock_minimo);

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
                                     descripcion_producto = @descripcion,
                                     precio_producto = @precio,
                                     precioUnitario_producto = @precioUnitario,
                                     stock_producto = @stock,
                                     stock_minimo = @stock_min,
                                     idCategoria = @idCategoria,
                                     estado_producto = @estado
                                     WHERE idProducto = @id;";

                    MySqlCommand cmd = new MySqlCommand(query, oconexion);
                    cmd.Parameters.AddWithValue("@id", obj.IdProducto);
                    cmd.Parameters.AddWithValue("@nombre", obj.Nombre_producto);
                    cmd.Parameters.AddWithValue("@descripcion", string.IsNullOrEmpty(obj.Descripcion_producto) ? obj.Nombre_producto : obj.Descripcion_producto);
                    cmd.Parameters.AddWithValue("@precio", obj.Precio_producto);
                    cmd.Parameters.AddWithValue("@precioUnitario", obj.PrecioUnitario_producto);
                    cmd.Parameters.AddWithValue("@stock", obj.Stock_producto);
                    cmd.Parameters.AddWithValue("@stock_min", obj.Stock_minimo);
                    cmd.Parameters.AddWithValue("@idCategoria", obj.IdCategoria.IdCategoria);
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

        public bool Eliminar(int idProducto, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = string.Empty;

            using (MySqlConnection oconexion = new MySqlConnection(cadenaConexion))
            {
                try
                {
                    string query = "DELETE FROM producto WHERE idProducto = @id;";
                    MySqlCommand cmd = new MySqlCommand(query, oconexion);
                    cmd.Parameters.AddWithValue("@id", idProducto);

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