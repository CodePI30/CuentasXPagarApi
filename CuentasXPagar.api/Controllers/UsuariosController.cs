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
    public class UsuariosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UsuariosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Usuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
            return await _context.Usuarios.ToListAsync();
        }

        // GET: api/Usuarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return usuario;
        }

        // PUT: api/Usuarios/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(int id, Usuario usuario)
        {
            if (id != usuario.id)
            {
                return BadRequest();
            }

            var usuarioAnterior = await _context.Usuarios.AsNoTracking().FirstOrDefaultAsync(u => u.id == id);
            if (usuarioAnterior == null)
            {
                return NotFound();
            }

            _context.Entry(usuario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                // Log update
                await LogOperacionAsync("usuarios", "UPDATE", datosAnteriores: usuarioAnterior, datosNuevos: usuario);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(id))
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

        // POST: api/Usuarios
        [HttpPost]
        public async Task<ActionResult<Usuario>> PostUsuario(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            // Log insert
            await LogOperacionAsync("usuarios", "INSERT", datosNuevos: usuario);

            return CreatedAtAction("GetUsuario", new { id = usuario.id }, usuario);
        }

        // DELETE: api/Usuarios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();

            // Log delete
            await LogOperacionAsync("usuarios", "DELETE", datosAnteriores: usuario);

            return NoContent();
        }


        private bool UsuarioExists(int id)
        {
            return _context.Usuarios.Any(e => e.id == id);
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
