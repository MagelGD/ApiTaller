using ApiTaller.Domain.Dtos.BrandModelVersion;
using ApiTaller.Domain.Interfaces.Services.BrandModelVersion;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiTaller.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BrandModelVersionsController : ControllerBase
    {
        private readonly ILogger<BrandModelVersionsController> _logger;
        private readonly IBrandModelVersionService _brandModelVersionService;

        public BrandModelVersionsController(ILogger<BrandModelVersionsController> logger, IBrandModelVersionService brandModelVersionService)
        {
            _logger = logger;
            _brandModelVersionService = brandModelVersionService;
        }

        [HttpGet("GetBrandModelVersions")]
        public async Task<IActionResult> GetBrandModelVersions(CancellationToken cancellation)
        {
            try
            {
                return Ok(await _brandModelVersionService.GetAllAsync(cancellation));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener las versiones de modelo de marca.");
            }
            return BadRequest();
        }

        [HttpGet("GetBrandModelVersionsActive")]
        public async Task<IActionResult> GetBrandModelVersionsActive(CancellationToken cancellation)
        {
            try
            {
                return Ok(await _brandModelVersionService.GetAllActiveAsync(cancellation));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener las versiones de modelo de marca activas.");
            }
            return BadRequest();
        }

        [HttpGet("GetBrandModelVersion/{id}")]
        public async Task<IActionResult> GetBrandModelVersionById(int id, CancellationToken cancellation)
        {
            try
            {
                return Ok(await _brandModelVersionService.GetByIdAsync(id, cancellation));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener la versión de modelo de marca con ID {id}.");
            }
            return BadRequest();
        }

        [HttpPost("CreateOrEditBrandModelVersion")]
        public async Task<IActionResult> CreateOrEditBrandModelVersion(GetBrandModelVersionDto value, CancellationToken cancellation)
        {
            try
            {
                return Ok(await _brandModelVersionService.CreateOrEditAsync(value, cancellation));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear o editar la versión de modelo de marca.");
            }
            return BadRequest();
        }
    }
}
