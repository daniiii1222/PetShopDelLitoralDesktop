
USE petshopdellitoral;

DROP PROCEDURE IF EXISTS sp_RegistrarClienteCompleto;

DELIMITER $$

CREATE PROCEDURE sp_RegistrarClienteCompleto(
    IN p_nombre    VARCHAR(100),
    IN p_apellido  VARCHAR(100),
    IN p_dni       VARCHAR(20),
    IN p_correo    VARCHAR(100),
    IN p_telefono  VARCHAR(30),
    IN p_direccion VARCHAR(150)
)
BEGIN
    DECLARE v_idPersona INT;

    -- Si ocurre cualquier error, se revierte la transacción
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        ROLLBACK;
        RESIGNAL;
    END;

    -- Comienza la transacción
    START TRANSACTION;

    -- Inserta la persona
    INSERT INTO persona (
        nombre_persona,
        apellido_persona,
        dni_persona,
        correo_persona,
        telefono_persona,
        direccion_persona,
        fechaCreacion_persona,
        estado_persona
    )
    VALUES (
        p_nombre,
        p_apellido,
        p_dni,
        p_correo,
        p_telefono,
        p_direccion,
        NOW(),
        1
    );

    -- Recupera el ID de la persona recién creada
    SET v_idPersona = LAST_INSERT_ID();

    -- Inserta el cliente asociado a esa persona
    INSERT INTO cliente (
        fechaCreacion_cliente,
        estado_cliente,
        idPersona
    )
    VALUES (
        NOW(),
        1,
        v_idPersona
    );

    -- Confirma ambas operaciones
    COMMIT;

END$$

DELIMITER ;
