# DemoAPI
Este proyecto permite desplegar un API Rest a través del C# Net Core

# Dependencias
Se deben instalar estos paquetes a través de la herramienta NuGet de Visual Studio .Net:

* Microsoft.EntityFrameworkCore v.7 
* Microsoft.EntityFrameworkCore.InMemory v.7
* Microsoft.EntityFrameworkCore.Relational v.7
* Microsoft.EntityFrameworkCore.SqlServer v.7
* Microsoft.EntityFrameworkCore.Tools v.7
* Microsoft.VisualStudio.Web.CodeGeneration.Design v.7
* Swashbuckle.AspNetCore v.6.2.3

# Componentes

## Controladores

### EmpleadosController.cs
Este archivo contiene el código que contiene los métodos del API para realizar el CRUD en la base de datos.

## Contexto de la base de datos
### DemoContext.cs
Este archivo contiene el código que hereda la clase *DbContext* del *Microsoft.EntityFrameworkCore* que abstrae las métodos necesarios para manipular los objetos de la base de datos

## Entidades
### Empleado.cs
Este archivo contiene la clase POJO para realizar la abstracción de la tabla Empleado de la base de datos.

## Configuración
### appsettings.json
Este archivo contiene la configuración de cadena de conexión del base de datos y comportamiento del servidor de aplicaciones.

