using System;
using System.Collections.Generic;
using System.Text;

namespace Capa_Entidad
{
    public class Venta
    {
        public int IdVenta { get; set; }
        public decimal Total_venta { get; set; }
        public DateTime Fecha_venta { get; set; }
        public Usuario IdUsuario { get; set; }
        public Cliente IdCliente { get; set; }
        public MetodoPago IdMetodoPago { get; set; }
        public DateTime FechaCreacion_venta { get; set; }
        public bool Estado_venta { get; set; }
    }
}
