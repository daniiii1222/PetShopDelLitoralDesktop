using System.Collections.Generic;
using Capa_Entidad;
using Capa_Datos;
using System.Text.RegularExpressions;

namespace Capa_Negocio
{
    public class CN_Proveedor
    {
        private CD_Proveedor obj_cd_proveedor = new CD_Proveedor();

        // =====================================================================
        // ALTA / MODIFICACIÓN / BAJA de proveedores (mismo esquema que CN_Cliente)
        // Las validaciones corren acá, antes de llegar a la capa de datos.
        // =====================================================================

        public bool RegistrarProveedor(Persona objPersona, out string mensaje)
        {
            if (!ValidarDatosPersona(objPersona, out mensaje)) return false;

            if (!ValidarDni(objPersona.dni_persona, out mensaje)) return false;

            // Evita registrar dos veces al mismo proveedor
            if (ExisteProveedorConDni(objPersona.dni_persona, 0))
            {
                mensaje = "Ya existe un proveedor registrado con ese DNI.";
                return false;
            }

            return obj_cd_proveedor.RegistrarProveedor(objPersona, out mensaje);
        }

        public bool ModificarProveedor(Persona objPersona, Proveedor objProveedor, out string mensaje)
        {
            if (!ValidarDatosPersona(objPersona, out mensaje)) return false;

            if (objProveedor == null || objProveedor.Id_proveedor <= 0)
            {
                mensaje = "Proveedor no válido.";
                return false;
            }

            if (!ValidarDni(objPersona.dni_persona, out mensaje)) return false;

            // El DNI no puede pertenecer a OTRO proveedor (el propio no cuenta como duplicado)
            if (ExisteProveedorConDni(objPersona.dni_persona, objProveedor.Id_proveedor))
            {
                mensaje = "Ya existe otro proveedor registrado con ese DNI.";
                return false;
            }

            return obj_cd_proveedor.ModificarProveedor(objPersona, objProveedor, out mensaje);
        }

        public bool EliminarProveedor(int idProveedor, out string mensaje)
        {
            mensaje = string.Empty;

            if (idProveedor <= 0)
            {
                mensaje = "ID de proveedor no válido.";
                return false;
            }

            return obj_cd_proveedor.EliminarProveedor(idProveedor, out mensaje);
        }

        // true si otro proveedor (distinto de idProveedorActual) ya usa ese DNI
        public bool ExisteProveedorConDni(string dni, int idProveedorActual)
        {
            if (string.IsNullOrWhiteSpace(dni)) return false;

            string buscado = dni.Trim();
            foreach (Proveedor p in obj_cd_proveedor.Listar())
            {
                if (p.Id_persona != null &&
                    string.Equals((p.Id_persona.dni_persona ?? "").Trim(), buscado, System.StringComparison.OrdinalIgnoreCase) &&
                    p.Id_proveedor != idProveedorActual)
                {
                    return true;
                }
            }
            return false;
        }

        private static bool ValidarDatosPersona(Persona objPersona, out string mensaje)
        {
            mensaje = string.Empty;

            if (objPersona == null)
            {
                mensaje = "Faltan los datos del proveedor.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(objPersona.nombre_persona) || string.IsNullOrWhiteSpace(objPersona.apellido_persona))
            {
                mensaje = "El nombre y apellido son obligatorios.";
                return false;
            }

            if (!string.IsNullOrWhiteSpace(objPersona.correo_persona) &&
                !Regex.IsMatch(objPersona.correo_persona.Trim(), @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                mensaje = "El correo ingresado no tiene un formato válido.";
                return false;
            }

            return true;
        }

        // Acepta DNI (solo números) o CUIT con guiones, como los proveedores cargados de ejemplo (30-11223344-5)
        private static bool ValidarDni(string dni, out string mensaje)
        {
            mensaje = string.Empty;

            if (string.IsNullOrWhiteSpace(dni))
            {
                mensaje = "El DNI es obligatorio.";
                return false;
            }

            if (!Regex.IsMatch(dni.Trim(), @"^\d[\d\-]{5,14}$"))
            {
                mensaje = "El DNI solo puede contener números (y guiones si es un CUIT).";
                return false;
            }

            return true;
        }

        public List<Proveedor> Listar()
        {
            return obj_cd_proveedor.Listar();
        }

        public int Registrar(Proveedor obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (obj.Id_persona == null || string.IsNullOrEmpty(obj.Id_persona.dni_persona))
            {
                Mensaje = "Es necesario el DNI de la persona asociada al proveedor";
                return 0;
            }

            return obj_cd_proveedor.Registrar(obj, out Mensaje);
        }

        public bool Editar(Proveedor obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (obj.Id_persona == null || string.IsNullOrEmpty(obj.Id_persona.dni_persona))
            {
                Mensaje = "Es necesario el DNI de la persona asociada al proveedor";
                return false;
            }

            return obj_cd_proveedor.Editar(obj, out Mensaje);
        }

        public bool CrearProveedoresIniciales(out string Mensaje)
        {
            return obj_cd_proveedor.CrearProveedoresIniciales(out Mensaje);
        }
    }
}