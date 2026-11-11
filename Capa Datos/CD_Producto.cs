using Capa_Entidad;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;

namespace Capa_Datos
{
   
    public class CD_Producto
    {
        // ── Listar ───────────────────────────────────────────────────────────

        public List<Producto> Listar()
        {
            ClsDataBase database = new ClsDataBase();
            database.NameSP = "sp_ListarProductos";
            database.TableName = "TablaProductos";

            database.EjecutarSelect();

            if (!string.IsNullOrEmpty(database.ErrorDB))
                throw new Exception("Error al listar productos: " + database.ErrorDB);

            List<Producto> lista = new List<Producto>();

            if (database.DsResults == null
                || !database.DsResults.Tables.Contains("TablaProductos"))
                return lista;

            foreach (DataRow dr in database.DsResults.Tables["TablaProductos"].Rows)
            {
                lista.Add(new Producto()
                {
                    IdProducto = Convert.ToInt32(dr["idProducto"]),
                    Codigo_producto = dr["codigo_producto"] != DBNull.Value ? dr["codigo_producto"].ToString() : string.Empty,
                    Nombre_producto = dr["nombre_producto"].ToString(),
                    Descripcion_producto = dr["descripcion_producto"] != DBNull.Value ? dr["descripcion_producto"].ToString() : string.Empty,
                    Precio_producto = ConvertirDecimal(dr["precio_producto"]),
                    PrecioUnitario_producto = ConvertirDecimal(dr["precioUnitario_producto"]),
                    Stock_producto = Convert.ToInt32(dr["stock_producto"]),
                    Stock_minimo = dr["stock_minimo"] != DBNull.Value ? Convert.ToInt32(dr["stock_minimo"]) : 0,
                    Estado_producto = Convert.ToBoolean(dr["estado_producto"]),
                    FechaCreacion_producto = dr["fechaCreacion_producto"] != DBNull.Value ? Convert.ToDateTime(dr["fechaCreacion_producto"]) : DateTime.MinValue,
                    IdCategoria = new Categoria()
                    {
                        IdCategoria = Convert.ToInt32(dr["idCategoria"]),
                        Nombre_categoria = dr["nombre_categoria"].ToString()
                    }
                });
            }

            return lista;
        }

        // Convierte valores DECIMAL o VARCHAR (ej. "3000.00" en precioUnitario_producto) sin que la configuración regional multiplique por 100
        private static decimal ConvertirDecimal(object valor)
        {
            if (valor == null || valor == DBNull.Value) return 0m;
            if (valor is decimal d) return d;

            string limpio = valor.ToString().Trim().Replace("$", "").Replace(" ", "");
            if (string.IsNullOrEmpty(limpio)) return 0m;

            int ultimaComa = limpio.LastIndexOf(',');
            int ultimoPunto = limpio.LastIndexOf('.');

            if (ultimaComa >= 0 && ultimoPunto >= 0)
            {
                if (ultimaComa > ultimoPunto)
                    limpio = limpio.Replace(".", "").Replace(',', '.');
                else
                    limpio = limpio.Replace(",", "");
            }
            else if (ultimaComa >= 0)
            {
                limpio = limpio.Replace(',', '.');
            }

            return decimal.TryParse(limpio, NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out decimal resultado)
                ? resultado
                : 0m;
        }

        // ── ExisteCodigo ─────────────────────────────────────────────────────

        public bool ExisteCodigo(string codigo, int idProductoExcluir)
        {
            if (string.IsNullOrWhiteSpace(codigo)) return false;

            ClsDataBase database = new ClsDataBase();
            database.NameSP = "sp_ExisteCodigoProducto";
            database.TableName = "TablaExiste";

            database.DtParameters.Rows.Add("@p_codigo", null, codigo.Trim());
            database.DtParameters.Rows.Add("@p_idExcluir", null, idProductoExcluir);

            database.EjecutarSelect();

            if (!string.IsNullOrEmpty(database.ErrorDB))
                throw new Exception("Error al verificar el código del producto: " + database.ErrorDB);

            if (database.DsResults != null
                && database.DsResults.Tables.Contains("TablaExiste")
                && database.DsResults.Tables["TablaExiste"].Rows.Count > 0)
            {
                return Convert.ToInt32(database.DsResults.Tables["TablaExiste"].Rows[0]["cantidad"]) > 0;
            }

            return false;
        }

