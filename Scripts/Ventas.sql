-- ================================================
-- VENTAS - script unico y final (Persona B)
--   * Columnas de descuento (venta.descuento_venta, detalle_venta.descuento_detalle)
--   * SP de ventas: metodos de pago, buscar productos, cliente por DNI y registrar venta
--     (con descuento por producto y por compra, y descuento de stock en transaccion)
-- Es idempotente: se puede correr varias veces. Correr en CADA base local.
-- NO incluye datos de prueba (esos van aparte) ni los SP de cliente.
-- Requiere MariaDB >= 10.6 (JSON_TABLE y ADD COLUMN IF NOT EXISTS).
-- ================================================
USE petshopdellitoral;

ALTER TABLE detalle_venta
    ADD COLUMN IF NOT EXISTS descuento_detalle DECIMAL(5,2) NOT NULL DEFAULT 0.00;

ALTER TABLE venta
    ADD COLUMN IF NOT EXISTS descuento_venta DECIMAL(5,2) NOT NULL DEFAULT 0.00;

DELIMITER $$

-- Métodos de pago activos, para el combo de FrmVentas
DROP PROCEDURE IF EXISTS `sp_ListarMetodosPago`$$
CREATE PROCEDURE `sp_ListarMetodosPago`()
BEGIN
    SELECT idMetodoPago, nombre_metodo
    FROM metodo_pago
    WHERE estado_metodo = 1
    ORDER BY nombre_metodo;
END$$

-- Busca productos activos con stock por nombre (para agregar al detalle de la venta)
DROP PROCEDURE IF EXISTS `sp_BuscarProductosVenta`$$
CREATE PROCEDURE `sp_BuscarProductosVenta`(IN p_texto VARCHAR(50))
BEGIN
    SELECT idProducto, nombre_producto, precio_producto, stock_producto
    FROM producto
    WHERE estado_producto = 1
      AND stock_producto > 0
      AND nombre_producto LIKE CONCAT('%', p_texto, '%')
    ORDER BY nombre_producto;
END$$

-- Busca un cliente ACTIVO por DNI (une cliente + persona).
-- A diferencia de sp_ObtenerPersonaPorDni, devuelve idCliente, que es lo que necesita una venta.
DROP PROCEDURE IF EXISTS `sp_ObtenerClientePorDni`$$
CREATE PROCEDURE `sp_ObtenerClientePorDni`(IN p_dni VARCHAR(20))
BEGIN
    SELECT c.idCliente, p.nombre_persona, p.apellido_persona, p.dni_persona
    FROM cliente c
    INNER JOIN persona p ON p.idPersona = c.idPersona
    WHERE p.dni_persona = p_dni
      AND c.estado_cliente = 1;
END$$

-- Registra la venta COMPLETA en una sola transaccion (cabecera + detalles + descuento de stock).
DROP PROCEDURE IF EXISTS `sp_RegistrarVentaCompleta`$$
CREATE PROCEDURE `sp_RegistrarVentaCompleta`(
    IN p_idUsuario    INT,
    IN p_idCliente    INT,
    IN p_idMetodoPago INT,
    IN p_fecha        DATE,
    IN p_descuento    DECIMAL(5,2),   -- NUEVO: % sobre la compra completa
    IN p_detalle      LONGTEXT        -- ahora: [{"idProducto":1,"cantidad":2,"descuento":10},...]
)
BEGIN
    DECLARE v_idVenta INT;
    DECLARE v_subtotal DECIMAL(12,2); -- NUEVO: suma de líneas ya con su descuento
    DECLARE v_total DECIMAL(12,2);
    DECLARE v_desc DECIMAL(5,2);      -- NUEVO

    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        ROLLBACK;
        RESIGNAL;
    END;

    -- NUEVO: validar los porcentajes
    SET v_desc = IFNULL(p_descuento, 0);
    IF v_desc < 0 OR v_desc > 100 THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'El descuento de la compra debe estar entre 0 y 100.';
    END IF;

    IF EXISTS (
        SELECT 1
        FROM JSON_TABLE(p_detalle, '$[*]' COLUMNS (
                descuento DECIMAL(5,2) PATH '$.descuento')) j
        WHERE IFNULL(j.descuento, 0) < 0 OR IFNULL(j.descuento, 0) > 100
    ) THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'El descuento de cada producto debe estar entre 0 y 100.';
    END IF;

    START TRANSACTION;

    -- 1) Validar stock (sin cambios)
    IF EXISTS (
        SELECT 1
        FROM JSON_TABLE(p_detalle, '$[*]' COLUMNS (
                idProducto INT PATH '$.idProducto',
                cantidad   INT PATH '$.cantidad')) j
        INNER JOIN producto p ON p.idProducto = j.idProducto
        WHERE p.stock_producto < j.cantidad OR p.estado_producto = 0
        FOR UPDATE
    ) THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Stock insuficiente o producto inactivo.';
    END IF;

    -- 2) CAMBIA: subtotal con descuento por producto, y total con descuento de la compra
    SELECT SUM(ROUND(p.precio_producto * j.cantidad * (1 - IFNULL(j.descuento, 0) / 100), 2))
    INTO v_subtotal
    FROM JSON_TABLE(p_detalle, '$[*]' COLUMNS (
            idProducto INT          PATH '$.idProducto',
            cantidad   INT          PATH '$.cantidad',
            descuento  DECIMAL(5,2) PATH '$.descuento')) j   -- NUEVO: columna descuento
    INNER JOIN producto p ON p.idProducto = j.idProducto;

    IF v_subtotal IS NULL THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'La venta no tiene productos.';
    END IF;

    SET v_total = ROUND(v_subtotal * (1 - v_desc / 100), 2);   -- NUEVO

    -- 3) Cabecera: CAMBIA, guarda descuento_venta
    INSERT INTO venta (total_venta, fecha_venta, idUsuario, idCliente, idMetodoPago, estado_venta, descuento_venta)
    VALUES (v_total, p_fecha, p_idUsuario, p_idCliente, p_idMetodoPago, 1, v_desc);

    SET v_idVenta = LAST_INSERT_ID();

    -- 4) Detalles: CAMBIA, subtotal con descuento y guarda descuento_detalle
    INSERT INTO detalle_venta (cantidad, precio, idProducto, idVenta, subtotal_venta, descuento_detalle)
    SELECT j.cantidad, p.precio_producto, j.idProducto, v_idVenta,
           ROUND(p.precio_producto * j.cantidad * (1 - IFNULL(j.descuento, 0) / 100), 2),
           IFNULL(j.descuento, 0)
    FROM JSON_TABLE(p_detalle, '$[*]' COLUMNS (
            idProducto INT          PATH '$.idProducto',
            cantidad   INT          PATH '$.cantidad',
            descuento  DECIMAL(5,2) PATH '$.descuento')) j   -- NUEVO: columna descuento
    INNER JOIN producto p ON p.idProducto = j.idProducto;

    -- 5) Descontar stock (sin cambios)
    UPDATE producto p
    INNER JOIN JSON_TABLE(p_detalle, '$[*]' COLUMNS (
            idProducto INT PATH '$.idProducto',
            cantidad   INT PATH '$.cantidad')) j ON j.idProducto = p.idProducto
    SET p.stock_producto = p.stock_producto - j.cantidad;

    COMMIT;

    SELECT v_idVenta AS idVenta;
END$$

DELIMITER ;