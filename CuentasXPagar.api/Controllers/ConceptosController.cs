using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CuentasXPagar.data.DbContextSqlServer ;
using CuentasXPagar.data.Entidades;

namespace CuentasXPagar.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConceptosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ConceptosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Conceptos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Concepto>>> GetConceptos()
        {
            return await _context.Conceptos.ToListAsync();
        }

        // GET: api/Conceptos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Concepto>> GetConcepto(int id)
        {
            var concepto = await _context.Conceptos.FindAsync(id);

            if (concepto == null)
            {
                return NotFound();
            }

            return concepto;
        }
        // POST
        [HttpPost]
        public async Task<ActionResult<Concepto>> PostConcepto(Concepto concepto)
        {
            _context.Conceptos.Add(concepto);
            await _context.SaveChangesAsync();

            await LogOperacionAsync("conceptos", "INSERT", datosNuevos: concepto);

            return CreatedAtAction("GetConcepto", new { id = concepto.id }, concepto);
        }

        // PUT
        [HttpPut("{id}")]
        public async Task<IActionResult> PutConcepto(int id, Concepto concepto)
        {
            if (id != concepto.id)
            {
                return BadRequest();
            }

            var conceptoAnterior = await _context.Conceptos.AsNoTracking().FirstOrDefaultAsync(c => c.id == id);
            if (conceptoAnterior == null)
            {
                return NotFound();
            }

            _context.Entry(concepto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                await LogOperacionAsync("conceptos", "UPDATE", conceptoAnterior, concepto);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConceptoExists(id))
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

        // DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConcepto(int id)
        {
            var concepto = await _context.Conceptos.FindAsync(id);
            if (concepto == null)
            {
                return NotFound();
            }

            _context.Conceptos.Remove(concepto);
            await _context.SaveChangesAsync();

            await LogOperacionAsync("conceptos", "DELETE", datosAnteriores: concepto);

            return NoContent();
        }


        private bool ConceptoExists(int id)
        {
            return _context.Conceptos.Any(e => e.id == id);
        }

        private string GetClientIp()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
            {
                var ipList = Request.Headers["X-Forwarded-For"].ToString();
                if (!string.IsNullOrEmpty(ipList))
                {
                    return ipList.Split(',')[0];
                }
            }

            return HttpContext.Connection.RemoteIpAddress?.ToString() ?? "IP desconocida";
        }

        private async Task LogOperacionAsync(string tabla, string operacion, object datosAnteriores = null, object datosNuevos = null)
        {
            var usuario = GetClientIp();

            var log = new LogOperacion
            {
                tabla_afectada = tabla,
                tipo_operacion = operacion,
                usuario = usuario,
                datos_anteriores = datosAnteriores != null ? System.Text.Json.JsonSerializer.Serialize(datosAnteriores) : null,
                datos_nuevos = datosNuevos != null ? System.Text.Json.JsonSerializer.Serialize(datosNuevos) : null,
                fecha_operacion = DateTime.UtcNow
            };

            _context.LogsOperaciones.Add(log);
            await _context.SaveChangesAsync();
        }

    }
}
