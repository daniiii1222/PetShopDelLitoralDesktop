-- Backup 23/9/2026 14:18:04
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
  `correo_cliente` varchar(100) DEFAULT NULL,
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
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_uca1400_ai_ci;

INSERT INTO `persona` VALUES ('1', 'Test', 'Admin', 'test@test.com', '123456789', 'Corrientes', '10/9/2026 21:30:08', '1', '12345678');
INSERT INTO `persona` VALUES ('2', 'Test', 'Admin', 'test@test.com', '123456789', 'Corrientes', '14/9/2026 08:39:54', '1', '908078');
INSERT INTO `persona` VALUES ('3', 'Virginia', 'Romero', 'vir@gmail.com', '6789', 'Buenos Aires 1200', '14/9/2026 08:53:11', '1', '45678');
INSERT INTO `persona` VALUES ('4', 'Ada', 'Lovelace', 'ada@gmail.com', '121314', 'Catamarca 1200', '14/9/2026 11:34:21', '1', '676869');
INSERT INTO `persona` VALUES ('5', 'Daniela', 'Recalde', 'danielaa@gmail.com', '98374934', 'Madariaga 1000', '14/9/2026 17:42:34', '1', '8374583');
INSERT INTO `persona` VALUES ('6', 'dani', 'rec', 'dani@gmail.com', '123456', 'Junin 1000', '14/9/2026 17:57:08', '1', '123456');

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
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_uca1400_ai_ci;

INSERT INTO `usuario` VALUES ('2', '1234', '1', '3', '10/9/2026 21:33:48', '1');
INSERT INTO `usuario` VALUES ('3', '12345', '1', '2', '14/9/2026 08:39:54', '2');
INSERT INTO `usuario` VALUES ('4', '1010', '1', '3', '14/9/2026 08:53:11', '3');
INSERT INTO `usuario` VALUES ('5', 'lovelace', '1', '3', '14/9/2026 11:34:21', '4');
INSERT INTO `usuario` VALUES ('6', '1212', '1', '1', '14/9/2026 17:42:34', '5');
INSERT INTO `usuario` VALUES ('7', '1212', '0', '2', '14/9/2026 17:57:08', '6');

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