        // ── Registrar ────────────────────────────────────────────────────────

        public int Registrar(Producto obj, out string mensaje)
        {
            mensaje = string.Empty;
            ClsDataBase database = new ClsDataBase();

            try
            {
                database.NameSP = "sp_RegistrarProducto";
                database.TableName = "TablaIdGenerado";

                database.DtParameters.Rows.Add("@p_codigo", null, string.IsNullOrWhiteSpace(obj.Codigo_producto) ? (object)DBNull.Value : obj.Codigo_producto.Trim());
                database.DtParameters.Rows.Add("@p_nombre", null, obj.Nombre_producto);
                database.DtParameters.Rows.Add("@p_descripcion", null, string.IsNullOrEmpty(obj.Descripcion_producto) ? obj.Nombre_producto : obj.Descripcion_producto);
                database.DtParameters.Rows.Add("@p_precio", null, obj.Precio_producto);
                database.DtParameters.Rows.Add("@p_precioUnitario", null, obj.PrecioUnitario_producto);
                database.DtParameters.Rows.Add("@p_stock", null, obj.Stock_producto);
                database.DtParameters.Rows.Add("@p_stockMinimo", null, obj.Stock_minimo);
                database.DtParameters.Rows.Add("@p_idCategoria", null, obj.IdCategoria.IdCategoria);

                database.EjecutarSelect(); // SELECT LAST_INSERT_ID() devuelve un resultado

                if (!string.IsNullOrEmpty(database.ErrorDB))
                {
                    mensaje = database.ErrorDB;
                    return 0;
                }

                if (database.DsResults != null
                    && database.DsResults.Tables.Contains("TablaIdGenerado")
                    && database.DsResults.Tables["TablaIdGenerado"].Rows.Count > 0)
                {
                    return Convert.ToInt32(database.DsResults.Tables["TablaIdGenerado"].Rows[0]["idGenerado"]);
                }

                return 0;
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
                return 0;
            }
        }

        // ── Editar ───────────────────────────────────────────────────────────

        public bool Editar(Producto obj, out string mensaje)
        {
            mensaje = string.Empty;
            ClsDataBase database = new ClsDataBase();

            try
            {
                database.NameSP = "sp_ModificarProducto";

                database.DtParameters.Rows.Add("@p_idProducto", null, obj.IdProducto);
                database.DtParameters.Rows.Add("@p_codigo", null, string.IsNullOrWhiteSpace(obj.Codigo_producto) ? (object)DBNull.Value : obj.Codigo_producto.Trim());
                database.DtParameters.Rows.Add("@p_nombre", null, obj.Nombre_producto);
                database.DtParameters.Rows.Add("@p_descripcion", null, string.IsNullOrEmpty(obj.Descripcion_producto) ? obj.Nombre_producto : obj.Descripcion_producto);
                database.DtParameters.Rows.Add("@p_precio", null, obj.Precio_producto);
                database.DtParameters.Rows.Add("@p_precioUnitario", null, obj.PrecioUnitario_producto);
                database.DtParameters.Rows.Add("@p_stock", null, obj.Stock_producto);
                database.DtParameters.Rows.Add("@p_stockMinimo", null, obj.Stock_minimo);
                database.DtParameters.Rows.Add("@p_idCategoria", null, obj.IdCategoria.IdCategoria);
                database.DtParameters.Rows.Add("@p_estado", null, obj.Estado_producto ? 1 : 0);

                database.EjecutarAccion();

                if (!string.IsNullOrEmpty(database.ErrorDB))
                {
                    mensaje = database.ErrorDB;
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
                return false;
            }
        }

        // ── Eliminar ─────────────────────────────────────────────────────────

        public bool Eliminar(int idProducto, out string mensaje)
        {
            mensaje = string.Empty;
            ClsDataBase database = new ClsDataBase();

            try
            {
                database.NameSP = "sp_EliminarProducto";
                database.DtParameters.Rows.Add("@p_idProducto", null, idProducto);

                database.EjecutarAccion();

                if (!string.IsNullOrEmpty(database.ErrorDB))
                {
                    mensaje = database.ErrorDB;
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
                return false;
            }
        }
    }
}