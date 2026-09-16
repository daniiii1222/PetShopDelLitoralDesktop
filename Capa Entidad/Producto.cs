using System;
using System.Collections.Generic;
using System.Text;

namespace Capa_Entidad
{
    public class Producto
    {
        public int IdProducto { get; set; }
        public string Nombre_producto { get; set; }
        public string Descripcion_producto { get; set; }
        public bool Estado_producto { get; set; }
        public decimal Precio_producto { get; set; }
        public int Stock_producto { get; set; }
        public Categoria IdCategoria { get; set; }
        public DateTime FechaCreacion_producto { get; set; }
        public decimal PrecioUnitario_producto { get; set; }
        public int Stock_minimo { get; set; }
    }
}
