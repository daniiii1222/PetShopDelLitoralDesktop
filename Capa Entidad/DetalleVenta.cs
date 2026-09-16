using System;
using System.Collections.Generic;
using System.Text;

namespace Capa_Entidad
{
    public class DetalleVenta
    {
        public int IdDetalleVenta { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
        public Producto IdProducto { get; set; }
        public Venta IdVenta { get; set; }
        public DateTime FechaCreacion_detalleV { get; set; }
        public decimal Subtotal_venta { get; set; }
    }
}
