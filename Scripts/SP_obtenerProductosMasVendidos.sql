-- Productos más vendidos en un rango de fechas (para la pantalla de Estadísticas)
-- Tablas usadas: venta, detalle_venta, producto

DROP PROCEDURE IF EXISTS sp_ObtenerProductosMasVendidos;

DELIMITER $$
CREATE PROCEDURE sp_ObtenerProductosMasVendidos(
    IN p_desde  DATE,
    IN p_hasta  DATE,
    IN p_limite INT
)
BEGIN
    SELECT p.nombre_producto    AS Producto,
           SUM(d.cantidad)      AS Unidades,
           SUM(d.subtotal_venta) AS Monto
    FROM detalle_venta d
    INNER JOIN venta    v ON v.IdVenta    = d.IdVenta
    INNER JOIN producto p ON p.IdProducto = d.IdProducto
    WHERE v.fecha_venta >= p_desde
      AND v.fecha_venta <  DATE_ADD(p_hasta, INTERVAL 1 DAY)   -- incluye todo el día "hasta"
      AND v.estado_venta = 1                                   -- solo ventas activas (verificar, ver nota)
    GROUP BY p.IdProducto, p.nombre_producto
    ORDER BY Unidades DESC, p.nombre_producto
    LIMIT p_limite;
END$$
DELIMITER ;

