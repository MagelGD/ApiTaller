using ApiTaller.Domain.Dtos.Brand;
using ApiTaller.Domain.Interfaces.Services.Brands;
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
    public class BrandsController : ControllerBase
    {
        private readonly ILogger<BrandsController> _logger;
        private readonly IBrandService _brandService;

        public BrandsController(ILogger<BrandsController> logger, IBrandService brandService)
        {
            _logger = logger;
            _brandService = brandService;
        }

        [HttpGet("GetBrands")]
        public async Task<IActionResult> GetBrands(CancellationToken cancellation)
        {
            try
            {
                return Ok(await _brandService.GetAllBrandsAsync(cancellation));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener las marcas.");
            }
            return BadRequest();
        }

        [HttpGet("GetBrandsActive")]
        public async Task<IActionResult> GetBrandsActive([FromQuery] string? vehicleType, CancellationToken cancellation)
        {
            try
            {
                return Ok(await _brandService.GetAllBrandsActiveAsync(vehicleType, cancellation));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener las marcas activas.");
            }
            return BadRequest();
        }

        [HttpGet("GetBrands/{id}")]
        public async Task<IActionResult> GetBrandById(int id, CancellationToken cancellation)
        {
            try
            {
                return Ok(await _brandService.GetBrandByIdAsync(id, cancellation));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener la marca con ID {id}.");
            }
            return BadRequest();
        }

        [HttpPost("SaveOrEditBrands")]
        public async Task<IActionResult> SaveOrEditBrands(GetBrandDto value, CancellationToken cancellation)
        {
            try
            {
                return Ok(await _brandService.CreateOrEditBrand(value, cancellation));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al guardar o editar la marca.");
            }
            return BadRequest();
        }
    }
}
