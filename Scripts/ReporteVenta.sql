DELIMITER //

CREATE PROCEDURE sp_Reporte_Ventas(
    IN p_fechaInicio DATE, 
    IN p_fechaFin DATE,
    IN p_idVendedor INT
)
BEGIN
    SELECT 
        v.idVenta AS 'N° Venta',
        v.fecha_venta AS 'Fecha',
        CONCAT(p_cli.nombre_persona, ' ', p_cli.apellido_persona) AS 'Cliente',
        CONCAT(p_ven.nombre_persona, ' ', p_ven.apellido_persona) AS 'Vendedor',
        m.nombre_metodo AS 'Método Pago',
        v.descuento_venta AS 'Desc. Compra (%)',
        v.total_venta AS 'Total ($)'
    FROM venta v
    INNER JOIN cliente c ON v.idCliente = c.idCliente
    INNER JOIN persona p_cli ON c.idPersona = p_cli.idPersona
    INNER JOIN usuario u ON v.idUsuario = u.idUsuario
    INNER JOIN persona p_ven ON u.idPersona = p_ven.idPersona
    INNER JOIN metodo_pago m ON v.idMetodoPago = m.idMetodoPago
    WHERE v.fecha_venta BETWEEN p_fechaInicio AND p_fechaFin
      AND (p_idVendedor = 0 OR v.idUsuario = p_idVendedor)
    ORDER BY v.fecha_venta DESC;
END //

DELIMITER ;