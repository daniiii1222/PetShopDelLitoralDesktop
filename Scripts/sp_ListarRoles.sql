-- Ejecutar como SCRIPT en DBeaver (Alt+X) para que tome el DELIMITER.
-- Lista los roles para el combo de la pantalla Personas.
USE petshopdellitoral;

DROP PROCEDURE IF EXISTS sp_ListarRoles;

DELIMITER $$

CREATE PROCEDURE sp_ListarRoles()
BEGIN
    SELECT idRol, descripcion_rol FROM rol;
END$$

DELIMITER ;
