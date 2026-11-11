
using System;
using System.Collections.Generic;
using System.Data;
using Capa_Entidad;

namespace Capa_Datos
{
    public class CD_Categoria
    {
        public List<Categoria> Listar()
        {
            List<Categoria> lista = new List<Categoria>();

            ClsDataBase database = new ClsDataBase();
            database.NameSP = "sp_ListarCategorias";
            database.TableName = "TablaCategorias";

            database.EjecutarSelect();

            if (!string.IsNullOrEmpty(database.ErrorDB))
                throw new Exception("Error al listar categorías: " + database.ErrorDB);

            if (database.DsResults == null ||
                !database.DsResults.Tables.Contains("TablaCategorias"))
                return lista;

            foreach (DataRow dr in database.DsResults.Tables["TablaCategorias"].Rows)
            {
                lista.Add(new Categoria()
                {
                    IdCategoria = Convert.ToInt32(dr["idCategoria"]),
                    Nombre_categoria = dr["nombre_categoria"].ToString(),
                    Estado_categoria = Convert.ToBoolean(dr["estado_categoria"])
                });
            }

            return lista;
        }

        public int Registrar(Categoria obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            try
            {
                ClsDataBase database = new ClsDataBase();

                database.NameSP = "sp_RegistrarCategoria";
                database.TableName = "TablaIdGenerado";

                database.DtParameters.Rows.Add(
                    "@p_nombre", null, obj.Nombre_categoria);

                database.DtParameters.Rows.Add(
                    "@p_estado", null, obj.Estado_categoria);

                database.EjecutarSelect();

                if (!string.IsNullOrEmpty(database.ErrorDB))
                {
                    Mensaje = database.ErrorDB;
                    return 0;
                }

                if (database.DsResults != null &&
                    database.DsResults.Tables.Contains("TablaIdGenerado") &&
                    database.DsResults.Tables["TablaIdGenerado"].Rows.Count > 0)
                {
                    return Convert.ToInt32(
                        database.DsResults.Tables["TablaIdGenerado"]
                        .Rows[0]["idGenerado"]);
                }

                Mensaje = "No se pudo obtener el ID de la categoría.";
                return 0;
            }
            catch (Exception ex)
            {
                Mensaje = ex.Message;
                return 0;
            }
        }

        public bool Editar(Categoria obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            try
            {
                ClsDataBase database = new ClsDataBase();

                database.NameSP = "sp_ModificarCategoria";

                database.DtParameters.Rows.Add(
                    "@p_id", null, obj.IdCategoria);

                database.DtParameters.Rows.Add(
                    "@p_nombre", null, obj.Nombre_categoria);

                database.DtParameters.Rows.Add(
                    "@p_estado", null, obj.Estado_categoria);

                database.EjecutarAccion();

                if (!string.IsNullOrEmpty(database.ErrorDB))
                {
                    Mensaje = database.ErrorDB;
                    return false;
                }

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
