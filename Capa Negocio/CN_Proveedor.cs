using System.Collections.Generic;
using Capa_Entidad;
using CapaDatos;

namespace CapaNegocio
{
    public class CN_Proveedor
    {
        private CD_Proveedor obj_cd_proveedor = new CD_Proveedor();

        public List<Proveedor> Listar()
        {
            return obj_cd_proveedor.Listar();
        }

        public int Registrar(Proveedor obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (obj.Id_persona == null || string.IsNullOrEmpty(obj.Id_persona.dni_persona))
            {
                Mensaje = "Es necesario el DNI de la persona asociada al proveedor";
                return 0;
            }

            return obj_cd_proveedor.Registrar(obj, out Mensaje);
        }

        public bool Editar(Proveedor obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (obj.Id_persona == null || string.IsNullOrEmpty(obj.Id_persona.dni_persona))
            {
                Mensaje = "Es necesario el DNI de la persona asociada al proveedor";
                return false;
            }

            return obj_cd_proveedor.Editar(obj, out Mensaje);
        }
    }
}