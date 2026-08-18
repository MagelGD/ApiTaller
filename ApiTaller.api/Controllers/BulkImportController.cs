using ApiTaller.Domain.Dtos.BulkImport;
using ApiTaller.Domain.Interfaces.Services.BulkImport;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace ApiTaller.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [RequestSizeLimit(5_242_880)] // 5 MB máximo
    public class BulkImportController : ControllerBase
    {
        private readonly IBulkImportService _service;
        private readonly ILogger<BulkImportController> _logger;

        public BulkImportController(IBulkImportService service, ILogger<BulkImportController> logger)
        {
            _service = service;
            _logger = logger;
        }

        #region Productos

        [HttpGet("products/template")]
        public async Task<IActionResult> DownloadProductTemplate(CancellationToken ct)
        {
            try
            {
                byte[] fileBytes = await _service.GenerateProductTemplateAsync(ct);
                return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Plantilla_Productos.xlsx");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al generar la plantilla de productos");
                return StatusCode(500, new { message = "Error al generar la plantilla de productos." });
            }
        }

        [HttpPost("products")]
        public async Task<IActionResult> ImportProducts([FromForm] IFormFile file, CancellationToken ct)
        {
            if (file == null || file.Length == 0)
                return BadRequest(new { message = "Debe proporcionar un archivo válido." });

            string extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (extension != ".xlsx")
                return BadRequest(new { message = "Formato no válido. Solo se admiten archivos de Excel (.xlsx)." });

            try
            {
                int userId = GetCurrentUserId();
                using Stream stream = file.OpenReadStream();
                BulkImportResultDto result = await _service.ImportProductsAsync(stream, userId, ct);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error durante la importación masiva de productos");
                return StatusCode(500, new { message = "Ocurrió un error inesperado al procesar el archivo.", error = ex.Message });
            }
        }

        #endregion

        #region Tipos de Producto

        [HttpGet("product-types/template")]
        public async Task<IActionResult> DownloadProductTypeTemplate(CancellationToken ct)
        {
            try
            {
                byte[] fileBytes = await _service.GenerateProductTypeTemplateAsync(ct);
                return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Plantilla_TiposProducto.xlsx");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al generar la plantilla de tipos de producto");
                return StatusCode(500, new { message = "Error al generar la plantilla de tipos de producto." });
            }
        }

        [HttpPost("product-types")]
        public async Task<IActionResult> ImportProductTypes([FromForm] IFormFile file, CancellationToken ct)
        {
            if (file == null || file.Length == 0)
                return BadRequest(new { message = "Debe proporcionar un archivo válido." });

            string extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (extension != ".xlsx")
                return BadRequest(new { message = "Formato no válido. Solo se admiten archivos de Excel (.xlsx)." });

            try
            {
                int userId = GetCurrentUserId();
                using Stream stream = file.OpenReadStream();
                BulkImportResultDto result = await _service.ImportProductTypesAsync(stream, userId, ct);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error durante la importación masiva de tipos de producto");
                return StatusCode(500, new { message = "Ocurrió un error al procesar el archivo.", error = ex.Message });
            }
        }

        #endregion

        #region Catálogo de Servicios

        [HttpGet("services/template")]
        public async Task<IActionResult> DownloadServiceCatalogTemplate(CancellationToken ct)
        {
            try
            {
                byte[] fileBytes = await _service.GenerateServiceCatalogTemplateAsync(ct);
                return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Plantilla_Servicios.xlsx");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al generar la plantilla de servicios");
                return StatusCode(500, new { message = "Error al generar la plantilla de servicios." });
            }
        }

        [HttpPost("services")]
        public async Task<IActionResult> ImportServices([FromForm] IFormFile file, CancellationToken ct)
        {
            if (file == null || file.Length == 0)
                return BadRequest(new { message = "Debe proporcionar un archivo válido." });

            string extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (extension != ".xlsx")
                return BadRequest(new { message = "Formato no válido. Solo se admiten archivos de Excel (.xlsx)." });

            try
            {
                int userId = GetCurrentUserId();
                using Stream stream = file.OpenReadStream();
                BulkImportResultDto result = await _service.ImportServiceCatalogsAsync(stream, userId, ct);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error durante la importación masiva de servicios");
                return StatusCode(500, new { message = "Ocurrió un error al procesar el archivo.", error = ex.Message });
            }
        }

        #endregion

        #region Tipos de Servicio

        [HttpGet("service-types/template")]
        public async Task<IActionResult> DownloadServiceTypeTemplate(CancellationToken ct)
        {
            try
            {
                byte[] fileBytes = await _service.GenerateServiceTypeTemplateAsync(ct);
                return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Plantilla_TiposServicio.xlsx");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al generar la plantilla de tipos de servicio");
                return StatusCode(500, new { message = "Error al generar la plantilla de tipos de servicio." });
            }
        }

        [HttpPost("service-types")]
        public async Task<IActionResult> ImportServiceTypes([FromForm] IFormFile file, CancellationToken ct)
        {
            if (file == null || file.Length == 0)
                return BadRequest(new { message = "Debe proporcionar un archivo válido." });

            string extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (extension != ".xlsx")
                return BadRequest(new { message = "Formato no válido. Solo se admiten archivos de Excel (.xlsx)." });

            try
            {
                int userId = GetCurrentUserId();
                using Stream stream = file.OpenReadStream();
                BulkImportResultDto result = await _service.ImportServiceTypesAsync(stream, userId, ct);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error durante la importación masiva de tipos de servicio");
                return StatusCode(500, new { message = "Ocurrió un error al procesar el archivo.", error = ex.Message });
            }
        }

        #endregion

        #region Private Helpers

        private int GetCurrentUserId()
        {
            string? userIdStr = User.FindFirst(ClaimTypes.Sid)?.Value
                             ?? User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                             ?? User.FindFirst("id")?.Value;

            return int.TryParse(userIdStr, out int id) ? id : 1;
        }

        #endregion
    }
}
