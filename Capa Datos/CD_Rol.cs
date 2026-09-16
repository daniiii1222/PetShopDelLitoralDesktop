using System;
using System.Data;
using CapaDatos;

namespace Capa_Datos
{
    public class CD_Rol
    {
        public DataSet ListarRoles()
        {
            ClsDataBase database = new ClsDataBase();

            database.NameSP = "sp_ListarRoles";
            database.TableName = "TablaRoles";

            database.EjecutarSelect();

            if (!string.IsNullOrEmpty(database.ErrorDB))
            {
                throw new Exception(database.ErrorDB);
            }

            return database.DsResults;
        }
    }
}