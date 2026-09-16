using System;
using System.Collections.Generic;
using System.Text;

namespace Capa_Entidad
{
    public class MetodoPago
    {
        public int IdMetodoPago { get; set; }
        public string Nombre_metodo { get; set; }
        public bool Estado_metodo { get; set; }
        public DateTime FechaCreacion_metodo { get; set; }
    }
}
