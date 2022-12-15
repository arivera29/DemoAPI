using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace DemoAPI.Models
{
    /// <summary>
    /// Esta clase contiene el contexto del modelo de la base de datos
    /// </summary>
    /// <remarks>
    /// Es utilizada para realizar las migraciones de la base de datos a través 
    /// del EntityFramework y los controladores del API
    /// </remarks>
    public class DemoContext : DbContext
    {
        public DemoContext()
        {
        }

        /// <summary>
        /// Método constructor de la clase
        /// </summary>
        /// <param name="options">Parametro de tipo DbContextOptions</param>
        public DemoContext(DbContextOptions<DemoContext> options)
            : base(options)
        {
        }

       
        /// <summary>
        /// Abstracción de la entidad Empleado de la base de datos
        /// </summary>
        public virtual DbSet<Empleado> Empleado { get; set; }

    }
}
