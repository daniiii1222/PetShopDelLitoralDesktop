# Pet Shop del Litoral

Sistema de escritorio (Windows Forms, .NET Framework 4.7.2) para la gestión de un pet shop: ventas, compras a proveedores, productos, personas (clientes, proveedores y usuarios), reportes, estadísticas y copias de seguridad.

## Arquitectura en capas

```
Presentación  (PetShopDelLitoral)  ->  Negocio (Capa Negocio)  ->  Datos (Capa Datos)  ->  MySQL
                         \________________  Entidad (Capa Entidad)  ________________/
```

| Proyecto | Namespace | Responsabilidad |
|---|---|---|
| `PetShopDelLitoral` | `PetShopDelLitoral` | Pantallas (Forms). Solo habla con la capa de Negocio; no tiene SQL ni conexiones. |
| `Capa Negocio` | `Capa_Negocio` | Reglas y validaciones (`CN_*`). No conoce el motor de base de datos ni las pantallas. |
| `Capa Datos` | `Capa_Datos` | Acceso a MySQL (`CD_*`) con procedimientos almacenados. La conexión se crea siempre con `ClsDataBase.CrearConexion()`. |
| `Capa Entidad` | `Capa_Entidad` | Clases de datos compartidas (`Producto`, `Venta`, `Usuario`, ...). |
| `Pruebas` | `Pruebas` | Pruebas unitarias (MSTest) de las reglas de negocio. |

Los errores inesperados que la interfaz no muestra se anotan con `Bitacora.Registrar(...)` en `Logs\errores.log` (junto al ejecutable) en lugar de ignorarse.

## Requisitos

- Visual Studio 2022 o superior (carga de trabajo *Desarrollo de escritorio con .NET*).
- MySQL 8 (por ejemplo desde XAMPP) corriendo en `localhost:3306`.
- Un cliente SQL (DBeaver, MySQL Workbench o la consola `mysql`).

## Cómo preparar la base de datos

1. Crear la base de datos vacía:

   ```sql
   CREATE DATABASE petshopdellitoral CHARACTER SET utf8mb4;
   ```

2. Cargar las **tablas y datos**: restaurar una copia de seguridad `.sql` generada por la propia aplicación (pantalla *Backup*, carpeta `Backups` junto al ejecutable) que el equipo comparte.
   Los scripts de `Scripts/` **no crean las tablas**: solo agregan columnas y procedimientos sobre una base que ya las tiene.

3. Ejecutar los scripts de `Scripts/` **como script completo** (en DBeaver: `Alt+X`) para que se respete el `DELIMITER`, en este orden:

   | Orden | Archivo | Qué hace |
   |---|---|---|
   | 1 | `Ventas.sql` | Columnas de descuento y procedimientos de ventas (`sp_RegistrarVentaCompleta`, búsqueda de productos, clientes por DNI, métodos de pago). |
   | 2 | `AgregarCodigoProducto.sql` | Agrega la columna `codigo_producto` (ej. `PROD2090`) y actualiza la búsqueda de productos. |
   | 3 | `ReporteVenta.sql` | Procedimiento del reporte de ventas. |
   | 4 | `ResumenDashboard.sql` | Resumen y ventas recientes del dashboard. |
   | 5 | `SP_detalleVenta.sql` | Detalle de una venta (botón *Ver detalle* de Reportes). |
   | 6 | `SP_obtenerProductosMasVendidos.sql` | Productos más vendidos (Estadísticas). |
   | 7 | `Proveedores.sql` | Alta, modificación y baja lógica de proveedores (3 procedimientos, con transacción). |
   | 8 | `Productos.sql` | Procedimientos del ABM de productos (`sp_ListarProductos`, `sp_RegistrarProducto`, `sp_ModificarProducto`, `sp_EliminarProducto`, `sp_ExisteCodigoProducto`). |

   `Venta.sql` es una versión anterior del registro de ventas (procedimientos sueltos) y no hace falta ejecutarla.

## Configuración de la conexión

La cadena de conexión está en `PetShopDelLitoral/App.config`:

```xml
<connectionStrings>
  <add name="conexion" connectionString="Server=localhost;Database=petshopdellitoral;Uid=root;Pwd= ;" providerName="MySql.Data.MySqlClient" />
</connectionStrings>
```

Se cambia ahí (usuario, contraseña, servidor) sin tocar el código. Si falta esa entrada, `ClsDataBase` usa `localhost` / `root` sin contraseña.

## Ejecutar

1. Abrir `PetShopCapaPresentacion.slnx` en Visual Studio.
2. Establecer `CapaPresentacion` (PetShopDelLitoral) como proyecto de inicio y presionar **F5**.
3. Si falla la restauración de paquetes: *Herramientas → Administrador de paquetes NuGet → Restaurar*.

## Pruebas

Desde Visual Studio: *Prueba → Ejecutar todas las pruebas*. O por consola:

```
dotnet test Pruebas/Pruebas.csproj
```

Las pruebas actuales cubren validaciones de usuarios, productos y copias de seguridad, y **no necesitan base de datos**.

## Flujo de trabajo en Git

- Cada integrante trabaja en su rama (`feature/...`) y no se sube directo a `master`.
- Antes de subir: `git pull --rebase origin <rama>`.
- No versionar material de cátedra ni datos sensibles (por ejemplo la guía en PDF), contraseñas reales ni copias de la base con datos personales.
