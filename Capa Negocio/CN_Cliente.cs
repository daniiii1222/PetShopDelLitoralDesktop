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
                throw new Exception("Error en la Capa de Negocio al listar clientes: " + ex.Message);
            }
        }

        public bool RegistrarCliente(Persona objPersona, out string mensaje)
        {
            mensaje = string.Empty;

            if (string.IsNullOrEmpty(objPersona.nombre_persona) || string.IsNullOrEmpty(objPersona.apellido_persona))
            {
                mensaje = "El nombre y apellido son obligatorios.";
                return false;
            }

            if (string.IsNullOrEmpty(objPersona.dni_persona))
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

            return objClienteDatos.RegistrarCliente(objPersona, out mensaje);
        }

        public bool ModificarCliente(Persona objPersona, Cliente objCliente, out string mensaje)
        {
            mensaje = string.Empty;

            if (string.IsNullOrEmpty(objPersona.nombre_persona) || string.IsNullOrEmpty(objPersona.apellido_persona))
            {
                mensaje = "El nombre y apellido son obligatorios.";
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

            return objClienteDatos.ModificarCliente(objPersona, objCliente, out mensaje);
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
    }
}
