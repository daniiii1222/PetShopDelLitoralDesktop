CREATE DEFINER=`root`@`localhost` PROCEDURE `petshopdellitoral`.`sp_BuscarProductosVenta`(IN p_texto VARCHAR(50))
BEGIN
    SELECT idProducto, codigo_producto, nombre_producto, precio_producto, stock_producto
    FROM producto
    WHERE estado_producto = 1
      AND stock_producto > 0
      AND (
          nombre_producto LIKE CONCAT('%', p_texto, '%')
          OR (codigo_producto IS NOT NULL AND (codigo_producto = p_texto OR codigo_producto LIKE CONCAT('%', p_texto, '%')))
          OR (CAST(idProducto AS CHAR) = p_texto)
      )
    ORDER BY 
      CASE WHEN codigo_producto = p_texto THEN 0 ELSE 1 END,
      nombre_producto;
END;