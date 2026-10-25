
USE petshopdellitoral;

DROP PROCEDURE IF EXISTS sp_ObtenerResumenDashboard;

DELIMITER $$

CREATE PROCEDURE sp_ObtenerResumenDashboard()
BEGIN
    SELECT
        -- Ventas registradas hoy y cuánto se vendió
        (SELECT COUNT(*)
           FROM venta
          WHERE DATE(fecha_venta) = CURDATE() AND estado_venta = 1)                  AS ventas_hoy,
        (SELECT IFNULL(SUM(total_venta), 0)
           FROM venta
          WHERE DATE(fecha_venta) = CURDATE() AND estado_venta = 1)                  AS total_hoy,

        -- Clientes activos (la baja es lógica: estado_cliente = 0)
        (SELECT COUNT(*) FROM cliente WHERE estado_cliente = 1)                AS clientes,

        -- Productos activos que llegaron al stock mínimo o por debajo
        (SELECT COUNT(*)
           FROM producto
          WHERE estado_producto = 1 AND stock_producto <= stock_minimo)        AS stock_bajo;
END$$

DROP PROCEDURE IF EXISTS sp_ObtenerVentasRecientes$$

CREATE PROCEDURE sp_ObtenerVentasRecientes()
BEGIN
    SELECT 
        v.idVenta,
        v.fecha_venta,
        IFNULL(CONCAT(p.nombre_persona, ' ', p.apellido_persona), 'Consumidor Final') AS cliente,
        v.total_venta
    FROM venta v
    LEFT JOIN cliente c ON v.idCliente = c.idCliente
    LEFT JOIN persona p ON c.idPersona = p.idPersona
    WHERE v.estado_venta = 1
    ORDER BY v.idVenta DESC
    LIMIT 15;
END$$

DELIMITER ;

