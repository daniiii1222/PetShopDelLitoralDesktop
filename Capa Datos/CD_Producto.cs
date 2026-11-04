using Capa_Entidad;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace Capa_Datos
{
    public class CD_Producto
    {
        // Si alguien se trae los cambios o restaura un backup viejo sin haber corrido Scripts/Productos.sql,
        // se crean automáticamente los 5 procedimientos para que la pantalla nunca falle.
        private static bool FaltaProcedimiento(string errorDb)
        {
            if (string.IsNullOrEmpty(errorDb)) return false;
            return errorDb.IndexOf("cannot be found", StringComparison.OrdinalIgnoreCase) >= 0
                || errorDb.IndexOf("does not exist", StringComparison.OrdinalIgnoreCase) >= 0;
        }

        private static void AsegurarProcedimientos()
        {
            string[] comandos = new string[]
            {
                "DROP PROCEDURE IF EXISTS sp_ListarProductos",
                @"CREATE PROCEDURE sp_ListarProductos()
BEGIN
    SELECT
        p.idProducto,
        p.codigo_producto,
        p.nombre_producto,
        p.descripcion_producto,
        p.precio_producto,
        p.precioUnitario_producto,
        p.stock_producto,
        p.stock_minimo,
        p.estado_producto,
        p.fechaCreacion_producto,
        p.idCategoria,
        c.nombre_categoria
    FROM producto p
    INNER JOIN categoria c ON p.idCategoria = c.idCategoria
    WHERE p.estado_producto = 1
    ORDER BY p.nombre_producto;
END",
                "DROP PROCEDURE IF EXISTS sp_RegistrarProducto",
                @"CREATE PROCEDURE sp_RegistrarProducto(
    IN p_codigo         VARCHAR(50),
    IN p_nombre         VARCHAR(150),
    IN p_descripcion    VARCHAR(255),
    IN p_precio         DECIMAL(10,2),
    IN p_precioUnitario DECIMAL(10,2),
    IN p_stock          INT,
    IN p_stockMinimo    INT,
    IN p_idCategoria    INT
)
BEGIN
    INSERT INTO producto (
        codigo_producto,
        nombre_producto,
        descripcion_producto,
        precio_producto,
        precioUnitario_producto,
        stock_producto,
        stock_minimo,
        idCategoria,
        estado_producto,
        fechaCreacion_producto
    )
    VALUES (
        UPPER(TRIM(p_codigo)),
        p_nombre,
        p_descripcion,
        p_precio,
        p_precioUnitario,
        p_stock,
        p_stockMinimo,
        p_idCategoria,
        1,
        NOW()
    );

    SELECT LAST_INSERT_ID() AS idGenerado;
END",
                "DROP PROCEDURE IF EXISTS sp_ModificarProducto",
                @"CREATE PROCEDURE sp_ModificarProducto(
    IN p_idProducto     INT,
    IN p_codigo         VARCHAR(50),
    IN p_nombre         VARCHAR(150),
    IN p_descripcion    VARCHAR(255),
    IN p_precio         DECIMAL(10,2),
    IN p_precioUnitario DECIMAL(10,2),
    IN p_stock          INT,
    IN p_stockMinimo    INT,
    IN p_idCategoria    INT,
    IN p_estado         TINYINT
)
BEGIN
    UPDATE producto
    SET
        codigo_producto         = UPPER(TRIM(p_codigo)),
        nombre_producto         = p_nombre,
        descripcion_producto    = p_descripcion,
        precio_producto         = p_precio,
        precioUnitario_producto = p_precioUnitario,
        stock_producto          = p_stock,
        stock_minimo            = p_stockMinimo,
        idCategoria             = p_idCategoria,
        estado_producto         = p_estado
    WHERE idProducto = p_idProducto;
END",
                "DROP PROCEDURE IF EXISTS sp_EliminarProducto",
                @"CREATE PROCEDURE sp_EliminarProducto(
    IN p_idProducto INT
)
BEGIN
    UPDATE producto
    SET estado_producto = 0
    WHERE idProducto = p_idProducto;
END",
                "DROP PROCEDURE IF EXISTS sp_ExisteCodigoProducto",
                @"CREATE PROCEDURE sp_ExisteCodigoProducto(
    IN p_codigo     VARCHAR(50),
    IN p_idExcluir  INT
)
BEGIN
    SELECT COUNT(*) AS cantidad
    FROM producto
    WHERE estado_producto = 1
      AND LOWER(codigo_producto) = LOWER(TRIM(p_codigo))
      AND idProducto != p_idExcluir;
