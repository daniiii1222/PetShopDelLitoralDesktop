using System;
using System.Collections.Generic;
using System.Text;


namespace Capa_Entidad
{
    public class Usuario
    {
       

            public int idUsuario { get; set; }
            public string contrasenia_usuario { get; set; }
            public bool estado_usuario { get; set; }
            public Rol idRol { get; set; }
            public Persona idPersona { get; set; }
            public DateTime fechaCreacion_usuario { get; set; }
        
    }
}

