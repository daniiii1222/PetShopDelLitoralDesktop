using System;
using System.Collections.Generic;
using System.Text;

namespace Capa_Entidad
{
    public class Cliente
    {
        public int IdCliente { get; set; }
        public DateTime FechaCreacion_cliente { get; set; }
        public bool Estado_cliente { get; set; }
        public Persona IdPersona { get; set; }
        public string Correo_cliente { get; set; }
    }
}
