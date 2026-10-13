
USE petshopdellitoral;

DROP PROCEDURE IF EXISTS sp_ObtenerResumenDashboard;

DELIMITER $$

CREATE PROCEDURE sp_ObtenerResumenDashboard()
BEGIN
    SELECT
        -- Ventas registradas hoy y cuánto se vendió
        (SELECT COUNT(*)
           FROM venta
          WHERE fecha_venta = CURDATE() AND estado_venta = 1)                  AS ventas_hoy,
        (SELECT IFNULL(SUM(total_venta), 0)
           FROM venta
          WHERE fecha_venta = CURDATE() AND estado_venta = 1)                  AS total_hoy,

        -- Clientes activos (la baja es lógica: estado_cliente = 0)
        (SELECT COUNT(*) FROM cliente WHERE estado_cliente = 1)                AS clientes,

        -- Productos activos que llegaron al stock mínimo o por debajo
        (SELECT COUNT(*)
           FROM producto
          WHERE estado_producto = 1 AND stock_producto <= stock_minimo)        AS stock_bajo;
END$$

DELIMITER ;

