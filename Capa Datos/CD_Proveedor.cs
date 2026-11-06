using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using Capa_Entidad;

namespace Capa_Datos
{
    public class CD_Proveedor
    {

        // =====================================================================
        // ALTA / MODIFICACIÓN / BAJA de proveedores (mismo esquema que CD_Cliente).
        // Cada operación es UN procedimiento almacenado (Scripts/Proveedores.sql) que guarda
        // Persona + Proveedor dentro de una sola transacción en la base de datos.
        // =====================================================================

        private static bool FaltaProcedimiento(string errorDb)
        {
            if (string.IsNullOrEmpty(errorDb)) return false;
            return errorDb.IndexOf("cannot be found", StringComparison.OrdinalIgnoreCase) >= 0
                || errorDb.IndexOf("does not exist", StringComparison.OrdinalIgnoreCase) >= 0;
        }

        private static void AsegurarProcedimientos()
        {
            string[] comandos = new string[]
            {
                "DROP PROCEDURE IF EXISTS sp_RegistrarProveedorCompleto",
                @"CREATE PROCEDURE sp_RegistrarProveedorCompleto(
    IN p_nombre    VARCHAR(100),
    IN p_apellido  VARCHAR(100),
    IN p_dni       VARCHAR(20),
    IN p_correo    VARCHAR(100),
    IN p_telefono  VARCHAR(30),
    IN p_direccion VARCHAR(150)
)
BEGIN
    DECLARE v_idPersona   INT DEFAULT NULL;
    DECLARE v_idProveedor INT DEFAULT NULL;

    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        ROLLBACK;
        RESIGNAL;
    END;

    START TRANSACTION;

    SET v_idPersona = (SELECT idPersona FROM persona WHERE dni_persona = p_dni LIMIT 1);

    IF v_idPersona IS NULL THEN
        INSERT INTO persona(nombre_persona, apellido_persona, dni_persona, correo_persona,
                            telefono_persona, direccion_persona, estado_persona, fechaCreacion_persona)
        VALUES (p_nombre, p_apellido, p_dni, p_correo, p_telefono, p_direccion, 1, NOW());

        SET v_idPersona = LAST_INSERT_ID();
    ELSE
        IF EXISTS (SELECT 1 FROM proveedor WHERE id_persona = v_idPersona) THEN
            SIGNAL SQLSTATE '45000'
                SET MESSAGE_TEXT = 'Ya existe un proveedor registrado con ese DNI.';
        END IF;

        UPDATE persona
           SET nombre_persona    = p_nombre,
               apellido_persona  = p_apellido,
               correo_persona    = p_correo,
               telefono_persona  = p_telefono,
               direccion_persona = p_direccion,
               estado_persona    = 1
         WHERE idPersona = v_idPersona;
    END IF;

    INSERT INTO proveedor(id_persona, estado_proveedor, fechaCreacion_proveedor)
    VALUES (v_idPersona, 1, NOW());

    SET v_idProveedor = LAST_INSERT_ID();

    COMMIT;

    SELECT v_idProveedor AS idProveedor;
END",
                "DROP PROCEDURE IF EXISTS sp_ModificarProveedorCompleto",
                @"CREATE PROCEDURE sp_ModificarProveedorCompleto(
    IN p_idProveedor INT,
    IN p_nombre      VARCHAR(100),
    IN p_apellido    VARCHAR(100),
    IN p_dni         VARCHAR(20),
    IN p_correo      VARCHAR(100),
    IN p_telefono    VARCHAR(30),
    IN p_direccion   VARCHAR(150),
    IN p_estado      TINYINT
)
BEGIN
    DECLARE v_idPersona INT DEFAULT NULL;

    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        ROLLBACK;
        RESIGNAL;
    END;

    START TRANSACTION;

    SET v_idPersona = (SELECT id_persona FROM proveedor WHERE idProveedor = p_idProveedor);

    IF v_idPersona IS NULL THEN
        SIGNAL SQLSTATE '45000'
            SET MESSAGE_TEXT = 'El proveedor que intenta modificar no existe.';
    END IF;

    IF EXISTS (SELECT 1 FROM persona WHERE dni_persona = p_dni AND idPersona <> v_idPersona) THEN
        SIGNAL SQLSTATE '45000'
            SET MESSAGE_TEXT = 'Ya existe otra persona registrada con ese DNI.';
    END IF;

    UPDATE persona
       SET nombre_persona    = p_nombre,
           apellido_persona  = p_apellido,
           dni_persona       = p_dni,
           correo_persona    = p_correo,
           telefono_persona  = p_telefono,
           direccion_persona = p_direccion
     WHERE idPersona = v_idPersona;

