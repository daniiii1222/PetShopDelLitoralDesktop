-- Ejecutar como SCRIPT en DBeaver (Alt+X) para que tome el DELIMITER.
-- Busca una persona por DNI (evita duplicados al registrar).
USE petshopdellitoral;

DROP PROCEDURE IF EXISTS sp_ObtenerPersonaPorDni;

DELIMITER $$

CREATE PROCEDURE sp_ObtenerPersonaPorDni(IN _dni_persona VARCHAR(20))
BEGIN
    SELECT 
        idPersona, 
        nombre_persona, 
        apellido_persona, 
        dni_persona, 
        correo_persona, 
        telefono_persona, 
        direccion_persona, 
        estado_persona
    FROM persona
    WHERE dni_persona = _dni_persona;
END$$

DELIMITER ;
