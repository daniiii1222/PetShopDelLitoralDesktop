SELECT 'rol' t, COUNT(*) n FROM rol UNION ALL
SELECT 'persona', COUNT(*) FROM persona UNION ALL
SELECT 'usuario', COUNT(*) FROM usuario UNION ALL
SELECT 'cliente', COUNT(*) FROM cliente UNION ALL
SELECT 'proveedor', COUNT(*) FROM proveedor UNION ALL
SELECT 'metodo_pago', COUNT(*) FROM metodo_pago UNION ALL
SELECT 'categoria', COUNT(*) FROM categoria UNION ALL
SELECT 'producto', COUNT(*) FROM producto UNION ALL
SELECT 'compra_proveedor', COUNT(*) FROM compra_proveedor UNION ALL
SELECT 'detalle_compra', COUNT(*) FROM detalle_compra UNION ALL
SELECT 'venta', COUNT(*) FROM venta UNION ALL
SELECT 'detalle_venta', COUNT(*) FROM detalle_venta;