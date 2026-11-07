-- Ejecutar como SCRIPT en DBeaver (Alt+X) para que tome el DELIMITER.
-- Baja lógica de un cliente (estado_cliente = 0).
USE petshopdellitoral;

DROP PROCEDURE IF EXISTS sp_EliminarCliente;

DELIMITER $$

CREATE PROCEDURE sp_EliminarCliente(
    IN p_idCliente INT
)
BEGIN
    UPDATE cliente SET estado_cliente = 0 WHERE idCliente = p_idCliente;
END$$

DELIMITER ;
