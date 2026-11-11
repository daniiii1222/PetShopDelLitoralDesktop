using Capa_Entidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Capa_Datos;
namespace Capa_Negocio
{
    public class CN_Cliente
    {
        private CD_Cliente objClienteDatos = new CD_Cliente();

        public DataSet ListarClientes()
        {
            try
            {
                return objClienteDatos.ListarClientes();
            }
            catch (Exception ex)
            {
                throw new Exception("Error en la Capa de Negocio al listar clientes: " + ex.Message, ex);
            }
        }

        public bool RegistrarCliente(Persona objPersona, out string mensaje)
        {
            mensaje = string.Empty;

            if (objPersona == null)
            {
                mensaje = "Los datos de la persona no son válidos.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(objPersona.nombre_persona) ||
            string.IsNullOrWhiteSpace(objPersona.apellido_persona))
            {
                mensaje = "El nombre y el apellido son obligatorios.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(objPersona.dni_persona))
            {
                mensaje = "El DNI es obligatorio.";
                return false;
            }

            if (!string.IsNullOrEmpty(objPersona.correo_persona) &&
                !Regex.IsMatch(objPersona.correo_persona, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                mensaje = "El correo ingresado no tiene un formato válido.";
                return false;
            }

            // Evita registrar dos veces a la misma persona como cliente
            if (ExistePersonaConDni(objPersona.dni_persona))
            {
                mensaje = "Ya existe una persona registrada con ese DNI.";
                return false;
            }

            bool resultado = objClienteDatos.RegistrarCliente(objPersona, out mensaje);

            if (!resultado)
                mensaje = TraducirMensajeError(mensaje);

            return resultado;
        }

        public bool ModificarCliente(Persona objPersona, Cliente objCliente, out string mensaje)
        {
            mensaje = string.Empty;


            if (objPersona == null)
            {
                mensaje = "Los datos de la persona no son válidos.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(objPersona.nombre_persona) ||
                string.IsNullOrWhiteSpace(objPersona.apellido_persona))
            {
                mensaje = "El nombre y el apellido son obligatorios.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(objPersona.dni_persona))
            {
                mensaje = "El DNI es obligatorio.";
                return false;
            }
            if (objCliente == null || objCliente.IdCliente <= 0)
            {
                mensaje = "Cliente no válido.";
                return false;
            }

            if (!string.IsNullOrEmpty(objPersona.correo_persona) &&
                !Regex.IsMatch(objPersona.correo_persona, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                mensaje = "El correo ingresado no tiene un formato válido.";
                return false;
            }

            bool resultado = objClienteDatos.ModificarCliente(objPersona, objCliente, out mensaje);

            if (!resultado)
                mensaje = TraducirMensajeError(mensaje);

            return resultado;
        }

        public bool EliminarCliente(int idCliente, out string mensaje)
        {
            mensaje = string.Empty;

            if (idCliente <= 0)
            {
                mensaje = "ID de cliente no válido.";
                return false;
            }

            return objClienteDatos.EliminarCliente(idCliente, out mensaje);
        }

        public DataTable ObtenerPersonaPorDni(string dni)
        {
            return objClienteDatos.ObtenerPersonaPorDni(dni);
        }

        private bool ExistePersonaConDni(string dni)
        {
            DataTable tabla = objClienteDatos.ObtenerPersonaPorDni(dni);
            return tabla != null && tabla.Rows.Count > 0;
        }

        private static string TraducirMensajeError(string mensaje)
        {
            if (!string.IsNullOrEmpty(mensaje) &&
                mensaje.IndexOf("uq_persona_correo", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                return "El correo electrónico ingresado ya se encuentra registrado. Por favor, ingresá otro correo.";
            }

            return mensaje;
        }
    }
}
