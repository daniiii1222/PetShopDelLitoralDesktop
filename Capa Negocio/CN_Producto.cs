using System;
using System.Collections.Generic;
using Capa_Entidad;
using Capa_Datos;

namespace Capa_Negocio
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

            if (string.IsNullOrWhiteSpace(obj.Codigo_producto))
            {
                Mensaje = "El código del producto no puede estar vacío.";
                return 0;
            }

            if (string.IsNullOrWhiteSpace(obj.Nombre_producto))
            {
                Mensaje = "El nombre del producto no puede estar vacío.";
                return 0;
            }

            if (objcd_producto.ExisteCodigo(obj.Codigo_producto, 0))
            {
                Mensaje = $"Ya existe un producto registrado con el código '{obj.Codigo_producto.Trim().ToUpper()}'.";
                return 0;
            }

            return objcd_producto.Registrar(obj, out Mensaje);
        }

        public bool Editar(Producto obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrWhiteSpace(obj.Codigo_producto))
            {
                Mensaje = "El código del producto no puede estar vacío.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(obj.Nombre_producto))
            {
                Mensaje = "El nombre del producto no puede estar vacío.";
                return false;
            }

            if (objcd_producto.ExisteCodigo(obj.Codigo_producto, obj.IdProducto))
            {
                Mensaje = $"Ya existe otro producto registrado con el código '{obj.Codigo_producto.Trim().ToUpper()}'.";
                return false;
            }

            return objcd_producto.Editar(obj, out Mensaje);
        }

        public bool Eliminar(int idProducto, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (idProducto <= 0)
            {
                Mensaje = "Identificador de producto no válido.";
                return false;
            }

            return objcd_producto.Eliminar(idProducto, out Mensaje);
        }
    }
}
