# GiphyNet
API REST construida con .NET 8.0 con base de datos SQLServer


## Instalación
Clonamos el repositorio en una carpeta:
1. Abrimos Visual Studio 2022
2. Se da click en clonar proyecto
3. Se ingresa la sigueinte url https://github.com/LuisMiguelMesaGarcia/GiphyNet.git
4. Crear

## Base de datos
El proyecto tiene migraciones por lo cual se puede generar la estructura de la base de datos
  - Abre el archivo appsettings.json y configura tu instancia local de SQL Server:
   ```c#
  "ConnectionStrings": {
    "DefaultConnection": "Server=TU_SERVER_NAME;Database=GifDb;Trusted_Connection=True;TrustServerCertificate=True;"
  }
  ```
  - Reemplaza TU_SERVER_NAME por el nombre de tu servidor local de SQL Server.
  - Ejemplo común: "Server=localhost\\SQLEXPRESS;"
  - Tools --> NuGet Package Manager --> Package Manager Console
  - Herramientas --> Administrador de paquetes NuGet --> Consola del administrador de paquetes
  - En la consola escribimos 
   ```PowerShell
  Update-Database
  ```

## Configuración
En el archivo Program.cs, asegúrate de que la política de CORS (policy.WithOrigins(...)) permita la URL del frontend (por defecto es Angular en localhost:4200):
```c#
builder.Services.AddCors(options =>
{
    options.AddPolicy("MyCorsPolicy", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});
  ```
 
