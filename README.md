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

## Configuración de la base de datos
Esta aplicación utiliza motor de base de datos MICROSOFT SQL SERVER.

### Levantar Servidor SQL Server con Docker
Para levantar un servidor del SQL Server en Linux a través de Docker debes realizar los siguiente pasos en Windows o Linux:
~~~
docker pull mcr.microsoft.com/mssql/server:2022-latest
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=<Password>" -p 1433:1433 --name sql1 --hostname sql1 -d mcr.microsoft.com/mssql/server:2022-latest
docker ps -a
~~~
Donde <Password> es la clave que se le va a asignar al usuasio SysAdmin (**sa**) de SQL Server

Luego desde un cliente de SQL Server, por ejemplo, Microsoft SQL Server Management Studio se conecta a la instancia de la base de datos y se crea la base de datos con el siguiente script:
~~~
CREATE DATABASE demo
GO
//Creamos un login para el usuario administrador
CREATE LOGIN AdminDemo WITH PASSWORD = 'StKRV6MR6A'
GO
//Creamos un login para el usuario de lectura
CREATE LOGIN demo WITH PASSWORD = 'Pantera.2341'
GO
//Cambiamos a la base de datos
USE demo
GO
//Creamos los usuarios administrador y de sistema
CREATE USER AdminDemo FOR LOGIN AdminDemo;
CREATE USER demo FOR LOGIN demo;
//Agregamos el permiso al usuario administrador de db_owner el cual 
//tiene acceso total a la base de datos
ALTER ROLE db_owner ADD MEMBER AdminDemo;
//Agregamos los roles de escritura y lectura para el 
// usuario de sistema
ALTER ROLE db_datareader ADD MEMBER demo;
ALTER ROLE db_datawriter ADD MEMBER demo;
~~~


### Configurar cadena de conexion a la base de datos desde la aplicación
Para configurar la cadena de conexión a la base de datos se debe modificar el archivo **appsettings.json** y agregar los datos de conexión a la base de datos:
~~~
"SQLServerConnection": "Server=localhost;Database=demo;User Id=demo;Password=Pantera.2341;TrustServerCertificate=True"
~~~
