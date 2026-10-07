using System;
using System.Collections.Generic;
using Capa_Entidad;
using CapaDatos;

namespace CapaNegocio
{
    public class CN_Producto
    {
        private CD_Producto objcd_producto = new CD_Producto();

        public List<Producto> Listar()
        {
            return objcd_producto.Listar();
        }

        public int Registrar(Producto obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.Nombre_producto) || string.IsNullOrWhiteSpace(obj.Nombre_producto))
            {
                Mensaje = "El nombre del producto no puede estar vacío.";
            }

            if (string.IsNullOrEmpty(Mensaje))
            {
                return objcd_producto.Registrar(obj, out Mensaje);
            }
            else
            {
                return 0;
            }
        }

        public bool Editar(Producto obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.Nombre_producto) || string.IsNullOrWhiteSpace(obj.Nombre_producto))
            {
                Mensaje = "El nombre del producto no puede estar vacío.";
            }

            if (string.IsNullOrEmpty(Mensaje))
            {
                return objcd_producto.Editar(obj, out Mensaje);
            }
            else
            {
                return false;
            }
        }
    }
}