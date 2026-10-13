using System.Collections.Generic;
using Capa_Entidad;
using CapaDatos;

namespace CapaNegocio
{
    public class CN_Categoria
    {
        private CD_Categoria obj_cd_categoria = new CD_Categoria();

        public List<Categoria> Listar()
        {
            return obj_cd_categoria.Listar();
        }

        public int Registrar(Categoria obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.Nombre_categoria))
            {
                Mensaje = "Es necesario el nombre de la categoría";
                return 0;
            }

            return obj_cd_categoria.Registrar(obj, out Mensaje);
        }

        public bool Editar(Categoria obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.Nombre_categoria))
            {
                Mensaje = "Es necesario el nombre de la categoría";
                return false;
            }

            return obj_cd_categoria.Editar(obj, out Mensaje);
        }
    }
}