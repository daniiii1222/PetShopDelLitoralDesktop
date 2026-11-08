<div align="center">

# 🐾 Pet Shop del Litoral

### Sistema de gestión para pet shops: ventas, compras, productos y clientes en un solo lugar

<br>

![.NET Framework](https://img.shields.io/badge/.NET%20Framework-4.7.2-FDCC6E?style=for-the-badge&labelColor=211815&logo=dotnet&logoColor=FDCC6E)
![C#](https://img.shields.io/badge/C%23-WinForms-FDCC6E?style=for-the-badge&labelColor=211815&logo=csharp&logoColor=FDCC6E)
![MySQL](https://img.shields.io/badge/MySQL%20%2F%20MariaDB-Base%20de%20datos-FDCC6E?style=for-the-badge&labelColor=211815&logo=mysql&logoColor=FDCC6E)
![Guna UI2](https://img.shields.io/badge/UI-Guna.UI2-FDCC6E?style=for-the-badge&labelColor=211815)
![Estado](https://img.shields.io/badge/Estado-En%20desarrollo-FFFFA0?style=for-the-badge&labelColor=211815)

<br>

[Módulos](#-módulos) ·
[Arquitectura](#-arquitectura) ·
[Instalación](#-instalación) ·
[Roles](#-roles-y-permisos) ·
[Estructura](#-estructura-del-repositorio) ·
[Equipo y Git](#-flujo-de-trabajo-con-git)

</div>

<br>

---

## ✨ Sobre el proyecto

**Pet Shop del Litoral** es una aplicación de escritorio para Windows que digitaliza la operación diaria de un pet shop: registrar ventas con descuentos, controlar el stock de productos, administrar clientes, usuarios y proveedores, registrar compras y consultar reportes.

Está desarrollada en **C# con Windows Forms** sobre **.NET Framework 4.7.2**, usa **MySQL / MariaDB** como base de datos y se organiza en **arquitectura de tres capas** (Presentación, Negocio y Datos) más una capa de Entidades compartidas. Casi toda la comunicación con la base se hace mediante **procedimientos almacenados**.

---

## 🧩 Módulos

| Módulo | Qué permite hacer |
|---|---|
| 🔐 **Login** | Ingreso con DNI y contraseña. Valida que el usuario exista y esté activo, y guarda la sesión con su rol. |
| 🏠 **Inicio / Dashboard** | Tablero con las ventas del día, la cantidad de clientes activos y una tabla de actividad reciente con las últimas ventas. |
| 🛒 **Ventas** | Alta de ventas con búsqueda de productos, cliente por DNI, método de pago, **descuento por producto y por compra** y descuento de stock dentro de una transacción. Genera PDF. |
| 📦 **Compras** | Registro de compras a proveedores. |
| 🏷️ **Productos** | Alta, edición y baja lógica de productos, con filtro por categoría y buscador. |
| 👥 **Personas** | Gestión de **usuarios**, **clientes** y **proveedores** con filtros de activos e inactivos y baja lógica. |
| 📊 **Reportes** | Reporte de ventas por rango de fechas y por vendedor, con una vista para administrador y otra para vendedor. |
| 💾 **Backup** | Copias de seguridad de la base con historial; se pueden copiar a otra ubicación o eliminar. |

> Las bajas son **lógicas**: el registro queda marcado como inactivo en vez de borrarse, así se conserva el historial.

---

## 🏛️ Arquitectura

El proyecto separa responsabilidades en capas. Cada una solo conoce a la que tiene debajo: el proyecto de Presentación no tiene referencia al de Datos, así que el compilador impide saltarse la capa de Negocio.

```
┌──────────────────────────────────────────────────────────┐
│  PRESENTACIÓN   ·   PetShopDelLitoral/                   │
│  Login, Inicio, FrmDashboard, FrmVentas, FrmPersonas...  │
│  Lo que ve y usa la persona.                             │
└────────────────────────────┬─────────────────────────────┘
                             │
┌────────────────────────────▼─────────────────────────────┐
│  NEGOCIO        ·   Capa Negocio/    (CN_*)              │
│  Validaciones y reglas: ¿hay cliente?, ¿descuento 0-100? │
└────────────────────────────┬─────────────────────────────┘
                             │
┌────────────────────────────▼─────────────────────────────┐
│  DATOS          ·   Capa Datos/      (CD_*, ClsDataBase) │
│  Ejecuta procedimientos almacenados en MySQL/MariaDB.    │
└────────────────────────────┬─────────────────────────────┘
                             │
                    ┌────────▼────────┐
                    │  petshopdellitoral  │  (base de datos)
                    └─────────────────┘

  ENTIDAD · Capa Entidad/  →  Usuario, Persona, Rol, Cliente, Venta...
  (clases con datos que viajan entre todas las capas)
```

### Cómo viaja un dato (ejemplo: el Dashboard)

```
FrmDashboard  →  CN_Venta.ObtenerResumenDashboard()
              →  CD_Venta  →  ClsDataBase.EjecutarSelect()
              →  sp_ObtenerResumenDashboard  (MySQL)
```

La sesión del usuario logueado se guarda en `Sesion.UsuarioActual`, una propiedad estática que cualquier pantalla puede consultar (por ejemplo, para saber el rol).

**Manejo de errores:** los que afectan lo que la persona está haciendo se le muestran en un mensaje. Los no críticos (autocompletado, fuente, ajustes visuales) no frenan la pantalla y quedan en la ventana de salida de Visual Studio con `Debug.WriteLine`.

---

## Tecnologías

| Tecnología | Uso |
|---|---|
| **C# / .NET Framework 4.7.2** | Lenguaje y plataforma |
| **Windows Forms** | Interfaz de escritorio |
| **Guna.UI2.WinForms** `2.0.3.3` | Paneles redondeados, grillas y controles con estilo |
| **FontAwesome.Sharp** `6.6.0` | Íconos del menú y de las tarjetas |
| **MySql.Data** `26.7.0` | Conexión con MySQL / MariaDB |
| **iTextSharp** `5.5.13.3` | Generación de PDF |
| **Procedimientos almacenados** | Casi todo el acceso a datos (`CD_Categoria`, `CD_Compra` y `CD_Backup` usan consultas parametrizadas) |
| **MSTest** | Pruebas unitarias de las reglas de negocio |

---

## Identidad visual

La interfaz mantiene la misma paleta en todas las pantallas:

| Color | Hex | Dónde se usa |
|---|---|---|
| ![](https://img.shields.io/badge/-%20%20%20%20-211815?style=flat-square) Marrón | `#211815` | Barra superior, menú lateral, tarjetas del Dashboard y cabeceras de tabla |
| ![](https://img.shields.io/badge/-%20%20%20%20-FDCC6E?style=flat-square) Dorado | `#FDCC6E` | Textos e íconos sobre marrón, bordes de tarjetas y botones |
| ![](https://img.shields.io/badge/-%20%20%20%20-FFFFA0?style=flat-square) Crema | `#FFFFA0` | Fondo de las tarjetas |
| ![](https://img.shields.io/badge/-%20%20%20%20-FFFFC0?style=flat-square) Crema claro | `#FFFFC0` | Fondo de las pantallas |

---

## Instalación

### Requisitos

- **Windows** con **Visual Studio 2022 versión 17.10 o superior** (la solución usa el formato `.slnx`)
- Componente **Desarrollo de escritorio con .NET** y el paquete de destino **.NET Framework 4.7.2**
- **MariaDB 10.6 o superior** o **MySQL 8**, corriendo en `localhost:3306`. El procedimiento de ventas usa `JSON_TABLE` y los scripts usan `ADD COLUMN IF NOT EXISTS`, que las versiones más viejas de MariaDB no tienen. Algunas versiones de XAMPP traen una MariaDB más vieja: conviene verificar con `SELECT VERSION();`.
- Un cliente SQL (DBeaver, MySQL Workbench o la consola `mysql`)
- Acceso a NuGet para restaurar los paquetes

### Pasos

**1. Clonar el repositorio**

```bash
git clone <URL-DEL-REPOSITORIO>
cd PetShopDelLitoral
```

**2. Crear la base de datos**

La aplicación se conecta a una base llamada **`petshopdellitoral`**. Creala en tu servidor local:

```sql
CREATE DATABASE petshopdellitoral CHARACTER SET utf8mb4;
```

**3. Cargar las tablas y los datos**

Restaurá una copia de seguridad `.sql` generada por la propia aplicación (pantalla *Backup*, carpeta `Backups` junto al ejecutable) que el equipo comparte. Los scripts de `Scripts/` **no crean las tablas**: solo agregan columnas y procedimientos sobre una base que ya las tiene.

**4. Ejecutar los scripts**

Ejecutá los archivos de [`Scripts/`](Scripts/) **como script completo** (en DBeaver: `Alt+X`) para que se respete el `DELIMITER`, en este orden:

| Orden | Archivo | Qué hace |
|---|---|---|
| 1 | `Ventas.sql` | Columnas de descuento y procedimientos de ventas (`sp_RegistrarVentaCompleta`, clientes por DNI, métodos de pago). Se puede ejecutar varias veces. |
| 2 | `AgregarCodigoProducto.sql` | Agrega la columna `codigo_producto` (ej. `PROD2090`) y actualiza la búsqueda de productos. |
| 3 | `ReporteVenta.sql` | `sp_Reporte_Ventas` (reporte por fechas y vendedor). |
| 4 | `ResumenDashboard.sql` | Resumen y ventas recientes del dashboard. |
| 5 | `SP_detalleVenta.sql` | Detalle de una venta (botón **Ver Detalle** de Reportes). |
| 6 | `SP_obtenerProductosMasVendidos.sql` | Productos más vendidos (Estadísticas). |
| 7 | `Proveedores.sql` | Alta, modificación y baja lógica de proveedores (con transacción). |
| 8 | `Productos.sql` | ABM de productos (`sp_ListarProductos`, `sp_RegistrarProducto`, `sp_ModificarProducto`, `sp_EliminarProducto`, `sp_ExisteCodigoProducto`). |
| 9 | `sp_ListarRoles.sql`, `sp_ListarUsuarios.sql`, `sp_LoginUsuario.sql`, `sp_RegistrarUsuarioCompleto.sql`, `sp_ModificarUsuarioCompleto.sql`, `sp_EliminarUsuario.sql` | Usuarios, roles e inicio de sesión. |
| 10 | `sp_ListarClientes.sql`, `sp_RegistrarClienteCompleto.sql`, `sp_ModificarClienteCompleto.sql`, `sp_EliminarCliente.sql` | Alta, modificación y baja lógica de clientes. |
| 11 | `sp_ObtenerPersonaPorDni.sql` | Búsqueda de una persona por DNI (pantalla Personas). |
| 12 | `sp_ObtenerVentasPorVendedor.sql` | Últimas ventas de un vendedor. |
| 13 | `DatosEjemplo_Proveedores.sql` | Dos proveedores de ejemplo (opcional, solo para probar Compras). |

Los archivos `sp_*.sql` (filas 9 a 12) definen un procedimiento cada uno. Ya vienen incluidos en la copia de seguridad del equipo, así que ejecutarlos es opcional, pero es seguro: cada uno elimina y vuelve a crear su procedimiento.

> ⚠️ **Importante:** cada integrante debe correr los scripts en **su propia base local**.

**5. Revisar la conexión**

La cadena de conexión está en [`PetShopDelLitoral/App.config`](PetShopDelLitoral/App.config):

```xml
<connectionStrings>
  <add name="conexion" connectionString="Server=localhost;Database=petshopdellitoral;Uid=root;Pwd= ;" providerName="MySql.Data.MySqlClient" />
</connectionStrings>
```

Si tu servidor usa otro usuario o contraseña, cambialo ahí, sin tocar el código. Si falta esa entrada, `ClsDataBase` usa `localhost` / `root` sin contraseña.

**6. Abrir y ejecutar**

1. Abrí `PetShopCapaPresentacion.slnx` en Visual Studio.
2. Esperá a que se restauren los paquetes NuGet.
3. Elegí como proyecto de inicio **CapaPresentacion** y presioná <kbd>F5</kbd>.


## 👤 Roles y permisos

El menú lateral cambia según el rol del usuario logueado.

| Módulo | Administrador | Supervisor | Vendedor |
|---|:---:|:---:|:---:|
| Inicio | ✅ | ✅ | ✅ |
| Ventas | ✅ | — | ✅ |
| Compras | ✅ | ✅ | — |
| Productos | ✅ | ✅ | ✅ |
| Personas | ✅ | — | ✅ |
| Reportes | ✅ | ✅ | ✅ |
| Backup | ✅ | — | — |

Dentro de **Personas**, cada rol ve distintos tipos de registro: el administrador gestiona usuarios, clientes y proveedores; el vendedor, solo clientes.



---

## 📁 Estructura del repositorio

```
PetShopDelLitoral/
├── 📂 Capa Entidad/        Clases de datos compartidas (Usuario, Persona, Rol...)
├── 📂 Capa Datos/          Acceso a la base
│   ├── Database/           ClsDataBase: conexión y ejecución de procedimientos
│   └── CD_*.cs             Una clase por módulo (CD_Usuario, CD_Venta...)
├── 📂 Capa Negocio/        Validaciones y reglas (CN_Usuario, CN_Venta...)
├── 📂 PetShopDelLitoral/   Capa de presentación (formularios WinForms)
│   ├── Login, Inicio       Acceso y ventana principal con menú
│   ├── FrmDashboard        Tablero de inicio
│   ├── FrmVentas, FrmCompras, FrmProductos, FrmPersonas
│   ├── FrmReportes*        Reportes de administrador y de vendedor
│   ├── FrmBackup           Copias de seguridad
│   └── Sesion.cs           Usuario logueado
├── 📂 Scripts/             Procedimientos almacenados y cambios de esquema (.sql)
└── 📄 PetShopCapaPresentacion.slnx
```

---

##  Cómo está armada la interfaz

Un detalle de diseño que conviene conocer antes de tocar las pantallas:

- **`Inicio`** es la ventana principal. Tiene el menú lateral y un panel central donde se muestran las demás pantallas: cada formulario se incrusta ahí con `Dock = Fill`.
- Por eso, el **tamaño real** de cada pantalla lo decide el panel central y no el tamaño que tiene en el diseñador. Las pantallas deben adaptarse con `Dock`, `Anchor` o `TableLayoutPanel` y no con posiciones fijas en píxeles.
- El **Dashboard** usa un `TableLayoutPanel` (dos tarjetas arriba y la actividad reciente abajo) justamente para evitar superposiciones.
- **Regla práctica:** lo visual y estático (colores, columnas, tamaños) va en el archivo `.Designer.cs`; lo dinámico (datos y eventos) va en el `.cs`. 

---



## Flujo de trabajo con Git

- Cada integrante trabaja en su rama (`feature/...`) y no se sube directo a `master`.
- Antes de subir: `git pull` de la rama en la que se trabaja.
- Los commits llevan solo lo que dice su mensaje: agregar los archivos por nombre y no con `git add .`.
- No versionar material de cátedra ni datos sensibles (por ejemplo guías en PDF), contraseñas reales ni copias de la base con datos personales.

---

<div align="center">

**Pet Shop del Litoral** · Hecho con 🧡 por el equipo de desarrollo

</div>
