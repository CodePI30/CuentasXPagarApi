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
    public class DocumentoXPagarController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DocumentoXPagarController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/DocumentoXPagar
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DocumentoPagar>>> GetDocumentoPago()
        {
            return await _context.DocumentoPago.ToListAsync();
        }

        // GET: api/DocumentoXPagar/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DocumentoPagar>> GetDocumentoPagar(int id)
        {
            var documentoPagar = await _context.DocumentoPago.FindAsync(id);

            if (documentoPagar == null)
            {
                return NotFound();
            }

            return documentoPagar;
        }

        // PUT: api/DocumentoXPagar/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDocumentoPagar(int id, DocumentoPagar documentoPagar)
        {
            if (id != documentoPagar.id)
            {
                return BadRequest();
            }

            var documentoAnterior = await _context.DocumentoPago.AsNoTracking().FirstOrDefaultAsync(d => d.id == id);
            if (documentoAnterior == null)
            {
                return NotFound();
            }

            _context.Entry(documentoPagar).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                // Log update
                await LogOperacionAsync("documentos_pagar", "UPDATE", datosAnteriores: documentoAnterior, datosNuevos: documentoPagar);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DocumentoPagarExists(id))
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

        // POST: api/DocumentoXPagar
        [HttpPost]
        public async Task<ActionResult<DocumentoPagar>> PostDocumentoPagar(DocumentoPagar documentoPagar)
        {
            _context.DocumentoPago.Add(documentoPagar);
            await _context.SaveChangesAsync();

            // Log insert
            await LogOperacionAsync("documentos_pagar", "INSERT", datosNuevos: documentoPagar);

            return CreatedAtAction("GetDocumentoPagar", new { id = documentoPagar.id }, documentoPagar);
        }

        // DELETE: api/DocumentoXPagar/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDocumentoPagar(int id)
        {
            var documentoPagar = await _context.DocumentoPago.FindAsync(id);
            if (documentoPagar == null)
            {
                return NotFound();
            }

            _context.DocumentoPago.Remove(documentoPagar);
            await _context.SaveChangesAsync();

            // Log delete
            await LogOperacionAsync("documentos_pagar", "DELETE", datosAnteriores: documentoPagar);

            return NoContent();
        }


        private bool DocumentoPagarExists(int id)
        {
            return _context.DocumentoPago.Any(e => e.id == id);
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
