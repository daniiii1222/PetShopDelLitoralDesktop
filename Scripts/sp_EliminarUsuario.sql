-- Ejecutar como SCRIPT en DBeaver (Alt+X) para que tome el DELIMITER.
-- Baja lógica de un usuario (estado_usuario = 0).
USE petshopdellitoral;

DROP PROCEDURE IF EXISTS sp_EliminarUsuario;

DELIMITER $$

CREATE PROCEDURE sp_EliminarUsuario(
    IN p_idUsuario INT
)
BEGIN
    UPDATE usuario 
    SET estado_usuario = 0 
    WHERE idUsuario = p_idUsuario;
END$$

DELIMITER ;
