using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CuentasXPagar.data.DbContextPostGreSql;
using CuentasXPagar.data.Entidades;

namespace CuentasXPagar.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogOperacionesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public LogOperacionesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/LogOperaciones
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LogOperacion>>> GetLogsOperaciones()
        {
            var result = await _context.LogsOperaciones
                .OrderByDescending(l => l.fecha_operacion)
                .ToListAsync();

            return result;
        }

        // GET: api/LogOperaciones/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LogOperacion>> GetLogOperacion(int id)
        {
            var logOperacion = await _context.LogsOperaciones.FindAsync(id);

            if (logOperacion == null)
            {
                return NotFound();
            }

            return logOperacion;
        }

        // PUT: api/LogOperaciones/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLogOperacion(int id, LogOperacion logOperacion)
        {
            if (id != logOperacion.id)
            {
                return BadRequest();
            }

            _context.Entry(logOperacion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LogOperacionExists(id))
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

        // POST: api/LogOperaciones
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<LogOperacion>> PostLogOperacion(LogOperacion logOperacion)
        {
            _context.LogsOperaciones.Add(logOperacion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLogOperacion", new { id = logOperacion.id }, logOperacion);
        }

        // DELETE: api/LogOperaciones/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLogOperacion(int id)
        {
            var logOperacion = await _context.LogsOperaciones.FindAsync(id);
            if (logOperacion == null)
            {
                return NotFound();
            }

            _context.LogsOperaciones.Remove(logOperacion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LogOperacionExists(int id)
        {
            return _context.LogsOperaciones.Any(e => e.id == id);
        }
    }
}
