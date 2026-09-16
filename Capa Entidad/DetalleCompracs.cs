using System;
using System.Collections.Generic;
using System.Text;

namespace Capa_Entidad
{
    public class DetalleCompracs
    {
        public int IdDetalleCompra { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio_unitario { get; set; }
        public decimal Subtotal_compra { get; set; }
        public Producto IdProducto { get; set; }
        public CompraProveedor IdCompra { get; set; }
        public DateTime FechaCreacion_detalleC { get; set; }
    }
}
