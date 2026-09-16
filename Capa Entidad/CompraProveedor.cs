using System;
using System.Collections.Generic;
using System.Text;

namespace Capa_Entidad
{
    public class CompraProveedor
    {
        public int IdCompra { get; set; }
        public decimal Monto_compra { get; set; }
        public bool Estado_compra { get; set; }
        public DateTime Fecha_compra { get; set; }
        public Usuario IdUsuario { get; set; }
        public Proveedor IdProveedor { get; set; }
        public DateTime FechaCreacion_compra { get; set; }
    }
}
