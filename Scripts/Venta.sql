-- ================================================
-- Stored Procedures de Ventas - VERSIÓN SIMPLE
-- Cada uno hace UNA sola cosa. La lógica de "revisar stock,
-- calcular total y decidir qué hacer" vive en C# (CN_Venta),
-- no acá.
-- Reemplaza a sp_RegistrarVentaCompleta (ese ya no se usa más).
-- ================================================

DELIMITER $$

-- Se mantienen igual, no hace falta tocarlos:
--   sp_ListarMetodosPago
--   sp_BuscarProductosVenta
--   sp_ObtenerClientePorDni

-- Trae el precio y stock ACTUALES de un producto puntual.
-- Se usa para validar antes de insertar nada.
DROP PROCEDURE IF EXISTS `sp_ObtenerStockProducto`$$
CREATE PROCEDURE `sp_ObtenerStockProducto`(IN p_idProducto INT)
BEGIN
    SELECT idProducto, nombre_producto, precio_producto, stock_producto
    FROM producto
    WHERE idProducto = p_idProducto AND estado_producto = 1;
END$$

-- Inserta solo la cabecera de la venta y devuelve el id generado
DROP PROCEDURE IF EXISTS `sp_InsertarCabeceraVenta`$$
CREATE PROCEDURE `sp_InsertarCabeceraVenta`(
    IN p_idUsuario    INT,
    IN p_idCliente    INT,
    IN p_idMetodoPago INT,
    IN p_fecha        DATE,
    IN p_total        DECIMAL(10,2)
)
BEGIN
    INSERT INTO venta (total_venta, fecha_venta, idUsuario, idCliente, idMetodoPago, estado_venta)
    VALUES (p_total, p_fecha, p_idUsuario, p_idCliente, p_idMetodoPago, 1);

    SELECT LAST_INSERT_ID() AS idVenta;
END$$

-- Inserta UNA línea de detalle
DROP PROCEDURE IF EXISTS `sp_InsertarDetalleVenta`$$
CREATE PROCEDURE `sp_InsertarDetalleVenta`(
    IN p_idVenta    INT,
    IN p_idProducto INT,
    IN p_cantidad   INT,
    IN p_precio     DECIMAL(10,2),
    IN p_subtotal   DECIMAL(10,2)
)
BEGIN
    INSERT INTO detalle_venta (cantidad, precio, idProducto, idVenta, subtotal_venta)
    VALUES (p_cantidad, p_precio, p_idProducto, p_idVenta, p_subtotal);
END$$

-- Descuenta stock de UN producto. La condición "stock_producto >= p_cantidad"
-- en el WHERE es la última barrera: si por algo no alcanza, no actualiza nada.
DROP PROCEDURE IF EXISTS `sp_DescontarStock`$$
CREATE PROCEDURE `sp_DescontarStock`(
    IN p_idProducto INT,
    IN p_cantidad   INT
)
BEGIN
    UPDATE producto
    SET stock_producto = stock_producto - p_cantidad
    WHERE idProducto = p_idProducto AND stock_producto >= p_cantidad;

    -- Devuelve cuántas filas se actualizaron (0 o 1).
    -- Si devuelve 0, el stock no alcanzaba y C# tiene que frenar.
    SELECT ROW_COUNT() AS filasActualizadas;
END$$

DELIMITER ;

-- Ya no se usa más, lo podés borrar de tu base si querés:
-- DROP PROCEDURE IF EXISTS `sp_RegistrarVentaCompleta`;