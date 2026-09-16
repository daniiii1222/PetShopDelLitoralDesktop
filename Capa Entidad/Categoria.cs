using System;
using System.Collections.Generic;
using System.Text;

namespace Capa_Entidad
{
    public class Categoria
    {
        public int IdCategoria { get; set; }
        public string Nombre_categoria { get; set; }
        public bool Estado_categoria { get; set; }
        public DateTime FechaCreacion_categoria { get; set; }
    }
}