    UPDATE proveedor
       SET estado_proveedor = p_estado
     WHERE idProveedor = p_idProveedor;

    COMMIT;
END",
                "DROP PROCEDURE IF EXISTS sp_EliminarProveedor",
                @"CREATE PROCEDURE sp_EliminarProveedor(
    IN p_idProveedor INT
)
BEGIN
    IF NOT EXISTS (SELECT 1 FROM proveedor WHERE idProveedor = p_idProveedor) THEN
        SIGNAL SQLSTATE '45000'
            SET MESSAGE_TEXT = 'El proveedor que intenta dar de baja no existe.';
    END IF;

    UPDATE proveedor
       SET estado_proveedor = 0
     WHERE idProveedor = p_idProveedor;
END"
            };

            using (MySqlConnection conn = ClsDataBase.CrearConexion())
            {
                conn.Open();
                foreach (string sql in comandos)
                {
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        // Crea la persona (o reutiliza la que ya tenga ese DNI) y el proveedor, todo en una transacción
        public bool RegistrarProveedor(Persona objPersona, out string mensajeError)
        {
            mensajeError = string.Empty;
            ClsDataBase database = new ClsDataBase();

            try
            {
                database.NameSP = "sp_RegistrarProveedorCompleto";

                database.DtParameters.Rows.Add("@p_nombre", null, objPersona.nombre_persona);
                database.DtParameters.Rows.Add("@p_apellido", null, objPersona.apellido_persona);
                database.DtParameters.Rows.Add("@p_dni", null, objPersona.dni_persona);
                database.DtParameters.Rows.Add("@p_correo", null, objPersona.correo_persona);
                database.DtParameters.Rows.Add("@p_telefono", null, objPersona.telefono_persona);
                database.DtParameters.Rows.Add("@p_direccion", null, objPersona.direccion_persona);

                database.EjecutarAccion();

                if (FaltaProcedimiento(database.ErrorDB))
                {
                    AsegurarProcedimientos();
                    database = new ClsDataBase();
                    database.NameSP = "sp_RegistrarProveedorCompleto";
                    database.DtParameters.Rows.Add("@p_nombre", null, objPersona.nombre_persona);
                    database.DtParameters.Rows.Add("@p_apellido", null, objPersona.apellido_persona);
                    database.DtParameters.Rows.Add("@p_dni", null, objPersona.dni_persona);
                    database.DtParameters.Rows.Add("@p_correo", null, objPersona.correo_persona);
                    database.DtParameters.Rows.Add("@p_telefono", null, objPersona.telefono_persona);
                    database.DtParameters.Rows.Add("@p_direccion", null, objPersona.direccion_persona);
                    database.EjecutarAccion();
                }

                if (!string.IsNullOrEmpty(database.ErrorDB))
                {
                    mensajeError = database.ErrorDB;
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                mensajeError = ex.Message;
                return false;
            }
        }

        // Actualiza los datos de la persona y el estado del proveedor en una transacción
        public bool ModificarProveedor(Persona objPersona, Proveedor objProveedor, out string mensajeError)
        {
            mensajeError = string.Empty;
            ClsDataBase database = new ClsDataBase();

            try
            {
                database.NameSP = "sp_ModificarProveedorCompleto";

                database.DtParameters.Rows.Add("@p_idProveedor", null, objProveedor.Id_proveedor);
                database.DtParameters.Rows.Add("@p_nombre", null, objPersona.nombre_persona);
                database.DtParameters.Rows.Add("@p_apellido", null, objPersona.apellido_persona);
                database.DtParameters.Rows.Add("@p_dni", null, objPersona.dni_persona);
                database.DtParameters.Rows.Add("@p_correo", null, objPersona.correo_persona);
                database.DtParameters.Rows.Add("@p_telefono", null, objPersona.telefono_persona);
                database.DtParameters.Rows.Add("@p_direccion", null, objPersona.direccion_persona);
                database.DtParameters.Rows.Add("@p_estado", null, objProveedor.Estado_proveedor ? 1 : 0);

                database.EjecutarAccion();

                if (FaltaProcedimiento(database.ErrorDB))
                {
                    AsegurarProcedimientos();
                    database = new ClsDataBase();
                    database.NameSP = "sp_ModificarProveedorCompleto";
                    database.DtParameters.Rows.Add("@p_idProveedor", null, objProveedor.Id_proveedor);
                    database.DtParameters.Rows.Add("@p_nombre", null, objPersona.nombre_persona);
                    database.DtParameters.Rows.Add("@p_apellido", null, objPersona.apellido_persona);
                    database.DtParameters.Rows.Add("@p_dni", null, objPersona.dni_persona);
                    database.DtParameters.Rows.Add("@p_correo", null, objPersona.correo_persona);
                    database.DtParameters.Rows.Add("@p_telefono", null, objPersona.telefono_persona);
                    database.DtParameters.Rows.Add("@p_direccion", null, objPersona.direccion_persona);
                    database.DtParameters.Rows.Add("@p_estado", null, objProveedor.Estado_proveedor ? 1 : 0);
                    database.EjecutarAccion();
                }

                if (!string.IsNullOrEmpty(database.ErrorDB))
                {
                    mensajeError = database.ErrorDB;
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                mensajeError = ex.Message;
                return false;
            }
        }

        // Baja lógica: el proveedor queda inactivo, no se borra (sus compras siguen existiendo)
        public bool EliminarProveedor(int idProveedor, out string mensajeError)
        {
            mensajeError = string.Empty;
            ClsDataBase database = new ClsDataBase();

            try
            {
                database.NameSP = "sp_EliminarProveedor";
                database.DtParameters.Rows.Add("@p_idProveedor", null, idProveedor);

                database.EjecutarAccion();

                if (FaltaProcedimiento(database.ErrorDB))
                {
                    AsegurarProcedimientos();
                    database = new ClsDataBase();
                    database.NameSP = "sp_EliminarProveedor";
                    database.DtParameters.Rows.Add("@p_idProveedor", null, idProveedor);
                    database.EjecutarAccion();
                }

                if (!string.IsNullOrEmpty(database.ErrorDB))
                {
                    mensajeError = database.ErrorDB;
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                mensajeError = ex.Message;
                return false;
            }
        }

        public List<Proveedor> Listar()
        {
            List<Proveedor> lista = new List<Proveedor>();

            using (MySqlConnection oconexion = ClsDataBase.CrearConexion())
            {
                try
                {
                    string query = @"SELECT p.idProveedor, p.estado_proveedor, 
                                            per.idPersona, per.dni_persona, per.nombre_persona, per.apellido_persona, 
                                            per.correo_persona, per.telefono_persona, per.direccion_persona
                                     FROM proveedor p
                                     INNER JOIN persona per ON p.id_persona = per.idPersona";

                    MySqlCommand cmd = new MySqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Proveedor()
                            {
                                Id_proveedor = Convert.ToInt32(dr["idProveedor"]),
                                Estado_proveedor = Convert.ToBoolean(dr["estado_proveedor"]),
                                Id_persona = new Persona()
                                {
                                    idPersona = Convert.ToInt32(dr["idPersona"]),
                                    dni_persona = dr["dni_persona"].ToString(),
                                    nombre_persona = dr["nombre_persona"].ToString(),
                                    apellido_persona = dr["apellido_persona"].ToString(),
                                    correo_persona = dr["correo_persona"].ToString(),
                                    telefono_persona = dr["telefono_persona"].ToString(),
                                    direccion_persona = dr["direccion_persona"].ToString()
                                }
                            });
                        }
                    }
                }
                catch (Exception)
                {
                    lista = new List<Proveedor>();
                }
            }

            return lista;
        }

        // Carga proveedores de ejemplo (con sus personas) cuando la tabla está vacía
        public bool CrearProveedoresIniciales(out string Mensaje)
        {
            Mensaje = string.Empty;

            using (MySqlConnection oconexion = ClsDataBase.CrearConexion())
            {
                try
                {
                    string query = @"
                INSERT INTO persona(nombre_persona, apellido_persona, dni_persona, correo_persona, telefono_persona, direccion_persona, estado_persona, fechaCreacion_persona)
                VALUES('Distribuidora PetFood', 'Litoral', '30-11223344-5', 'ventas@petfood.com', '3794112233', 'Av. Libertad 1234', 1, NOW()),
                      ('Laboratorio VetSalud', 'Argentina', '30-55667788-9', 'contacto@vetsalud.com', '3794556677', 'Ruta 12 Km 5', 1, NOW());

                INSERT INTO proveedor(id_persona, estado_proveedor, fechaCreacion_proveedor)
                SELECT idPersona, 1, NOW() FROM persona WHERE dni_persona IN ('30-11223344-5', '30-55667788-9')
                AND idPersona NOT IN (SELECT id_persona FROM proveedor);";

                    MySqlCommand cmd = new MySqlCommand(query, oconexion);
                    oconexion.Open();
                    cmd.ExecuteNonQuery();
                    return true;
                }
                catch (Exception ex)
                {
                    Mensaje = ex.Message;
                    return false;
                }
            }
        }
    }
}