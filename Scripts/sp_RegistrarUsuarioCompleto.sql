-- Ejecutar como SCRIPT en DBeaver (Alt+X) para que tome el DELIMITER.
-- Da de alta la persona y el usuario (con rol y contraseña) en un solo paso.
USE petshopdellitoral;

DROP PROCEDURE IF EXISTS sp_RegistrarUsuarioCompleto;

DELIMITER $$

CREATE PROCEDURE sp_RegistrarUsuarioCompleto(
    IN p_nombre VARCHAR(100),
    IN p_apellido VARCHAR(100),
    IN p_dni VARCHAR(20),
    IN p_correo VARCHAR(100),
    IN p_telefono VARCHAR(50),
    IN p_direccion VARCHAR(255),
    IN p_contrasena VARCHAR(100),
    IN p_idRol INT
)
BEGIN
    DECLARE v_idPersona INT;

    INSERT INTO persona (nombre_persona, apellido_persona, dni_persona, correo_persona, telefono_persona, direccion_persona, estado_persona)
    VALUES (p_nombre, p_apellido, p_dni, p_correo, p_telefono, p_direccion, 1);

    SET v_idPersona = LAST_INSERT_ID();

    INSERT INTO usuario (contrasenia_usuario, estado_usuario, idRol, idPersona)
    VALUES (p_contrasena, 1, p_idRol, v_idPersona);
END$$

DELIMITER ;
