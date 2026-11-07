-- Ejecutar como SCRIPT en DBeaver (Alt+X) para que tome el DELIMITER.
-- Busca al usuario por DNI para validar el inicio de sesión (Login).
USE petshopdellitoral;

DROP PROCEDURE IF EXISTS sp_LoginUsuario;

DELIMITER $$

CREATE PROCEDURE sp_LoginUsuario(
    IN p_dni VARCHAR(20)
)
BEGIN
    SELECT u.idUsuario, u.contrasenia_usuario, u.estado_usuario,
           p.idPersona, p.nombre_persona, p.apellido_persona,
           r.idRol, r.descripcion_rol
    FROM usuario u
    INNER JOIN persona p ON u.idPersona = p.idPersona
    INNER JOIN rol r ON u.idRol = r.idRol
    WHERE p.dni_persona = p_dni;
END$$

DELIMITER ;
