-- Ejecutar como SCRIPT en DBeaver (Alt+X) para que tome el DELIMITER.
-- Lista los usuarios con sus datos personales y su rol (pantalla Personas).
USE petshopdellitoral;

DROP PROCEDURE IF EXISTS sp_ListarUsuarios;

DELIMITER $$

CREATE PROCEDURE sp_ListarUsuarios()
BEGIN
    SELECT 
        u.idUsuario AS idUsuario,
        p.idPersona AS idPersona,
        p.nombre_persona AS nombre_persona,
        p.apellido_persona AS apellido_persona,
        p.dni_persona AS dni_persona,
        p.correo_persona AS correo_persona,
        p.telefono_persona AS telefono_persona,
        p.direccion_persona AS direccion_persona,
        r.idRol AS idRol,
        r.descripcion_rol AS descripcion_rol,
        u.estado_usuario AS estado_usuario
    FROM usuario u
    INNER JOIN persona p ON u.idPersona = p.idPersona
    INNER JOIN rol r ON u.idRol = r.idRol;
END$$

DELIMITER ;
