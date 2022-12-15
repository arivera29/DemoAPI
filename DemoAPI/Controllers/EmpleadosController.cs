using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DemoAPI.Models;
using DemoAPI.DAO;

namespace DemoAPI.Controllers
{
    /// <summary>
    /// Clase Controlador de las API REST de la entidad Empleado.
    /// </summary>
    /// <remarks>
    /// <para>Esta clase implementa los métodos para realizar el CRUD en la tabla Empleados</para>
    /// <para>La clase hereda de ControllerBase</para>
    /// <para>El End-Point del API es GET: api/Empleados</para>
    /// </remarks>
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadosController : ControllerBase
    {
        private readonly DemoContext _context;
        private EmpleadoDAO empleadoDAO;

        /// <summary>
        /// Construcctor de la clase
        /// </summary>
        /// <param name="context">Contexto para realizar las conexiones a la base de datos</param>
        public EmpleadosController(DemoContext context)
        {
            _context = context;
            empleadoDAO = new EmpleadoDAO(context);
        }

        /// <summary>
        /// Método para obtener el listado de los empleados registrados en la base datos.
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// <para>El End-Point del API es GET: api/Empleados</para>
        /// </remarks>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Empleado>>> GetEmpleado()
        {
            return await empleadoDAO.ObtenerTodoAsync();
        }
        /// <summary>
        /// Método para obtener los datos de un empleado registrado en la base de datos.
        /// </summary>
        /// <remarks>
        /// <para>El End-Point del API es GET: api/Empleados/{id}</para>
        /// </remarks>
        /// <param name="id">Identificador del empleado</param>
        /// <returns>Retorna un objeto de tipo Empleado que contiene los datos del empleado consultado en la base datos.</returns>

        [HttpGet("{id}")]
        public async Task<ActionResult<Empleado>> GetEmpleado(int id)
        {
            var empleado = await empleadoDAO
                                 .ObtenerPorIdAsync(id);
            if (empleado == null)
                return NotFound();
            return Ok(empleado);
        }

        /// <summary>
        /// Método para actualizar un registro en la tabla empleados de la base de datos
        /// </summary>
        /// <remarks>
        /// <para>El End-Point del API es PUT: api/Empleados/{id}</para>
        /// </remarks>
        /// <param name="id">Identificador único del empleado</param>
        /// <param name="empleado">Objeto de tipo Empleado que contiene los datos del empleado</param>
        /// <returns></returns>
        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmpleado(int id, Empleado empleado)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != empleado.Id)
                return BadRequest();

            if (!await empleadoDAO.ModificarAsync(empleado))
            {
                return StatusCode(empleadoDAO.customError.StatusCode,
                                         empleadoDAO.customError.Message);
            }

            return NoContent();
        }
        /// <summary>
        /// Método para agregar un registro en la tabla empleados de la base de datos
        /// </summary>
        /// <remarks>
        /// <para>El End-Point del API es PUT: api/Empleados</para>
        /// </remarks>
        /// <param name="empleado">Objeto de tipo Empleado que contiene los datos del empleado</param>
        /// <returns>Objeto de tipo Empleado</returns>
        
        [HttpPost]
        public async Task<ActionResult<Empleado>> PostEmpleado(Empleado empleado)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            //Cambiamos el código para agregar aquí la clase.
            //Si no fue correcto regresamos el mensaje de error devuelto 
            if (!await empleadoDAO.AgregarAsync(empleado))
            {
                return StatusCode(empleadoDAO.customError.StatusCode,
                                  empleadoDAO.customError.Message);
            }
            return CreatedAtAction("GetEmpleado",
                                      new { id = empleado.Id }, empleado);
        }
        /// <summary>
        /// Método para eliminar un empleado en la base de datos
        /// </summary>
        /// <remarks>
        /// <para>El End-Point del API es DELETE: api/Empleados/{id}</para>
        /// </remarks>
        /// <param name="id">Identificador del empleado en la tabla</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmpleado(int id)
        {
            var empleado = await _context.Empleado.FindAsync(id);
            if (empleado == null)
            {
                return NotFound();
            }

            _context.Empleado.Remove(empleado);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        
    }
}
