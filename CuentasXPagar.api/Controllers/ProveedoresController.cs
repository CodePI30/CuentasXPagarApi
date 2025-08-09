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
    public class ProveedoresController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProveedoresController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Proveedores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Proveedor>>> GetProveedores()
        {
            return await _context.Proveedores.ToListAsync();
        }

        // GET: api/Proveedores/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Proveedor>> GetProveedor(int id)
        {
            var proveedor = await _context.Proveedores.FindAsync(id);

            if (proveedor == null)
            {
                return NotFound();
            }

            return proveedor;
        }

        // PUT: api/Proveedores/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProveedor(int id, Proveedor proveedor)
        {
            if (id != proveedor.id)
            {
                return BadRequest();
            }

            //if (!ValidarCedulaORnc(proveedor.cedula_rnc))
            //{
            //    return BadRequest("El RNC o cédula no es válido.");
            //}


            var proveedorAnterior = await _context.Proveedores.AsNoTracking().FirstOrDefaultAsync(p => p.id == id);
            if (proveedorAnterior == null)
            {
                return NotFound();
            }

            _context.Entry(proveedor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                // Log update
                await LogOperacionAsync("proveedores", "UPDATE", datosAnteriores: proveedorAnterior, datosNuevos: proveedor);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProveedorExists(id))
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

        [HttpPost]
        public async Task<ActionResult<Proveedor>> PostProveedor(Proveedor proveedor)
        {
            //if (!ValidarCedulaORnc(proveedor.cedula_rnc))
            //{
            //    return BadRequest("El RNC o cédula no es válido.");
            //}

            _context.Proveedores.Add(proveedor);
            await _context.SaveChangesAsync();

            await LogOperacionAsync("proveedores", "INSERT", datosNuevos: proveedor);

            return CreatedAtAction("GetProveedor", new { id = proveedor.id }, proveedor);
        }


        // DELETE: api/Proveedores/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProveedor(int id)
        {
            var proveedor = await _context.Proveedores.FindAsync(id);
            if (proveedor == null)
            {
                return NotFound();
            }

            _context.Proveedores.Remove(proveedor);
            await _context.SaveChangesAsync();

            // Log delete
            await LogOperacionAsync("proveedores", "DELETE", datosAnteriores: proveedor);

            return NoContent();
        }


        private bool ProveedorExists(int id)
        {
            return _context.Proveedores.Any(e => e.id == id);
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

        //public static bool ValidarCedulaORnc(string numero)
        //{
        //    if (string.IsNullOrWhiteSpace(numero)) return false;

        //    numero = numero.Replace("-", "").Trim();

        //    if (numero.Length != 11 || !numero.All(char.IsDigit)) return false;

        //    int[] pesos = new int[] { 1, 2, 1, 2, 1, 2, 1, 2, 1, 2 };
        //    int suma = 0;

        //    for (int i = 0; i < 10; i++)
        //    {
        //        int multiplicacion = (numero[i] - '0') * pesos[i];
        //        suma += (multiplicacion < 10) ? multiplicacion : (multiplicacion / 10) + (multiplicacion % 10);
        //    }

        //    int digitoVerificador = (10 - (suma % 10)) % 10;

        //    return digitoVerificador == (numero[10] - '0');
        //}

    }
}
