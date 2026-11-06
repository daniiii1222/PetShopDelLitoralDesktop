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

        public bool ExisteCodigo(string codigo, int idProductoExcluir)
        {
            return objcd_producto.ExisteCodigo(codigo, idProductoExcluir);
        }

        public int Registrar(Producto obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (obj == null)
            {
                Mensaje = "El producto no puede ser nulo.";
                return 0;
            }

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

            if (obj.Precio_producto < 0)
            {
                Mensaje = "El precio de venta no puede ser negativo.";
                return 0;
            }

            if (obj.PrecioUnitario_producto < 0)
            {
                Mensaje = "El precio de costo no puede ser negativo.";
                return 0;
            }

            if (obj.Precio_producto > 99999999.99m || obj.PrecioUnitario_producto > 99999999.99m)
            {
                Mensaje = "El precio ingresado supera el máximo permitido (99.999.999,99).";
                return 0;
            }

            if (obj.Stock_producto < 0)
            {
                Mensaje = "El stock no puede ser negativo.";
                return 0;
            }

            if (obj.Stock_minimo < 0)
            {
                Mensaje = "El stock mínimo no puede ser negativo.";
                return 0;
            }
            bool codigoExiste;
            try
            {
                codigoExiste = objcd_producto.ExisteCodigo(obj.Codigo_producto, 0);
            }
            catch (Exception ex)
            {
                Mensaje = ex.Message;
                return 0;
            }

            if (codigoExiste)
            {
                Mensaje = $"Ya existe un producto registrado con el código '{obj.Codigo_producto.Trim().ToUpper()}'.";
                return 0;
            }

            return objcd_producto.Registrar(obj, out Mensaje);
        }

        public bool Editar(Producto obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (obj == null)
            {
                Mensaje = "El producto no puede ser nulo.";
                return false;
            }

            if (obj.IdProducto <= 0)
            {
                Mensaje = "Identificador de producto no válido.";
                return false;
            }

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

            if (obj.Precio_producto < 0)
            {
                Mensaje = "El precio de venta no puede ser negativo.";
                return false;
            }

            if (obj.PrecioUnitario_producto < 0)
            {
                Mensaje = "El precio de costo no puede ser negativo.";
                return false;
            }

            if (obj.Precio_producto > 99999999.99m || obj.PrecioUnitario_producto > 99999999.99m)
            {
                Mensaje = "El precio ingresado supera el máximo permitido (99.999.999,99).";
                return false;
            }

            if (obj.Stock_producto < 0)
            {
                Mensaje = "El stock no puede ser negativo.";
                return false;
            }

            if (obj.Stock_minimo < 0)
            {
                Mensaje = "El stock mínimo no puede ser negativo.";
                return false;
            }
            bool codigoExiste;
            try
            {
                codigoExiste = objcd_producto.ExisteCodigo(obj.Codigo_producto, obj.IdProducto);
            }
            catch (Exception ex)
            {
                Mensaje = ex.Message;
                return false;
            }

            if (codigoExiste)
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
