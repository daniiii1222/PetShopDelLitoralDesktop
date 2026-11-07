-- Ejecutar como SCRIPT en DBeaver (Alt+X) para que tome el DELIMITER.
-- Últimas 10 ventas de un vendedor.
USE petshopdellitoral;

DROP PROCEDURE IF EXISTS sp_ObtenerVentasPorVendedor;

DELIMITER $$

CREATE PROCEDURE sp_ObtenerVentasPorVendedor(IN p_idUsuario INT)
BEGIN
    SELECT 
        v.idVenta,
        v.fecha_venta,
        v.total_venta,
        CONCAT(p.nombre_persona, ' ', p.apellido_persona) AS cliente
    FROM venta v
    INNER JOIN cliente c ON v.idCliente = c.idCliente
    INNER JOIN persona p ON c.idPersona = p.idPersona
    WHERE v.idUsuario = p_idUsuario
    ORDER BY v.idVenta DESC
    LIMIT 10;
END$$

DELIMITER ;
