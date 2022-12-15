using DemoAPI.Core;
using DemoAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoAPI.DAO
{
    /// <summary>
    /// Clase para realizar las operaciones CRUD con la tabla Empleados de la base de datos
    /// </summary>
    public class EmpleadoDAO
    {
        private readonly DemoContext contexto;
        public CustomError customError;

        /// <summary>
        /// Contructor de clase
        /// </summary>
        /// <param name="context">Recibe el contexto de la base de datos</param>
        public EmpleadoDAO(DemoContext context)
        {
            this.contexto = context;
        }

        /// <summary>
        /// Obtiene todas los empleados
        /// </summary>
        /// <returns></returns>
        public async Task<List<Empleado>> ObtenerTodoAsync()
        {
            return await contexto.Empleado.ToListAsync();

        }

        /// <summary>
        /// Metodo para obtener los datos del empleado por Id
        /// </summary>
        /// <param name="id">Identificador del empleado</param>
        /// <returns></returns>

        public async Task<Empleado> ObtenerPorIdAsync(int id)
        {
            return await contexto.Empleado.FindAsync(id);
        }

        /// <summary>
        /// Metodo para agregar un registro de empleado
        /// </summary>
        /// <param name="empleado">Objeto que contiene los datos del empleado</param>
        /// <returns></returns>
        public async Task<bool> AgregarAsync(Empleado empleado)
        {
            Empleado registroRepetido;

            registroRepetido = contexto.Empleado
               .FirstOrDefault(c => c.Nombre == empleado.Nombre);
            if (registroRepetido != null)
            {
                customError = new CustomError(400,
                      "Ya existe un empleado con este nombre, " +
                      "por favor teclea un nombre diferente", "Nombre");
                return false;
            }
            registroRepetido = contexto.Empleado
              .FirstOrDefault(c => c.Id == empleado.Id);
            if (registroRepetido != null)
            {
                customError = new CustomError(400,
                       "Ya existe un empleado con este Id, " +
                       "por favor teclea id diferente", "Nombre");
                return false;
            }

            contexto.Empleado.Add(empleado);
            await contexto.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Modidica un empleado
        /// </summary>
        /// <param name="empleado">Datos del empleado</param>
        /// <returns></returns>
        public async Task<bool> ModificarAsync(Empleado empleado)
        {
            Empleado registroRepetido;
            try
            {
                //Se busca si existe un empleado con el mismo nombre 
                //pero diferente Id
                registroRepetido = contexto.Empleado
                            .FirstOrDefault(c => c.Nombre == empleado.Nombre
                                             && c.Id != empleado.Id);
                if (registroRepetido != null)
                {
                    customError = new CustomError(400,
                                   "Ya existe un empleado con este nombre, " +
                                   "por favor teclea un nombre diferente",
                                    "Nombre");
                    return false;
                }
                
                contexto.Entry(empleado).State = EntityState.Modified;
                await contexto.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExisteEmpleado(empleado.Id))
                {
                    customError = new CustomError(400, "El empleado " +
                                                      "ya no existe",
                                                      "Empleado");
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// Metodo para validar que existe un empleado en la base de datos
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool ExisteEmpleado(int id)
        {
            return contexto.Empleado.Any(e => e.Id == id);
        }

    }
}
