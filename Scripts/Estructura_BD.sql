-- ============================================================
-- Pet Shop del Litoral
-- Estructura de la base de datos (sin datos)
-- Ejecutar este archivo para crear la base desde cero.
-- ============================================================

CREATE DATABASE IF NOT EXISTS `petshopdellitoral`
  DEFAULT CHARACTER SET utf8mb4
  COLLATE utf8mb4_uca1400_ai_ci;

USE `petshopdellitoral`;

-- ------------------------------------------------------------
-- Tablas independientes
-- ------------------------------------------------------------

CREATE TABLE `categoria` (
  `idCategoria` int(11) NOT NULL AUTO_INCREMENT,
  `nombre_categoria` varchar(50) DEFAULT NULL,
  `estado_categoria` bit(1) DEFAULT NULL,
  `fechaCreacion_categoria` datetime DEFAULT current_timestamp(),
  PRIMARY KEY (`idCategoria`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_uca1400_ai_ci;

CREATE TABLE `metodo_pago` (
  `idMetodoPago` int(11) NOT NULL AUTO_INCREMENT,
  `nombre_metodo` varchar(50) DEFAULT NULL,
  `estado_metodo` bit(1) DEFAULT NULL,
  `fechaCreacion_metodo` datetime DEFAULT current_timestamp(),
  PRIMARY KEY (`idMetodoPago`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_uca1400_ai_ci;

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
  PRIMARY KEY (`idPersona`),
  UNIQUE KEY `uq_persona_dni` (`dni_persona`),
  UNIQUE KEY `uq_persona_correo` (`correo_persona`),
  UNIQUE KEY `uq_persona_telefono` (`telefono_persona`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_uca1400_ai_ci;

CREATE TABLE `rol` (
  `idRol` int(11) NOT NULL AUTO_INCREMENT,
  `descripcion_rol` varchar(50) DEFAULT NULL,
  `fechaCreacion_rol` datetime DEFAULT current_timestamp(),
  PRIMARY KEY (`idRol`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_uca1400_ai_ci;

-- ------------------------------------------------------------
-- Tablas dependientes
-- ------------------------------------------------------------

CREATE TABLE `cliente` (
  `idCliente` int(11) NOT NULL AUTO_INCREMENT,
  `fechaCreacion_cliente` datetime DEFAULT current_timestamp(),
  `estado_cliente` bit(1) DEFAULT NULL,
  `idPersona` int(11) DEFAULT NULL,
  PRIMARY KEY (`idCliente`),
  KEY `fk_cliente_persona` (`idPersona`),
  CONSTRAINT `fk_cliente_persona`
    FOREIGN KEY (`idPersona`) REFERENCES `persona` (`idPersona`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_uca1400_ai_ci;

CREATE TABLE `producto` (
  `idProducto` int(11) NOT NULL AUTO_INCREMENT,
  `codigo_producto` varchar(50) DEFAULT NULL,
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
  CONSTRAINT `fk_producto_categoria`
    FOREIGN KEY (`idCategoria`) REFERENCES `categoria` (`idCategoria`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_uca1400_ai_ci;

CREATE TABLE `proveedor` (
  `idProveedor` int(11) NOT NULL AUTO_INCREMENT,
  `estado_proveedor` bit(1) DEFAULT NULL,
  `fechaCreacion_proveedor` datetime DEFAULT current_timestamp(),
  `id_persona` int(11) DEFAULT NULL,
  PRIMARY KEY (`idProveedor`),
  KEY `fk_proveedor_persona` (`id_persona`),
  CONSTRAINT `fk_proveedor_persona`
    FOREIGN KEY (`id_persona`) REFERENCES `persona` (`idPersona`) ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_uca1400_ai_ci;

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
  CONSTRAINT `fk_usuario_rol`
    FOREIGN KEY (`idRol`) REFERENCES `rol` (`idRol`),
  CONSTRAINT `fk_usuario_persona`
    FOREIGN KEY (`idPersona`) REFERENCES `persona` (`idPersona`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_uca1400_ai_ci;

CREATE TABLE `venta` (
  `idVenta` int(11) NOT NULL AUTO_INCREMENT,
  `total_venta` decimal(10,2) DEFAULT NULL,
  `fecha_venta` date DEFAULT NULL,
  `idUsuario` int(11) DEFAULT NULL,
  `idCliente` int(11) DEFAULT NULL,
  `idMetodoPago` int(11) DEFAULT NULL,
  `fechaCreacion_venta` datetime DEFAULT current_timestamp(),
  `estado_venta` bit(1) DEFAULT NULL,
  `descuento_venta` decimal(5,2) NOT NULL DEFAULT 0.00,
  PRIMARY KEY (`idVenta`),
  KEY `idUsuario` (`idUsuario`),
  KEY `idCliente` (`idCliente`),
  KEY `idMetodoPago` (`idMetodoPago`),
  CONSTRAINT `fk_venta_usuario`
    FOREIGN KEY (`idUsuario`) REFERENCES `usuario` (`idUsuario`),
  CONSTRAINT `fk_venta_cliente`
    FOREIGN KEY (`idCliente`) REFERENCES `cliente` (`idCliente`),
  CONSTRAINT `fk_venta_metodo_pago`
    FOREIGN KEY (`idMetodoPago`) REFERENCES `metodo_pago` (`idMetodoPago`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_uca1400_ai_ci;

CREATE TABLE `compra_proveedor` (
  `idCompra` int(11) NOT NULL AUTO_INCREMENT,
  `monto_compra` decimal(10,2) DEFAULT NULL,
  `estado_compra` bit(1) DEFAULT NULL,
  `fecha_compra` date DEFAULT NULL,
  `idUsuario` int(11) NOT NULL,
  `idProveedor` int(11) NOT NULL,
  `fechaCreacion_compra` datetime DEFAULT current_timestamp(),
  `tipo_comprobante` varchar(20) DEFAULT NULL,
  `numero_comprobante` varchar(30) DEFAULT NULL,
  `observaciones` varchar(200) DEFAULT NULL,
  `idMetodoPago` int(11) DEFAULT NULL,
  PRIMARY KEY (`idCompra`),
  KEY `fk_compra_usuario` (`idUsuario`),
  KEY `fk_compra_proveedor` (`idProveedor`),
  CONSTRAINT `fk_compra_proveedor`
    FOREIGN KEY (`idProveedor`) REFERENCES `proveedor` (`idProveedor`),
  CONSTRAINT `fk_compra_usuario`
    FOREIGN KEY (`idUsuario`) REFERENCES `usuario` (`idUsuario`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_uca1400_ai_ci;

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
  CONSTRAINT `fk_detalle_compra_producto`
    FOREIGN KEY (`idProducto`) REFERENCES `producto` (`idProducto`),
  CONSTRAINT `fk_detalle_compra_compra`
    FOREIGN KEY (`idCompra`) REFERENCES `compra_proveedor` (`idCompra`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_uca1400_ai_ci;

CREATE TABLE `detalle_venta` (
  `idDetalleVenta` int(11) NOT NULL AUTO_INCREMENT,
  `cantidad` int(11) DEFAULT NULL,
  `precio` decimal(10,2) DEFAULT NULL,
  `idProducto` int(11) DEFAULT NULL,
  `idVenta` int(11) DEFAULT NULL,
  `fechaCreacion_detalleV` datetime DEFAULT current_timestamp(),
  `subtotal_venta` decimal(10,2) DEFAULT NULL,
  `descuento_detalle` decimal(5,2) NOT NULL DEFAULT 0.00,
  PRIMARY KEY (`idDetalleVenta`),
  KEY `idProducto` (`idProducto`),
  KEY `idVenta` (`idVenta`),
  CONSTRAINT `fk_detalle_venta_producto`
    FOREIGN KEY (`idProducto`) REFERENCES `producto` (`idProducto`),
  CONSTRAINT `fk_detalle_venta_venta`
    FOREIGN KEY (`idVenta`) REFERENCES `venta` (`idVenta`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_uca1400_ai_ci;

-- ============================================================
-- Fin de la estructura. Este archivo no inserta registros.
-- ============================================================
