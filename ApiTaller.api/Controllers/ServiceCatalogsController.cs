using ApiTaller.Domain.Dtos.ServiceCatalogs;
using ApiTaller.Domain.Interfaces.Services.ServiceCatalogs;
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
    public class ServiceCatalogsController : ControllerBase
    {
        private readonly ILogger<ServiceCatalogsController> _logger;
        private readonly IServiceCatalogService _service;

        public ServiceCatalogsController(ILogger<ServiceCatalogsController> logger, IServiceCatalogService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet("GetServiceCatalog")]
        public async Task<IActionResult> GetServiceCatalog(CancellationToken cancellation)
        {
            try
            {
                return Ok(await _service.GetAllAsync(cancellation));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving service catalogs");
            }
            return BadRequest();
        }

        [HttpGet("GetServiceCatalogActive")]
        public async Task<IActionResult> GetServiceCatalogActive(CancellationToken cancellation)
        {
            try
            {
                return Ok(await _service.GetAllActiveAsync(cancellation));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving active service catalogs");
            }
            return BadRequest();
        }

        [HttpGet("GetServiceCatalog/{id}")]
        public async Task<IActionResult> GetServiceCatalogById(int id, CancellationToken cancellation)
        {
            try
            {
                return Ok(await _service.GetByIdAsync(id, cancellation));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving service catalog with id {id}");
            }
            return BadRequest();
        }

        [HttpPost("SaveOrUpdate")]
        public async Task<IActionResult> SaveOrUpdate(GetServiceCatalogDto value, CancellationToken cancellation)
        {
            try
            {
                return Ok(await _service.CreateOrEditServiceCatalog(value, cancellation));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving or updating service catalog");
            }
            return BadRequest();
        }
    }
}
