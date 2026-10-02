-- Backup 27/9/2026 09:35:44
DROP TABLE IF EXISTS `categoria`;
CREATE TABLE `categoria` (
  `idCategoria` int(11) NOT NULL AUTO_INCREMENT,
  `nombre_categoria` varchar(50) DEFAULT NULL,
  `estado_categoria` bit(1) DEFAULT NULL,
  `fechaCreacion_categoria` datetime DEFAULT current_timestamp(),
  PRIMARY KEY (`idCategoria`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_uca1400_ai_ci;


DROP TABLE IF EXISTS `cliente`;
CREATE TABLE `cliente` (
  `idCliente` int(11) NOT NULL AUTO_INCREMENT,
  `fechaCreacion_cliente` datetime DEFAULT current_timestamp(),
  `estado_cliente` bit(1) DEFAULT NULL,
  `idPersona` int(11) DEFAULT NULL,
  PRIMARY KEY (`idCliente`),
  KEY `fk_cliente_persona` (`idPersona`),
  CONSTRAINT `fk_cliente_persona` FOREIGN KEY (`idPersona`) REFERENCES `persona` (`idPersona`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_uca1400_ai_ci;


DROP TABLE IF EXISTS `compra_proveedor`;
CREATE TABLE `compra_proveedor` (
  `idCompra` int(11) NOT NULL AUTO_INCREMENT,
  `monto_compra` decimal(10,2) DEFAULT NULL,
  `estado_compra` bit(1) DEFAULT NULL,
  `fecha_compra` date DEFAULT NULL,
  `idUsuario` int(11) NOT NULL,
  `idProveedor` int(11) NOT NULL,
  `fechaCreacion_compra` datetime DEFAULT current_timestamp(),
  PRIMARY KEY (`idCompra`),
  KEY `fk_compra_usuario` (`idUsuario`),
  KEY `fk_compra_proveedor` (`idProveedor`),
  CONSTRAINT `fk_compra_proveedor` FOREIGN KEY (`idProveedor`) REFERENCES `proveedor` (`idProveedor`),
  CONSTRAINT `fk_compra_usuario` FOREIGN KEY (`idUsuario`) REFERENCES `usuario` (`idUsuario`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_uca1400_ai_ci;


DROP TABLE IF EXISTS `detalle_compra`;
CREATE TABLE `detalle_compra` (
  `idDetalleCompra` int(11) NOT NULL AUTO_INCREMENT,
  `cantidad` int(11) DEFAULT NULL,
  `precio_unitario` decimal(10,2) DEFAULT NULL,
  `subtotal_compra` decimal(10,2) DEFAULT NULL,
  `idProducto` int(11) DEFAULT NULL,
  `idCompra` int(11) DEFAULT NULL,
  `fechaCreacion_detalleC` datetime DEFAULT current_timestamp(),
  PRIMARY KEY (`idDetalleCompra`),
  KEY `idProducto` (`idProducto`),
  KEY `idCompra` (`idCompra`),
  CONSTRAINT `1` FOREIGN KEY (`idProducto`) REFERENCES `producto` (`idProducto`),
  CONSTRAINT `2` FOREIGN KEY (`idCompra`) REFERENCES `compra_proveedor` (`idCompra`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_uca1400_ai_ci;


DROP TABLE IF EXISTS `detalle_venta`;
CREATE TABLE `detalle_venta` (
  `idDetalleVenta` int(11) NOT NULL AUTO_INCREMENT,
  `cantidad` int(11) DEFAULT NULL,
  `precio` decimal(10,2) DEFAULT NULL,
  `idProducto` int(11) DEFAULT NULL,
  `idVenta` int(11) DEFAULT NULL,
  `fechaCreacion_detalleV` datetime DEFAULT current_timestamp(),
  `subtotal_venta` decimal(10,2) DEFAULT NULL,
  PRIMARY KEY (`idDetalleVenta`),
  KEY `idProducto` (`idProducto`),
  KEY `idVenta` (`idVenta`),
  CONSTRAINT `1` FOREIGN KEY (`idProducto`) REFERENCES `producto` (`idProducto`),
  CONSTRAINT `2` FOREIGN KEY (`idVenta`) REFERENCES `venta` (`idVenta`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_uca1400_ai_ci;


DROP TABLE IF EXISTS `metodo_pago`;
CREATE TABLE `metodo_pago` (
  `idMetodoPago` int(11) NOT NULL AUTO_INCREMENT,
  `nombre_metodo` varchar(50) DEFAULT NULL,
  `estado_metodo` bit(1) DEFAULT NULL,
  `fechaCreacion_metodo` datetime DEFAULT current_timestamp(),
  PRIMARY KEY (`idMetodoPago`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_uca1400_ai_ci;


DROP TABLE IF EXISTS `persona`;
CREATE TABLE `persona` (
  `idPersona` int(11) NOT NULL AUTO_INCREMENT,
  `nombre_persona` varchar(50) NOT NULL,
  `apellido_persona` varchar(50) NOT NULL,
  `correo_persona` varchar(100) DEFAULT NULL,
  `telefono_persona` varchar(20) DEFAULT NULL,
  `direccion_persona` varchar(100) DEFAULT NULL,
  `fechaCreacion_persona` datetime DEFAULT current_timestamp(),
  `estado_persona` bit(1) DEFAULT NULL,
  `dni_persona` varchar(30) NOT NULL,
  PRIMARY KEY (`idPersona`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_uca1400_ai_ci;

INSERT INTO `persona` VALUES ('1', 'Carlos', 'Gomez', 'carlos@mail.com', '3794111111', 'Junin 100', '23/9/2026 14:39:07', '1', '11111111');
INSERT INTO `persona` VALUES ('2', 'Maria', 'Perez', 'maria@mail.com', '3794222222', 'Catamarca 200', '23/9/2026 14:39:07', '1', '22222222');
INSERT INTO `persona` VALUES ('3', 'Juan', 'Lopez', 'juan@mail.com', '3794333333', 'San Juan 300', '23/9/2026 14:39:07', '1', '33333333');
INSERT INTO `persona` VALUES ('4', 'Ana', 'Gomez', 'ana@gmail.com', '12345', '9 de Julio 1900', '23/9/2026 18:10:15', '1', '101010');

DROP TABLE IF EXISTS `producto`;
CREATE TABLE `producto` (
  `idProducto` int(11) NOT NULL AUTO_INCREMENT,
  `nombre_producto` varchar(50) DEFAULT NULL,
  `descripcion_producto` varchar(50) DEFAULT NULL,
  `estado_producto` bit(1) DEFAULT NULL,
  `precio_producto` decimal(10,2) DEFAULT 0.00,
  `stock_producto` int(11) NOT NULL DEFAULT 0,
  `idCategoria` int(11) NOT NULL,
  `fechaCreacion_producto` datetime DEFAULT current_timestamp(),
  `precioUnitario_producto` varchar(100) DEFAULT NULL,
  `stock_minimo` int(11) DEFAULT 0,
  PRIMARY KEY (`idProducto`),
  KEY `idCategoria` (`idCategoria`),
  CONSTRAINT `1` FOREIGN KEY (`idCategoria`) REFERENCES `categoria` (`idCategoria`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_uca1400_ai_ci;


DROP TABLE IF EXISTS `proveedor`;
CREATE TABLE `proveedor` (
  `idProveedor` int(11) NOT NULL AUTO_INCREMENT,
  `estado_proveedor` bit(1) DEFAULT NULL,
  `fechaCreacion_proveedor` datetime DEFAULT current_timestamp(),
  `id_persona` int(11) DEFAULT NULL,
  PRIMARY KEY (`idProveedor`),
  KEY `fk_proveedor_persona` (`id_persona`),
  CONSTRAINT `fk_proveedor_persona` FOREIGN KEY (`id_persona`) REFERENCES `persona` (`idPersona`) ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_uca1400_ai_ci;


DROP TABLE IF EXISTS `rol`;
CREATE TABLE `rol` (
  `idRol` int(11) NOT NULL AUTO_INCREMENT,
  `descripcion_rol` varchar(50) DEFAULT NULL,
  `fechaCreacion_rol` datetime DEFAULT current_timestamp(),
  PRIMARY KEY (`idRol`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_uca1400_ai_ci;

INSERT INTO `rol` VALUES ('1', 'Vendedor', '10/9/2026 20:57:39');
INSERT INTO `rol` VALUES ('2', 'Supervisor', '10/9/2026 20:57:39');
INSERT INTO `rol` VALUES ('3', 'Administrador', '10/9/2026 20:57:39');

DROP TABLE IF EXISTS `usuario`;
CREATE TABLE `usuario` (
  `idUsuario` int(11) NOT NULL AUTO_INCREMENT,
  `contrasenia_usuario` varchar(100) DEFAULT NULL,
  `estado_usuario` bit(1) DEFAULT NULL,
  `idRol` int(11) NOT NULL,
  `fechaCreacion_usuario` datetime DEFAULT current_timestamp(),
  `idPersona` int(11) DEFAULT NULL,
  PRIMARY KEY (`idUsuario`),
  KEY `idRol` (`idRol`),
  KEY `fk_usuario_persona` (`idPersona`),
  CONSTRAINT `1` FOREIGN KEY (`idRol`) REFERENCES `rol` (`idRol`),
  CONSTRAINT `fk_usuario_persona` FOREIGN KEY (`idPersona`) REFERENCES `persona` (`idPersona`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_uca1400_ai_ci;

INSERT INTO `usuario` VALUES ('1', '1234', '1', '1', '23/9/2026 14:39:07', '1');
INSERT INTO `usuario` VALUES ('2', '12345', '1', '2', '23/9/2026 14:39:07', '2');
INSERT INTO `usuario` VALUES ('3', '123456', '1', '3', '23/9/2026 14:39:07', '3');
INSERT INTO `usuario` VALUES ('4', '191919', '1', '1', '23/9/2026 18:10:15', '4');

DROP TABLE IF EXISTS `venta`;
CREATE TABLE `venta` (
  `idVenta` int(11) NOT NULL AUTO_INCREMENT,
  `total_venta` decimal(10,2) DEFAULT NULL,
  `fecha_venta` date DEFAULT NULL,
  `idUsuario` int(11) DEFAULT NULL,
  `idCliente` int(11) DEFAULT NULL,
  `idMetodoPago` int(11) DEFAULT NULL,
  `fechaCreacion_venta` datetime DEFAULT current_timestamp(),
  `estado_venta` bit(1) DEFAULT NULL,
  PRIMARY KEY (`idVenta`),
  KEY `idUsuario` (`idUsuario`),
  KEY `idCliente` (`idCliente`),
  KEY `idMetodoPago` (`idMetodoPago`),
  CONSTRAINT `1` FOREIGN KEY (`idUsuario`) REFERENCES `usuario` (`idUsuario`),
  CONSTRAINT `2` FOREIGN KEY (`idCliente`) REFERENCES `cliente` (`idCliente`),
  CONSTRAINT `3` FOREIGN KEY (`idMetodoPago`) REFERENCES `metodo_pago` (`idMetodoPago`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_uca1400_ai_ci;



-- ================================
-- STORED PROCEDURES
-- ================================
DELIMITER $$

DROP PROCEDURE IF EXISTS `sp_EliminarUsuario`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_EliminarUsuario`(
    IN p_idUsuario INT
)
BEGIN
    UPDATE usuario 
    SET estado_usuario = 0 
    WHERE idUsuario = p_idUsuario;
END$$

DROP PROCEDURE IF EXISTS `sp_ListarRoles`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_ListarRoles`()
BEGIN
    SELECT idRol, descripcion_rol FROM rol;
END$$

DROP PROCEDURE IF EXISTS `sp_ListarUsuarios`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_ListarUsuarios`()
BEGIN
    SELECT 
        u.idUsuario AS idUsuario,
        p.idPersona AS idPersona,
        p.nombre_persona AS nombre_persona,
        p.apellido_persona AS apellido_persona,
        p.dni_persona AS dni_persona,
        p.correo_persona AS correo_persona,
        p.telefono_persona AS telefono_persona,
        p.direccion_persona AS direccion_persona,
        r.idRol AS idRol,
        r.descripcion_rol AS descripcion_rol,
        u.estado_usuario AS estado_usuario
    FROM usuario u
    INNER JOIN persona p ON u.idPersona = p.idPersona
    INNER JOIN rol r ON u.idRol = r.idRol;
END$$

DROP PROCEDURE IF EXISTS `sp_LoginUsuario`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_LoginUsuario`(
    IN p_dni VARCHAR(20)
)
BEGIN
    SELECT u.idUsuario, u.contrasenia_usuario, u.estado_usuario,
           p.idPersona, p.nombre_persona, p.apellido_persona,
           r.idRol, r.descripcion_rol
    FROM usuario u
    INNER JOIN persona p ON u.idPersona = p.idPersona
    INNER JOIN rol r ON u.idRol = r.idRol
    WHERE p.dni_persona = p_dni;
END$$

DROP PROCEDURE IF EXISTS `sp_ModificarUsuarioCompleto`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_ModificarUsuarioCompleto`(
    IN p_idUsuario INT,
    IN p_nombre VARCHAR(100),
    IN p_apellido VARCHAR(100),
    IN p_dni VARCHAR(20),
    IN p_correo VARCHAR(100),
    IN p_telefono VARCHAR(20),
    IN p_direccion VARCHAR(200),
    IN p_idRol INT,
    IN p_contrasena VARCHAR(100),
    IN p_estado TINYINT(1) 
)
BEGIN
    DECLARE v_idPersona INT;

    -- Buscamos cuál es el idPersona
    SELECT idPersona INTO v_idPersona FROM usuario WHERE idUsuario = p_idUsuario;

    -- Actualizamos tabla persona
    UPDATE persona 
    SET 
        nombre_persona = p_nombre,
        apellido_persona = p_apellido,
        dni_persona = p_dni,
        correo_persona = p_correo,
        telefono_persona = p_telefono,
        direccion_persona = p_direccion
    WHERE idPersona = v_idPersona;

    -- Actualizamos tabla usuario
    IF p_contrasena IS NULL OR p_contrasena = '' THEN
        UPDATE usuario 
        SET 
            idRol = p_idRol,
            estado_usuario = p_estado
        WHERE idUsuario = p_idUsuario;
    ELSE
        UPDATE usuario 
        SET 
            idRol = p_idRol,
            estado_usuario = p_estado,
            contrasenia_usuario = p_contrasena
        WHERE idUsuario = p_idUsuario;
    END IF;

END$$

DROP PROCEDURE IF EXISTS `sp_ObtenerPersonaPorDni`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_ObtenerPersonaPorDni`(IN _dni_persona VARCHAR(20))
BEGIN
    SELECT 
        idPersona, 
        nombre_persona, 
        apellido_persona, 
        dni_persona, 
        correo_persona, 
        telefono_persona, 
        direccion_persona, 
        estado_persona
    FROM persona
    WHERE dni_persona = _dni_persona;
END$$

DROP PROCEDURE IF EXISTS `sp_RegistrarUsuarioCompleto`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_RegistrarUsuarioCompleto`(
    IN p_nombre VARCHAR(100),
    IN p_apellido VARCHAR(100),
    IN p_dni VARCHAR(20),
    IN p_correo VARCHAR(100),
    IN p_telefono VARCHAR(50),
    IN p_direccion VARCHAR(255),
    IN p_contrasena VARCHAR(100),
    IN p_idRol INT
)
BEGIN
    DECLARE v_idPersona INT;

    INSERT INTO persona (nombre_persona, apellido_persona, dni_persona, correo_persona, telefono_persona, direccion_persona, estado_persona)
    VALUES (p_nombre, p_apellido, p_dni, p_correo, p_telefono, p_direccion, 1);

    SET v_idPersona = LAST_INSERT_ID();

    INSERT INTO usuario (contrasenia_usuario, estado_usuario, idRol, idPersona)
    VALUES (p_contrasena, 1, p_idRol, v_idPersona);
END$$

DELIMITER ;
