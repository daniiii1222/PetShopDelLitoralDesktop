-- Ejecutar como SCRIPT en DBeaver (Alt+X) para que tome el DELIMITER.
-- Modifica los datos personales, el rol, el estado y (si se envía) la contraseña de un usuario.
USE petshopdellitoral;

DROP PROCEDURE IF EXISTS sp_ModificarUsuarioCompleto;

DELIMITER $$

CREATE PROCEDURE sp_ModificarUsuarioCompleto(
    IN p_idUsuario INT,
    IN p_nombre VARCHAR(100),
    IN p_apellido VARCHAR(100),
    IN p_dni VARCHAR(20),
    IN p_correo VARCHAR(100),
    IN p_telefono VARCHAR(20),
    IN p_direccion VARCHAR(200),
    IN p_idRol INT,
    IN p_contrasena VARCHAR(100),
    IN p_estado TINYINT(1) 
)
BEGIN
    DECLARE v_idPersona INT;

    -- Buscamos cuál es el idPersona
    SELECT idPersona INTO v_idPersona FROM usuario WHERE idUsuario = p_idUsuario;

    -- Actualizamos tabla persona
    UPDATE persona 
    SET 
        nombre_persona = p_nombre,
        apellido_persona = p_apellido,
        dni_persona = p_dni,
        correo_persona = p_correo,
        telefono_persona = p_telefono,
        direccion_persona = p_direccion
    WHERE idPersona = v_idPersona;

    -- Actualizamos tabla usuario
    IF p_contrasena IS NULL OR p_contrasena = '' THEN
        UPDATE usuario 
        SET 
            idRol = p_idRol,
            estado_usuario = p_estado
        WHERE idUsuario = p_idUsuario;
    ELSE
        UPDATE usuario 
        SET 
            idRol = p_idRol,
            estado_usuario = p_estado,
            contrasenia_usuario = p_contrasena
        WHERE idUsuario = p_idUsuario;
    END IF;

END$$

DELIMITER ;
