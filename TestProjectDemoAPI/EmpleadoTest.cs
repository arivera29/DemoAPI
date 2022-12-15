using DemoAPI.DAO;
using DemoAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProjectDemoAPI
{
    public class EmpleadoTest
    {
        DemoContext context;

        public EmpleadoTest()
        {
            context = new DemoContextMemoria().ObtenerContexto();

        }

        public async Task AgregarEmpleadoAsync()
        {
            var empleadoDAO = new EmpleadoDAO(context);
            var empleado = new Empleado { Id = 5, Nombre = "Salvatore", Apellido = "Rivera Escobar", FechaNacimiento = new DateTime(2016, 11, 29), Foto = new Byte[10], EstadoCivil = 0, Hermanos = true };


            var resp = await empleadoDAO.AgregarAsync(empleado);

            Assert.True(resp);
            
        }
    }
}
