-- ================================================
-- Proveedores: alta, modificacion y baja logica
-- ================================================

USE petshopdellitoral;

DROP PROCEDURE IF EXISTS sp_RegistrarProveedorCompleto;
DROP PROCEDURE IF EXISTS sp_ModificarProveedorCompleto;
DROP PROCEDURE IF EXISTS sp_EliminarProveedor;

DELIMITER $$

-- ------------------------------------------------
-- 1) Registrar: crea la persona (o reutiliza la que ya tiene ese DNI) y el proveedor
-- ------------------------------------------------
CREATE PROCEDURE sp_RegistrarProveedorCompleto(
    IN p_nombre    VARCHAR(100),
    IN p_apellido  VARCHAR(100),
    IN p_dni       VARCHAR(20),
    IN p_correo    VARCHAR(100),
    IN p_telefono  VARCHAR(30),
    IN p_direccion VARCHAR(150)
)
BEGIN
    DECLARE v_idPersona   INT DEFAULT NULL;
    DECLARE v_idProveedor INT DEFAULT NULL;

    -- Si algo falla se deshace TODO y el error vuelve a la aplicacion
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        ROLLBACK;
        RESIGNAL;
    END;

    START TRANSACTION;

    SET v_idPersona = (SELECT idPersona FROM persona WHERE dni_persona = p_dni LIMIT 1);

    IF v_idPersona IS NULL THEN
        INSERT INTO persona(nombre_persona, apellido_persona, dni_persona, correo_persona,
                            telefono_persona, direccion_persona, estado_persona, fechaCreacion_persona)
        VALUES (p_nombre, p_apellido, p_dni, p_correo, p_telefono, p_direccion, 1, NOW());

        SET v_idPersona = LAST_INSERT_ID();
    ELSE
        -- La persona ya existe (por ejemplo es cliente): no puede ser proveedor dos veces
        IF EXISTS (SELECT 1 FROM proveedor WHERE id_persona = v_idPersona) THEN
            SIGNAL SQLSTATE '45000'
                SET MESSAGE_TEXT = 'Ya existe un proveedor registrado con ese DNI.';
        END IF;

        -- Se reutiliza la persona y se actualizan sus datos con los del formulario
        UPDATE persona
           SET nombre_persona    = p_nombre,
               apellido_persona  = p_apellido,
               correo_persona    = p_correo,
               telefono_persona  = p_telefono,
               direccion_persona = p_direccion,
               estado_persona    = 1
         WHERE idPersona = v_idPersona;
    END IF;

    INSERT INTO proveedor(id_persona, estado_proveedor, fechaCreacion_proveedor)
    VALUES (v_idPersona, 1, NOW());

    SET v_idProveedor = LAST_INSERT_ID();

    COMMIT;

    SELECT v_idProveedor AS idProveedor;
END$$

-- ------------------------------------------------
-- 2) Modificar: actualiza los datos de la persona y el estado del proveedor
-- ------------------------------------------------
CREATE PROCEDURE sp_ModificarProveedorCompleto(
    IN p_idProveedor INT,
    IN p_nombre      VARCHAR(100),
    IN p_apellido    VARCHAR(100),
    IN p_dni         VARCHAR(20),
    IN p_correo      VARCHAR(100),
    IN p_telefono    VARCHAR(30),
    IN p_direccion   VARCHAR(150),
    IN p_estado      TINYINT
)
BEGIN
    DECLARE v_idPersona INT DEFAULT NULL;

    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        ROLLBACK;
        RESIGNAL;
    END;

    START TRANSACTION;

    SET v_idPersona = (SELECT id_persona FROM proveedor WHERE idProveedor = p_idProveedor);

    IF v_idPersona IS NULL THEN
        SIGNAL SQLSTATE '45000'
            SET MESSAGE_TEXT = 'El proveedor que intenta modificar no existe.';
    END IF;

    -- El DNI no puede pertenecer a otra persona
    IF EXISTS (SELECT 1 FROM persona WHERE dni_persona = p_dni AND idPersona <> v_idPersona) THEN
        SIGNAL SQLSTATE '45000'
            SET MESSAGE_TEXT = 'Ya existe otra persona registrada con ese DNI.';
    END IF;

    UPDATE persona
       SET nombre_persona    = p_nombre,
           apellido_persona  = p_apellido,
           dni_persona       = p_dni,
           correo_persona    = p_correo,
           telefono_persona  = p_telefono,
           direccion_persona = p_direccion
     WHERE idPersona = v_idPersona;

    UPDATE proveedor
       SET estado_proveedor = p_estado
     WHERE idProveedor = p_idProveedor;

    COMMIT;
END$$

-- ------------------------------------------------
-- 3) Baja logica: el proveedor queda inactivo (no se borra, sus compras siguen existiendo)
-- ------------------------------------------------
CREATE PROCEDURE sp_EliminarProveedor(
    IN p_idProveedor INT
)
BEGIN
    IF NOT EXISTS (SELECT 1 FROM proveedor WHERE idProveedor = p_idProveedor) THEN
        SIGNAL SQLSTATE '45000'
            SET MESSAGE_TEXT = 'El proveedor que intenta dar de baja no existe.';
    END IF;

    UPDATE proveedor
       SET estado_proveedor = 0
     WHERE idProveedor = p_idProveedor;
END$$

DELIMITER ;
