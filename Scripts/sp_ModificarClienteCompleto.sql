-- Ejecutar como SCRIPT en DBeaver (Alt+X) para que tome el DELIMITER.
-- Modifica los datos personales y el estado de un cliente.
USE petshopdellitoral;

DROP PROCEDURE IF EXISTS sp_ModificarClienteCompleto;

DELIMITER $$

CREATE PROCEDURE sp_ModificarClienteCompleto(
    IN p_idCliente INT,
    IN p_nombre    VARCHAR(100),
    IN p_apellido  VARCHAR(100),
    IN p_dni       VARCHAR(20),
    IN p_correo    VARCHAR(100),
    IN p_telefono  VARCHAR(30),
    IN p_direccion VARCHAR(150),
    IN p_estado    TINYINT
)
BEGIN
    DECLARE v_idPersona INT;
 
    SELECT idPersona INTO v_idPersona FROM cliente WHERE idCliente = p_idCliente;
 
    UPDATE persona
    SET nombre_persona = p_nombre,
        apellido_persona = p_apellido,
        dni_persona = p_dni,
        correo_persona = p_correo,
        telefono_persona = p_telefono,
        direccion_persona = p_direccion
    WHERE idPersona = v_idPersona;
 
    UPDATE cliente
    SET estado_cliente = p_estado
    WHERE idCliente = p_idCliente;
END$$

DELIMITER ;