END"
            };

            using (MySqlConnection conn = ClsDataBase.CrearConexion())
            {
                conn.Open();
                foreach (string sql in comandos)
                {
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        // ── Listar ───────────────────────────────────────────────────────────

        public List<Producto> Listar()
        {
            ClsDataBase database = new ClsDataBase();
            database.NameSP    = "sp_ListarProductos";
            database.TableName = "TablaProductos";

            database.EjecutarSelect();

            if (FaltaProcedimiento(database.ErrorDB))
            {
                try
                {
                    AsegurarProcedimientos();
                    database = new ClsDataBase();
                    database.NameSP    = "sp_ListarProductos";
                    database.TableName = "TablaProductos";
                    database.EjecutarSelect();
                }
                catch { }
            }

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
                    IdProducto              = Convert.ToInt32(dr["idProducto"]),
                    Codigo_producto         = dr["codigo_producto"]         != DBNull.Value ? dr["codigo_producto"].ToString()         : string.Empty,
                    Nombre_producto         = dr["nombre_producto"].ToString(),
                    Descripcion_producto    = dr["descripcion_producto"]    != DBNull.Value ? dr["descripcion_producto"].ToString()    : string.Empty,
                    Precio_producto         = Convert.ToDecimal(dr["precio_producto"]),
                    PrecioUnitario_producto = dr["precioUnitario_producto"] != DBNull.Value ? Convert.ToDecimal(dr["precioUnitario_producto"]) : 0,
                    Stock_producto          = Convert.ToInt32(dr["stock_producto"]),
                    Stock_minimo            = dr["stock_minimo"]            != DBNull.Value ? Convert.ToInt32(dr["stock_minimo"])            : 0,
                    Estado_producto         = Convert.ToBoolean(dr["estado_producto"]),
                    FechaCreacion_producto  = dr["fechaCreacion_producto"]  != DBNull.Value ? Convert.ToDateTime(dr["fechaCreacion_producto"]) : DateTime.MinValue,
                    IdCategoria = new Categoria()
                    {
                        IdCategoria      = Convert.ToInt32(dr["idCategoria"]),
                        Nombre_categoria = dr["nombre_categoria"].ToString()
                    }
                });
            }

            return lista;
        }

        // ── ExisteCodigo ─────────────────────────────────────────────────────

        public bool ExisteCodigo(string codigo, int idProductoExcluir)
        {
            if (string.IsNullOrWhiteSpace(codigo)) return false;

            ClsDataBase database = new ClsDataBase();
            database.NameSP    = "sp_ExisteCodigoProducto";
            database.TableName = "TablaExiste";

            database.DtParameters.Rows.Add("@p_codigo",    null, codigo.Trim());
            database.DtParameters.Rows.Add("@p_idExcluir", null, idProductoExcluir);

            database.EjecutarSelect();

            if (FaltaProcedimiento(database.ErrorDB))
            {
                try
                {
                    AsegurarProcedimientos();
                    database = new ClsDataBase();
                    database.NameSP    = "sp_ExisteCodigoProducto";
                    database.TableName = "TablaExiste";
                    database.DtParameters.Rows.Add("@p_codigo",    null, codigo.Trim());
                    database.DtParameters.Rows.Add("@p_idExcluir", null, idProductoExcluir);
                    database.EjecutarSelect();
                }
                catch { }
            }

            if (!string.IsNullOrEmpty(database.ErrorDB)) return false;

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

                database.DtParameters.Rows.Add("@p_codigo",         null, string.IsNullOrWhiteSpace(obj.Codigo_producto) ? (object)DBNull.Value : obj.Codigo_producto.Trim());
                database.DtParameters.Rows.Add("@p_nombre",         null, obj.Nombre_producto);
                database.DtParameters.Rows.Add("@p_descripcion",    null, string.IsNullOrEmpty(obj.Descripcion_producto) ? obj.Nombre_producto : obj.Descripcion_producto);
                database.DtParameters.Rows.Add("@p_precio",         null, obj.Precio_producto);
                database.DtParameters.Rows.Add("@p_precioUnitario", null, obj.PrecioUnitario_producto);
                database.DtParameters.Rows.Add("@p_stock",          null, obj.Stock_producto);
                database.DtParameters.Rows.Add("@p_stockMinimo",    null, obj.Stock_minimo);
                database.DtParameters.Rows.Add("@p_idCategoria",    null, obj.IdCategoria.IdCategoria);

                database.EjecutarSelect(); // SELECT LAST_INSERT_ID() devuelve un resultado

                if (FaltaProcedimiento(database.ErrorDB))
                {
                    AsegurarProcedimientos();
                    database = new ClsDataBase();
                    database.NameSP = "sp_RegistrarProducto";
                    database.TableName = "TablaIdGenerado";
                    database.DtParameters.Rows.Add("@p_codigo",         null, string.IsNullOrWhiteSpace(obj.Codigo_producto) ? (object)DBNull.Value : obj.Codigo_producto.Trim());
                    database.DtParameters.Rows.Add("@p_nombre",         null, obj.Nombre_producto);
                    database.DtParameters.Rows.Add("@p_descripcion",    null, string.IsNullOrEmpty(obj.Descripcion_producto) ? obj.Nombre_producto : obj.Descripcion_producto);
                    database.DtParameters.Rows.Add("@p_precio",         null, obj.Precio_producto);
                    database.DtParameters.Rows.Add("@p_precioUnitario", null, obj.PrecioUnitario_producto);
                    database.DtParameters.Rows.Add("@p_stock",          null, obj.Stock_producto);
                    database.DtParameters.Rows.Add("@p_stockMinimo",    null, obj.Stock_minimo);
                    database.DtParameters.Rows.Add("@p_idCategoria",    null, obj.IdCategoria.IdCategoria);
                    database.EjecutarSelect();
                }

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

                database.DtParameters.Rows.Add("@p_idProducto",     null, obj.IdProducto);
                database.DtParameters.Rows.Add("@p_codigo",         null, string.IsNullOrWhiteSpace(obj.Codigo_producto) ? (object)DBNull.Value : obj.Codigo_producto.Trim());
                database.DtParameters.Rows.Add("@p_nombre",         null, obj.Nombre_producto);
                database.DtParameters.Rows.Add("@p_descripcion",    null, string.IsNullOrEmpty(obj.Descripcion_producto) ? obj.Nombre_producto : obj.Descripcion_producto);
                database.DtParameters.Rows.Add("@p_precio",         null, obj.Precio_producto);
                database.DtParameters.Rows.Add("@p_precioUnitario", null, obj.PrecioUnitario_producto);
                database.DtParameters.Rows.Add("@p_stock",          null, obj.Stock_producto);
                database.DtParameters.Rows.Add("@p_stockMinimo",    null, obj.Stock_minimo);
                database.DtParameters.Rows.Add("@p_idCategoria",    null, obj.IdCategoria.IdCategoria);
                database.DtParameters.Rows.Add("@p_estado",         null, obj.Estado_producto ? 1 : 0);

                database.EjecutarAccion();

                if (FaltaProcedimiento(database.ErrorDB))
                {
                    AsegurarProcedimientos();
                    database = new ClsDataBase();
                    database.NameSP = "sp_ModificarProducto";
                    database.DtParameters.Rows.Add("@p_idProducto",     null, obj.IdProducto);
                    database.DtParameters.Rows.Add("@p_codigo",         null, string.IsNullOrWhiteSpace(obj.Codigo_producto) ? (object)DBNull.Value : obj.Codigo_producto.Trim());
                    database.DtParameters.Rows.Add("@p_nombre",         null, obj.Nombre_producto);
                    database.DtParameters.Rows.Add("@p_descripcion",    null, string.IsNullOrEmpty(obj.Descripcion_producto) ? obj.Nombre_producto : obj.Descripcion_producto);
                    database.DtParameters.Rows.Add("@p_precio",         null, obj.Precio_producto);
                    database.DtParameters.Rows.Add("@p_precioUnitario", null, obj.PrecioUnitario_producto);
                    database.DtParameters.Rows.Add("@p_stock",          null, obj.Stock_producto);
                    database.DtParameters.Rows.Add("@p_stockMinimo",    null, obj.Stock_minimo);
                    database.DtParameters.Rows.Add("@p_idCategoria",    null, obj.IdCategoria.IdCategoria);
                    database.DtParameters.Rows.Add("@p_estado",         null, obj.Estado_producto ? 1 : 0);
                    database.EjecutarAccion();
                }

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

                if (FaltaProcedimiento(database.ErrorDB))
                {
                    AsegurarProcedimientos();
                    database = new ClsDataBase();
                    database.NameSP = "sp_EliminarProducto";
                    database.DtParameters.Rows.Add("@p_idProducto", null, idProducto);
                    database.EjecutarAccion();
                }

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
