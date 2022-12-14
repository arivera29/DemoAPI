using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DemoAPI.Models;

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

        /// <summary>
        /// Construcctor de la clase
        /// </summary>
        /// <param name="context">Contexto para realizar las conexiones a la base de datos</param>
        public EmpleadosController(DemoContext context)
        {
            _context = context;
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
            return await _context.Empleado.ToListAsync();
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
            var empleado = await _context.Empleado.FindAsync(id);

            if (empleado == null)
            {
                return NotFound();
            }

            return empleado;
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
            if (id != empleado.Id)
            {
                return BadRequest();
            }

            _context.Entry(empleado).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmpleadoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
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
            _context.Empleado.Add(empleado);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmpleado", new { id = empleado.Id }, empleado);
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

        /// <summary>
        /// Método para consultar si existe un empleado en la base de datos
        /// </summary>
        /// <param name="id">Identificador del empleado</param>
        /// <returns>Objeto de tipo Empleado</returns>

        private bool EmpleadoExists(int id)
        {
            return _context.Empleado.Any(e => e.Id == id);
        }
    }
}
