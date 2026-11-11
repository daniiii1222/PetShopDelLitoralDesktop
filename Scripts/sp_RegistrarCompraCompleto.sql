
USE petshopdellitoral;

DROP PROCEDURE IF EXISTS sp_RegistrarCompraCompleta;

DELIMITER $$

CREATE PROCEDURE sp_RegistrarCompraCompleta(
    IN p_monto DECIMAL(12,2),
    IN p_fecha DATE,
    IN p_idUsuario INT,
    IN p_idProveedor INT,
    IN p_tipo VARCHAR(50),
    IN p_numero VARCHAR(50),
    IN p_observaciones TEXT,
    IN p_detalles LONGTEXT
)
BEGIN
    DECLARE v_idCompra INT;
    DECLARE v_totalDetalles INT DEFAULT 0;
    DECLARE v_productosDistintos INT DEFAULT 0;
    DECLARE v_productosNoEncontrados INT DEFAULT 0;
    DECLARE v_detallesInvalidos INT DEFAULT 0;
    DECLARE v_sumaDetalles DECIMAL(12,2) DEFAULT 0;

    -- Manejo de errores: deshacer la transacción
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        ROLLBACK;
        RESIGNAL;
    END;

    START TRANSACTION;

    -- Validar JSON
    IF p_detalles IS NULL
       OR JSON_VALID(p_detalles) = 0
       OR JSON_TYPE(p_detalles) <> 'ARRAY'
       OR JSON_LENGTH(p_detalles) = 0 THEN

        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'La compra no contiene detalles válidos.';
    END IF;

    -- Obtener cantidad de detalles y suma de subtotales
    SELECT
        COUNT(*),
        COUNT(DISTINCT j.idProducto),
        COALESCE(SUM(j.subtotal_compra), 0)
    INTO
        v_totalDetalles,
        v_productosDistintos,
        v_sumaDetalles
    FROM JSON_TABLE(
        p_detalles,
        '$[*]' COLUMNS (
            idProducto INT PATH '$.idProducto',
            cantidad INT PATH '$.cantidad',
            precio_unitario DECIMAL(12,2) PATH '$.precio_unitario',
            subtotal_compra DECIMAL(12,2) PATH '$.subtotal_compra'
        )
    ) AS j;

    -- Validar que haya detalles
    IF v_totalDetalles = 0 THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'La compra no contiene productos.';
    END IF;

    -- Evitar que un mismo producto aparezca repetido
    IF v_totalDetalles <> v_productosDistintos THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Hay productos repetidos en los detalles de la compra.';
    END IF;

    -- Validar cantidad, precio y subtotal
    SELECT COUNT(*)
    INTO v_detallesInvalidos
    FROM JSON_TABLE(
        p_detalles,
        '$[*]' COLUMNS (
            idProducto INT PATH '$.idProducto',
            cantidad INT PATH '$.cantidad',
            precio_unitario DECIMAL(12,2) PATH '$.precio_unitario',
            subtotal_compra DECIMAL(12,2) PATH '$.subtotal_compra'
        )
    ) AS j
    WHERE j.idProducto IS NULL
       OR j.idProducto <= 0
       OR j.cantidad IS NULL
       OR j.cantidad <= 0
       OR j.precio_unitario IS NULL
       OR j.precio_unitario <= 0
       OR j.subtotal_compra IS NULL
       OR j.subtotal_compra < 0
       OR j.subtotal_compra <> ROUND(
            j.cantidad * j.precio_unitario, 2
       );

    IF v_detallesInvalidos > 0 THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Hay cantidades, precios o subtotales inválidos.';
    END IF;

    -- Comprobar que todos los productos existan
    SELECT COUNT(*)
    INTO v_productosNoEncontrados
    FROM JSON_TABLE(
        p_detalles,
        '$[*]' COLUMNS (
            idProducto INT PATH '$.idProducto'
        )
    ) AS j
    LEFT JOIN producto p
        ON p.idProducto = j.idProducto
    WHERE p.idProducto IS NULL;

    IF v_productosNoEncontrados > 0 THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Uno o más productos no existen.';
    END IF;

    -- Comprobar que el monto coincida con los detalles
    IF p_monto IS NULL
       OR ABS(v_sumaDetalles - p_monto) > 0.01 THEN

        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'El monto de la compra no coincide con los detalles.';
    END IF;

    -- Registrar cabecera de compra
    INSERT INTO compra_proveedor(
        monto_compra,
        estado_compra,
        fecha_compra,
        idUsuario,
        idProveedor,
        tipo_comprobante,
        numero_comprobante,
        observaciones,
        fechaCreacion_compra
    )
    VALUES(
        p_monto,
        1,
        p_fecha,
        p_idUsuario,
        p_idProveedor,
        p_tipo,
        p_numero,
        p_observaciones,
        NOW()
    );

    SET v_idCompra = LAST_INSERT_ID();

    -- Registrar todos los detalles juntos
    INSERT INTO detalle_compra(
        cantidad,
        precio_unitario,
        subtotal_compra,
        idProducto,
        idCompra,
        fechaCreacion_detalleC
    )
    SELECT
        j.cantidad,
        j.precio_unitario,
        j.subtotal_compra,
        j.idProducto,
        v_idCompra,
        NOW()
    FROM JSON_TABLE(
        p_detalles,
        '$[*]' COLUMNS (
            idProducto INT PATH '$.idProducto',
            cantidad INT PATH '$.cantidad',
            precio_unitario DECIMAL(12,2) PATH '$.precio_unitario',
            subtotal_compra DECIMAL(12,2) PATH '$.subtotal_compra'
        )
    ) AS j;

    -- Actualizar stock y precio de costo de todos los productos
    UPDATE producto p
    INNER JOIN JSON_TABLE(
        p_detalles,
        '$[*]' COLUMNS (
            idProducto INT PATH '$.idProducto',
            cantidad INT PATH '$.cantidad',
            precio_unitario DECIMAL(12,2) PATH '$.precio_unitario'
        )
    ) AS j
        ON p.idProducto = j.idProducto
    SET
        p.stock_producto = p.stock_producto + j.cantidad,
        p.precioUnitario_producto = j.precio_unitario;

    COMMIT;

END$$

DELIMITER ;
