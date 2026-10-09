-- Ejecutar como SCRIPT en DBeaver (Alt+X) para que tome el DELIMITER.
-- Crea el procedimiento que usa el botón "Ver Detalle" de Reportes.
USE petshopdellitoral;

DROP PROCEDURE IF EXISTS sp_ObtenerDetalleVenta;

DELIMITER $$

CREATE PROCEDURE sp_ObtenerDetalleVenta(IN p_idVenta INT)
BEGIN
    SELECT
        p.nombre_producto    AS 'Producto',
        dv.cantidad          AS 'Cantidad',
        dv.precio            AS 'Precio ($)',
        dv.descuento_detalle AS 'Desc. (%)',
        dv.subtotal_venta    AS 'Subtotal ($)'
    FROM detalle_venta dv
    INNER JOIN producto p ON p.idProducto = dv.idProducto
    WHERE dv.idVenta = p_idVenta
    ORDER BY dv.idDetalleVenta;
END$$

DELIMITER ;

