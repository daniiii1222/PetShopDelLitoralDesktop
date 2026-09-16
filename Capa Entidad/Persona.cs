using System;
using System.Collections.Generic;
using System.Text;

namespace Capa_Entidad
{
    public class Persona
    {
        public int idPersona { get; set; }
        public string nombre_persona { get; set; }
        public string apellido_persona { get; set; }
        public string correo_persona { get; set; }
        public string telefono_persona { get; set; }
        public string direccion_persona { get; set; }
        public DateTime fechaCreacion_persona { get; set; }
        public bool estado_persona { get; set; }
        public string dni_persona { get; set; }
    }
}
