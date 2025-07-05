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
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDocumentoPagar(int id, DocumentoPagar documentoPagar)
        {
            if (id != documentoPagar.id)
            {
                return BadRequest();
            }

            _context.Entry(documentoPagar).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
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
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DocumentoPagar>> PostDocumentoPagar(DocumentoPagar documentoPagar)
        {
            _context.DocumentoPago.Add(documentoPagar);
            await _context.SaveChangesAsync();

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

            return NoContent();
        }

        private bool DocumentoPagarExists(int id)
        {
            return _context.DocumentoPago.Any(e => e.id == id);
        }
    }
}
