using DemoAPI.Datos;
using DemoAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProjectDemoAPI
{
    public class DemoContextMemoria
    {
        public DemoContext ObtenerContexto()
        {
            //Indicamos que utilizará base de datos en memoria
            //y que no deseamos que marque error si realizamos
            //transacciones en el código de nuestra aplicación
            var options = new DbContextOptionsBuilder<DemoContext>()
                          .ConfigureWarnings
                          (x => x.Ignore(InMemoryEventId
                                    .TransactionIgnoredWarning))
                          .UseInMemoryDatabase(databaseName: "TestCaduca")
                                   .Options;
            //Inicializamos la configuración de la base de datos
            var context = new DemoContext(options);
            //Mandamos llamar la función para inicializar los 
            //datos de prueba
            InicializaDatos.Inicializar(context);
            return context;
        }
    }
}
