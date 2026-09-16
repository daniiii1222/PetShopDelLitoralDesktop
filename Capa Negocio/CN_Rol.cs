using System;
using System.Data;
using Capa_Datos;

namespace Capa_Negocio
{
    public class CN_Rol
    {
        private CD_Rol objRolDatos = new CD_Rol();

        public DataSet ListarRoles()
        {
            try
            {
                return objRolDatos.ListarRoles();
            }
            catch (Exception ex)
            {
                throw new Exception("Error en la Capa de Negocio al listar roles: " + ex.Message);
            }
        }
    }
}