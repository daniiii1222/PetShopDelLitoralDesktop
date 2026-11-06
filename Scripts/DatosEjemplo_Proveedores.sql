-- ============================================================================
-- Proveedores iniciales de ejemplo (persona + proveedor)
-- ============================================================================

INSERT INTO persona(nombre_persona, apellido_persona, dni_persona, correo_persona, telefono_persona, direccion_persona, estado_persona, fechaCreacion_persona)
SELECT 'Distribuidora PetFood', 'Litoral', '30-11223344-5', 'ventas@petfood.com', '3794112233', 'Av. Libertad 1234', 1, NOW()
WHERE NOT EXISTS (SELECT 1 FROM persona WHERE dni_persona = '30-11223344-5');

INSERT INTO persona(nombre_persona, apellido_persona, dni_persona, correo_persona, telefono_persona, direccion_persona, estado_persona, fechaCreacion_persona)
SELECT 'Laboratorio VetSalud', 'Argentina', '30-55667788-9', 'contacto@vetsalud.com', '3794556677', 'Ruta 12 Km 5', 1, NOW()
WHERE NOT EXISTS (SELECT 1 FROM persona WHERE dni_persona = '30-55667788-9');

INSERT INTO proveedor(id_persona, estado_proveedor, fechaCreacion_proveedor)
SELECT idPersona, 1, NOW() FROM persona WHERE dni_persona IN ('30-11223344-5', '30-55667788-9')
AND idPersona NOT IN (SELECT id_persona FROM proveedor);
