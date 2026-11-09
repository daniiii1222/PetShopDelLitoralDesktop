CREATE PROCEDURE sp_ListarProveedores()
BEGIN
    SELECT
        p.idProveedor,
        p.estado_proveedor,
        per.idPersona,
        per.dni_persona,
        per.nombre_persona,
        per.apellido_persona,
        per.correo_persona,
        per.telefono_persona,
        per.direccion_persona
    FROM proveedor p
    INNER JOIN persona per
        ON p.id_persona = per.idPersona
    ORDER BY per.nombre_persona, per.apellido_persona;
END