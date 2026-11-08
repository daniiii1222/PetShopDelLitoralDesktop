-- Ejecutar como SCRIPT en DBeaver (Alt+X) para que tome el DELIMITER.
-- Lista los clientes con sus datos personales (pantalla Personas).
USE petshopdellitoral;

DROP PROCEDURE IF EXISTS sp_ListarClientes;

DELIMITER $$

CREATE PROCEDURE sp_ListarClientes()
BEGIN
    SELECT
        c.idCliente,
        c.fechaCreacion_cliente,
        c.estado_cliente,
        p.idPersona,
        p.nombre_persona,
        p.apellido_persona,
        p.dni_persona,
        p.correo_persona,
        p.telefono_persona,
        p.direccion_persona
    FROM cliente c
    INNER JOIN persona p ON c.idPersona = p.idPersona
    ORDER BY p.apellido_persona, p.nombre_persona;
END$$

DELIMITER ;
