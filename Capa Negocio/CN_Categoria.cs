using Capa_Datos;
using Capa_Entidad;
using System.Collections.Generic;

namespace Capa_Negocio
{
    public class CN_Categoria
    {
        private const int NOMBRE_MAXIMO = 50;   // largo de la columna nombre_categoria en la base

        private CD_Categoria obj_cd_categoria = new CD_Categoria();

        public List<Categoria> Listar()
        {
            return obj_cd_categoria.Listar();
        }

        public int Registrar(Categoria obj, out string Mensaje)
        {
            if (!ValidarNombre(obj, out Mensaje))
                return 0;

            return obj_cd_categoria.Registrar(obj, out Mensaje);
        }

        public bool Editar(Categoria obj, out string Mensaje)
        {
            if (!ValidarNombre(obj, out Mensaje))
                return false;

            return obj_cd_categoria.Editar(obj, out Mensaje);
        }

        // Regla de negocio: la categoría necesita un nombre que no sea solo espacios y que entre en la columna
        private static bool ValidarNombre(Categoria obj, out string mensaje)
        {
            mensaje = string.Empty;

            if (obj == null)
            {
                mensaje = "La categoría no puede ser nula.";
                return false;
            }

            string nombre = (obj.Nombre_categoria ?? string.Empty).Trim();

            if (nombre.Length == 0)
            {
                mensaje = "Es necesario el nombre de la categoría";
                return false;
            }

            if (nombre.Length > NOMBRE_MAXIMO)
            {
                mensaje = "El nombre de la categoría no puede superar los " + NOMBRE_MAXIMO + " caracteres.";
                return false;
            }

            return true;
        }
    }
}