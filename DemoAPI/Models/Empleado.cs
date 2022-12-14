using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DemoAPI.Models
{
    /// <summary>
    /// Clase que realiza la abstracción de la estructura de la tabla Empleado en la base de datos
    /// </summary>
    public class Empleado
    {
        /// <summary>
        /// Identificador del empleado
        /// </summary>
        /// <value>El identificador es autoincremental</value>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Nombre del empleado
        /// </summary>
        /// <value>Nombre del empleado</value>
        [Required(ErrorMessage = "Debe ingresar el nombre")]
        public string Nombre { get; set; }
        
        /// <summary>
        /// Apellido del empleado
        /// </summary>
        /// <value>Apellido del empleado</value>
        [Required(ErrorMessage = "Debe ingresar el apellido")]
        public string Apellido { get; set; }

        /// <summary>
        /// Fecha de nacimiento del empleado
        /// </summary>
        /// <value>Fecha de nacimiento del empleado</value>
        [Required(ErrorMessage = "Debe ingresar la fecha de nacimiento")]
        public DateTime FechaNacimiento { get; set; }
        /// <summary>
        /// Foto del empleado
        /// </summary>
        /// <value>Foto del empleado</value>
        public byte[] Foto { get; set; }

        /// <summary>
        /// Estado civil del empleado
        /// </summary>
        /// <value>Estado civil del empleado</value>
        [Required(ErrorMessage = "Debe ingresar el esatdo civil")]
        [Range(0, 1)]
        public int EstadoCivil { get; set; }
        /// <summary>
        /// Empleado tiene hermanos?
        /// </summary>
        /// <value>Empleado tiene hermanos?</value>
        [Required(ErrorMessage = "Debe ingresar si el empleado tiene hermanos")]
        [Range(0, 1)]
        public Boolean Hermanos { get; set; }

        
    }
}
