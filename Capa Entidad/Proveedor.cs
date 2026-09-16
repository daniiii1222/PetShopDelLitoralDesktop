using System;
using System.Collections.Generic;
using System.Text;

namespace Capa_Entidad
{
    public class Proveedor
    {
        public int Id_proveedor { get; set; }
        public bool Estado_proveedor { get; set; } 
        
        public Persona Id_persona { get; set; }
    }
}
