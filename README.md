# E-commerce Project

## Descripción

Este es un proyecto de e-commerce que permite a los usuarios navegar y comprar productos. Hay dos roles principales: usuario y administrador. Los administradores tienen acceso a funcionalidades adicionales para gestionar el sitio.

## Características Principales

- **Usuarios**: Navegar, buscar productos, agregar productos al carrito y realizar compras.
- **Administradores**: Gestionar productos, visualizar estadísticas y gráficos de ventas.
- **Seguridad**: Autenticación y autorización basada en roles.

## Tecnologías Utilizadas

- **Lenguajes y Frameworks**:
  - C#
  - ASP.NET Core
  - ADO.NET
  - .NET Core
- **Base de Datos**:
  - SQL Server
- **Frontend**:
  - Bootstrap
  - Chart.js
- **IDE**:
  - Visual Studio

## Instalación

Para configurar el proyecto localmente, sigue estos pasos:

1. Clona el repositorio:
    ```sh
    git clone https://github.com/FrancoJack123/ecommerce-project.git
    ```
2. Configura la cadena de conexión a tu base de datos SQL Server en `appsettings.json`.
3. Restaura las dependencias y construye el proyecto:
    ```sh
    dotnet restore
    dotnet build
    ```
4. Aplica las migraciones a la base de datos:
    ```sh
    dotnet ef database update
    ```
5. Inicia la aplicación:
    ```sh
    dotnet run
    ```

## Uso

Puedes ver una demo en vivo del proyecto en [este enlace](http://nawijodevs.somee.com/).

### Credenciales de administrador

- **Correo electrónico**: admin@gmail.com
- **Contraseña**: hola

### Ejemplos de Uso

- **Usuarios**: Registrarse, iniciar sesión, buscar productos, agregar al carrito y realizar compras.
- **Administradores**: Iniciar sesión, gestionar inventario, visualizar estadísticas de ventas.

## Contribución

Las contribuciones son bienvenidas. Para contribuir, sigue estos pasos:

1. Haz un fork del proyecto.
2. Crea una nueva rama (`git checkout -b feature/nueva-caracteristica`).
3. Realiza los cambios necesarios y haz commits (`git commit -m 'Agrega nueva característica'`).
4. Envía tus cambios a tu repositorio fork (`git push origin feature/nueva-caracteristica`).
5. Crea un Pull Request.

## Licencia

Este proyecto está bajo la Licencia MIT. Consulta el archivo `LICENSE` para más detalles.

## Autor

Este proyecto fue creado por [Franco Jack](https://github.com/FrancoJack123).

## Contacto

Para preguntas o soporte, puedes contactar al autor a través de [GitHub](https://github.com/FrancoJack123).

