# Microservicio de Gestión de Pedidos

Este proyecto implementa un microservicio RESTful desarrollado en .NET Core para la gestión de pedidos, incluyendo funcionalidades CRUD para pedidos y productos, con persistencia en SQL Server.

## Requisitos previos

Asegúrate de tener instalados los siguientes componentes en tu máquina:

- [.NET SDK 7.0](https://dotnet.microsoft.com/download/dotnet/7.0)
- [Docker](https://www.docker.com/)
- [Git](https://git-scm.com/)

## Clonar el repositorio

Clona este repositorio en tu máquina local:

```bash
git clone <URL_DEL_REPOSITORIO>
cd <NOMBRE_DEL_REPOSITORIO>
```

## Configurar el entorno de desarrollo

### Configuración de la base de datos
Este microservicio utiliza SQL Server como base de datos, ejecutado en un contenedor Docker.

1. **Verifica el archivo `docker-compose.yml`:**
   Asegúrate de que el archivo `docker-compose.yml` está configurado correctamente con los puertos y credenciales necesarias.

2. **Configuración en `appsettings.json`:**
   El archivo `appsettings.json` debe contener la cadena de conexión a SQL Server:

   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=sqlserver;Database=PedidoDb;User Id=sa;Password=YourStrongPassword123!;TrustServerCertificate=True"
     }
   }
   ```

   **Nota:** La configuración del servidor de base de datos está definida en el contenedor `sqlserver` especificado en `docker-compose.yml`.

### Migraciones de la base de datos

Ejecuta las migraciones automáticamente al iniciar la aplicación. Este proceso está configurado en el archivo `Program.cs` y se ejecuta al levantar el contenedor.

Si deseas aplicar manualmente las migraciones:

1. Genera las migraciones (si no existen):
   ```bash
   dotnet ef migrations add InitialCreate
   ```
2. Aplica las migraciones:
   ```bash
   dotnet ef database update
   ```

## Ejecutar la aplicación

### Usando Docker Compose

Levanta los contenedores de la aplicación y la base de datos:

```bash
docker-compose up --build
```

Esto iniciará:
- El microservicio escuchando en el puerto `9001`.
- SQL Server en el puerto `1433`.

Accede al microservicio en:
```
http://localhost:9001/api/
```

### Usando .NET CLI

Si prefieres ejecutar la aplicación localmente sin Docker:

1. Inicia la base de datos Docker manualmente:
   ```bash
   docker-compose up sqlserver
   ```

2. Ejecuta el proyecto:
   ```bash
   dotnet run
   ```

La aplicación estará disponible en el puerto configurado (por defecto, `http://localhost:5000`).

## Realizar peticiones al microservicio

### Endpoints disponibles

#### 1. **Pedidos**
- **GET /api/Pedido**
  Devuelve todos los pedidos.
  ```bash
  curl -X GET http://localhost:9001/api/Pedido
  ```

- **POST /api/Pedido**
  Crea un nuevo pedido.
  ```bash
  curl -X POST http://localhost:9001/api/Pedido \
    -H "Content-Type: application/json" \
    -d '{"Fecha": "2025-01-06", "ClienteId": 1}'
  ```

- **GET /api/Pedido/{id}**
  Devuelve un pedido por su ID.
  ```bash
  curl -X GET http://localhost:9001/api/Pedido/1
  ```

- **PUT /api/Pedido/{id}**
  Actualiza un pedido existente.
  ```bash
  curl -X PUT http://localhost:9001/api/Pedido/1 \
    -H "Content-Type: application/json" \
    -d '{"Fecha": "2025-01-07", "ClienteId": 2}'
  ```

- **DELETE /api/Pedido/{id}**
  Elimina un pedido.
  ```bash
  curl -X DELETE http://localhost:9001/api/Pedido/1
  ```

#### 2. **Productos**
- **GET /api/Producto**
  Devuelve todos los productos.
  ```bash
  curl -X GET http://localhost:9001/api/Producto
  ```

- **POST /api/Producto**
  Crea un nuevo producto.
  ```bash
  curl -X POST http://localhost:9001/api/Producto \
    -H "Content-Type: application/json" \
    -d '{"Nombre": "Producto A", "Precio": 100.00}'
  ```

- **GET /api/Producto/{id}**
  Devuelve un producto por su ID.
  ```bash
  curl -X GET http://localhost:9001/api/Producto/1
  ```

- **PUT /api/Producto/{id}**
  Actualiza un producto existente.
  ```bash
  curl -X PUT http://localhost:9001/api/Producto/1 \
    -H "Content-Type: application/json" \
    -d '{"Nombre": "Producto B", "Precio": 150.00}'
  ```

- **DELETE /api/Producto/{id}**
  Elimina un producto.
  ```bash
  curl -X DELETE http://localhost:9001/api/Producto/1
  ```

## Notas adicionales

- Si encuentras errores relacionados con el puerto `9001`, verifica que no esté ocupado y que esté correctamente mapeado en `docker-compose.yml`.
- Puedes modificar las configuraciones avanzadas en `appsettings.json` o mediante variables de entorno en `docker-compose.yml`.

