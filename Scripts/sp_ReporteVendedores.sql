
DROP PROCEDURE IF EXISTS sp_Reporte_Vendedores;
DELIMITER //

CREATE PROCEDURE sp_Reporte_Vendedores()
BEGIN
    SELECT
        u.idUsuario AS 'ID Vendedor',
        CONCAT(p.nombre_persona, ' ', p.apellido_persona) AS 'Vendedor',
        COUNT(v.idVenta) AS 'Cantidad de Ventas'
    FROM usuario u
    INNER JOIN persona p
        ON u.idPersona = p.idPersona
    INNER JOIN venta v
        ON u.idUsuario = v.idUsuario
    GROUP BY
        u.idUsuario,
        p.nombre_persona,
        p.apellido_persona
    ORDER BY COUNT(v.idVenta) DESC;
END //

DELIMITER ;
