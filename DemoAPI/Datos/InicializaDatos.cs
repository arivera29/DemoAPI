using DemoAPI.Models;

namespace DemoAPI.Datos
{
    /// <summary>
    /// Clase para inicializar los datos para las pruebas unitarias
    /// </summary>
    public class InicializaDatos
    {
        /// <summary>
        /// Metodo para crear un set de datos para las pruebas unitarias
        /// </summary>
        /// <param name="contexto"></param>
        public static void Inicializar(DemoContext contexto)
        {
            //Si no es base de datos en memoria no se agrega nada
            if (contexto.Database.ProviderName
                          != "Microsoft.EntityFrameworkCore.InMemory")
                return;
            //Te aseguras que la base de datos haya sido creada
            contexto.Database.EnsureCreated();

            List<Empleado> empleados = getTestEmpleados();

            foreach (var empleado in empleados)
            {
                contexto.Add(empleado);
            }



        }

        private static List<Empleado> getTestEmpleados()
        {
            var testEmpleados = new List<Empleado>();
            testEmpleados.Add(new Empleado { Id = 1, Nombre = "Aimer", Apellido = "Rivera Centeno", FechaNacimiento = new DateTime(1978, 11, 29), Foto = new Byte[10], EstadoCivil = 1, Hermanos = true });
            testEmpleados.Add(new Empleado { Id = 2, Nombre = "Maira", Apellido = "Escobar Mendoza", FechaNacimiento = new DateTime(1981, 6, 21), Foto = new Byte[10], EstadoCivil = 1, Hermanos = true });
            testEmpleados.Add(new Empleado { Id = 3, Nombre = "Alejandra", Apellido = "Rivera Escobar", FechaNacimiento = new DateTime(2006, 3, 2), Foto = new Byte[10], EstadoCivil = 0, Hermanos = true });
            testEmpleados.Add(new Empleado { Id = 4, Nombre = "Camila", Apellido = "Rivera Escobar", FechaNacimiento = new DateTime(2009, 9, 14), Foto = new Byte[10], EstadoCivil = 0, Hermanos = true });
            testEmpleados.Add(new Empleado { Id = 5, Nombre = "Salvatore", Apellido = "Rivera Escobar", FechaNacimiento = new DateTime(2016, 11, 29), Foto = new Byte[10], EstadoCivil = 0, Hermanos = true });
            return testEmpleados;
        }
    }
}