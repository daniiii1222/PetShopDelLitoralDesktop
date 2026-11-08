-- Script para agregar columna codigo_producto a la tabla producto
-- y actualizar el procedimiento sp_BuscarProductosVenta

USE petshopdellitoral;

-- 1. Agregar columna si no existe
SET @col_exists := (
    SELECT COUNT(*) 
    FROM INFORMATION_SCHEMA.COLUMNS 
    WHERE TABLE_SCHEMA = 'petshopdellitoral' 
      AND TABLE_NAME = 'producto' 
      AND COLUMN_NAME = 'codigo_producto'
);

SET @sql := IF(@col_exists = 0, 
    'ALTER TABLE producto ADD COLUMN codigo_producto VARCHAR(50) NULL AFTER idProducto;', 
    'SELECT "Columna codigo_producto ya existe";'
);
PREPARE stmt FROM @sql;
EXECUTE stmt;
DEALLOCATE PREPARE stmt;

-- 2. Asignar código por defecto a productos existentes que no tengan código
UPDATE producto 
SET codigo_producto = CONCAT('PROD', LPAD(idProducto, 3, '0')) 
WHERE codigo_producto IS NULL OR codigo_producto = '';

-- 3. Actualizar procedimiento de búsqueda para ventas
DROP PROCEDURE IF EXISTS sp_BuscarProductosVenta;

DELIMITER $$

CREATE PROCEDURE sp_BuscarProductosVenta(IN p_texto VARCHAR(50))
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
END$$

DELIMITER ;
