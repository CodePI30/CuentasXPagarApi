using CuentasXPagar.api.BulkUpdate;
using CuentasXPagar.api.HttpService;
using CuentasXPagar.api.Models;
using CuentasXPagar.data.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace CuentasXPagar.api.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class ContabilidadController : ControllerBase
    {
        private readonly ContabilidadHttpService _httpService;
        private readonly BulkUpdateData _bulkUpdate;
        public ContabilidadController(ContabilidadHttpService httpService, BulkUpdateData bulkUpdateData)
        {
            _httpService = httpService;
            _bulkUpdate = bulkUpdateData;
        }

        [HttpGet("catalogo-cuentas")]
        public async Task<IActionResult> ObtenerCatalogoCuentas()
        {
            try
            {
                var resultado = await _httpService.GetCatalogoCuentasAsync();
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener el catálogo: {ex.Message}");
            }
        }

        [HttpGet("entradas-contables")]
        public async Task<IActionResult> GetEntradasContablesAsync(
            [FromQuery] DateTime fechaInicio,
            [FromQuery] DateTime fechaFin,
            [FromQuery] int cuentaId)
        {
            try
            {
                var resultado = await _httpService.GetEntradasContablesAsync(fechaInicio, fechaFin, cuentaId);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener las entradas contables: {ex.Message}");
            }
        }

        [HttpPost("input-entradas-contables")]
        public async Task<IActionResult> GetEntradasContablesAsync(EntradaContableRequest request)
        {
            try
            {
                var resultado = await _httpService.PostEntradaContableAsync(request);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al crear la entrada contable: {ex.Message}");
            }
        }

        [HttpGet("balance-cuentas")]
        public async Task<IActionResult> ObtenerBalanceCuentas()
        {
            try
            {
                var resultado = await _httpService.GetBalancesCuentasAsync();
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener el catálogo: {ex.Message}");
            }
        }

        [HttpPatch("actualizar-IdContable")]
        public async Task<IActionResult> ActualizarIdContableDocumentos(
            [FromBody] List<string> idDocumento,
            [FromQuery] int IdAsientoContable)
        {
            try
            {
                var updatedRecords = await _bulkUpdate.BulkUpdateDocuments(idDocumento, IdAsientoContable);
                return Ok(updatedRecords);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al actualizar el id de cuenta: {ex.Message}");
            }
        }

    }
}
