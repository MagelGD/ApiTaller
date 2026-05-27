using ApiTaller.Domain.Dtos.BrandModels;
using ApiTaller.Domain.Interfaces.Services.BrandModels;
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
    public class BrandModelsController : ControllerBase
    {
        private readonly ILogger<BrandModelsController> _logger;
        private readonly IBrandModelsService _brandModelService;

        public BrandModelsController(ILogger<BrandModelsController> logger, IBrandModelsService brandModelService)
        {
            _logger = logger;
            _brandModelService = brandModelService;
        }

        [HttpGet("GetBrandModels")]
        public async Task<IActionResult> GetBrandModels(CancellationToken cancellation)
        {
            try
            {
                return Ok(await _brandModelService.GetAllBrandModelsAsync(cancellation));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener los modelos de marca.");
            }
            return BadRequest();
        }

        [HttpGet("GetBrandModelsActive")]
        public async Task<IActionResult> GetBrandModelsActive([FromQuery] string? vehicleType, CancellationToken cancellation)
        {
            try
            {
                return Ok(await _brandModelService.GetAllBrandModelsActiveAsync(vehicleType, cancellation));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener los modelos de marca activos.");
            }
            return BadRequest();
        }

        [HttpGet("GetBrandModel/{id}")]
        public async Task<IActionResult> GetBrandModelById(int id, CancellationToken cancellation)
        {
            try
            {
                return Ok(await _brandModelService.GetBrandModelByIdAsync(id, cancellation));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener el modelo de marca con ID {id}.");
            }
            return BadRequest();
        }

        [HttpPost("CreateOrEditBrandModel")]
        public async Task<IActionResult> CreateOrEditBrandModel(GetBrandModelsDto value, CancellationToken cancellation)
        {
            try
            {
                return Ok(await _brandModelService.CreateOrEditBrandModel(value, cancellation));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear o editar el modelo de marca.");
            }
            return BadRequest();
        }
    }
}
