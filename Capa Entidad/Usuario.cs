using System;
using System.Collections.Generic;
using System.Text;


namespace Capa_Entidad
{
    internal class Usuario
    {
        public int idUsuario { get; set; }
        public string nombre_usuario { get; set; }
        public string apellido_usuario { get; set; }
        public string correo_usuario { get; set; }
        public string telefono_usuario { get; set; }
        public string contrasenia_usuario { get; set; }
        public int estado_usuario { get; set; }
        public int idRol { get; set; }
        public DateTime fechaCreacion_usuario { get; set; }
    }
}
