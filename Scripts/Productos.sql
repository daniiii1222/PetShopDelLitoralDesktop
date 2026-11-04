-- =============================================================================
-- Procedimientos almacenados para el ABM de Productos
-- Base de datos: petshopdellitoral
-- =============================================================================

USE petshopdellitoral;

-- =============================================
-- sp_ListarProductos
-- =============================================
DROP PROCEDURE IF EXISTS sp_ListarProductos;
DELIMITER $$
CREATE PROCEDURE sp_ListarProductos()
BEGIN
    SELECT
        p.idProducto,
        p.codigo_producto,
        p.nombre_producto,
        p.descripcion_producto,
        p.precio_producto,
        p.precioUnitario_producto,
        p.stock_producto,
        p.stock_minimo,
        p.estado_producto,
        p.fechaCreacion_producto,
        p.idCategoria,
        c.nombre_categoria
    FROM producto p
    INNER JOIN categoria c ON p.idCategoria = c.idCategoria
    WHERE p.estado_producto = 1
    ORDER BY p.nombre_producto;
END$$
DELIMITER ;


-- =============================================
-- sp_RegistrarProducto
-- =============================================
DROP PROCEDURE IF EXISTS sp_RegistrarProducto;
DELIMITER $$
CREATE PROCEDURE sp_RegistrarProducto(
    IN p_codigo         VARCHAR(50),
    IN p_nombre         VARCHAR(150),
    IN p_descripcion    VARCHAR(255),
    IN p_precio         DECIMAL(10,2),
    IN p_precioUnitario DECIMAL(10,2),
    IN p_stock          INT,
    IN p_stockMinimo    INT,
    IN p_idCategoria    INT
)
BEGIN
    INSERT INTO producto (
        codigo_producto,
        nombre_producto,
        descripcion_producto,
        precio_producto,
        precioUnitario_producto,
        stock_producto,
        stock_minimo,
        idCategoria,
        estado_producto,
        fechaCreacion_producto
    )
    VALUES (
        UPPER(TRIM(p_codigo)),
        p_nombre,
        p_descripcion,
        p_precio,
        p_precioUnitario,
        p_stock,
        p_stockMinimo,
        p_idCategoria,
        1,       -- estado activo por defecto
        NOW()    -- fecha automática
    );

    SELECT LAST_INSERT_ID() AS idGenerado;
END$$
DELIMITER ;


-- =============================================
-- sp_ModificarProducto
-- =============================================
DROP PROCEDURE IF EXISTS sp_ModificarProducto;
DELIMITER $$
CREATE PROCEDURE sp_ModificarProducto(
    IN p_idProducto     INT,
    IN p_codigo         VARCHAR(50),
    IN p_nombre         VARCHAR(150),
    IN p_descripcion    VARCHAR(255),
    IN p_precio         DECIMAL(10,2),
    IN p_precioUnitario DECIMAL(10,2),
    IN p_stock          INT,
    IN p_stockMinimo    INT,
    IN p_idCategoria    INT,
    IN p_estado         TINYINT
)
BEGIN
    UPDATE producto
    SET
        codigo_producto         = UPPER(TRIM(p_codigo)),
        nombre_producto         = p_nombre,
        descripcion_producto    = p_descripcion,
        precio_producto         = p_precio,
        precioUnitario_producto = p_precioUnitario,
        stock_producto          = p_stock,
        stock_minimo            = p_stockMinimo,
        idCategoria             = p_idCategoria,
        estado_producto         = p_estado
    WHERE idProducto = p_idProducto;
END$$
DELIMITER ;


-- =============================================
-- sp_EliminarProducto  (baja lógica)
-- =============================================
DROP PROCEDURE IF EXISTS sp_EliminarProducto;
DELIMITER $$
CREATE PROCEDURE sp_EliminarProducto(
    IN p_idProducto INT
)
BEGIN
    UPDATE producto
    SET estado_producto = 0
    WHERE idProducto = p_idProducto;
END$$
DELIMITER ;


-- =============================================
-- sp_ExisteCodigoProducto
-- =============================================
DROP PROCEDURE IF EXISTS sp_ExisteCodigoProducto;
DELIMITER $$
CREATE PROCEDURE sp_ExisteCodigoProducto(
    IN p_codigo     VARCHAR(50),
    IN p_idExcluir  INT          -- 0 cuando es un registro nuevo
)
BEGIN
    SELECT COUNT(*) AS cantidad
    FROM producto
    WHERE estado_producto = 1
      AND LOWER(codigo_producto) = LOWER(TRIM(p_codigo))
      AND idProducto != p_idExcluir;
END$$
DELIMITER ;
