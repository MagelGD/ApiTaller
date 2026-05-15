using ApiTaller.Domain.Dtos.ServicePrices;
using ApiTaller.Domain.Interfaces.Services.ServicePrices;
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
    public class ServicePricesController : ControllerBase
    {
        private readonly ILogger<ServicePricesController> _logger;
        private readonly IServicePriceByVersionService _service;

        public ServicePricesController(ILogger<ServicePricesController> logger, IServicePriceByVersionService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet("GetServicePrice")]
        public async Task<IActionResult> GetServicePrice(CancellationToken cancellation)
        {
            try
            {
                return Ok(await _service.GetAllAsync(cancellation));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving service prices");
            }
            return BadRequest();
        }

        [HttpGet("GetServicePriceActive")]
        public async Task<IActionResult> GetServicePriceActive(CancellationToken cancellation)
        {
            try
            {
                return Ok(await _service.GetAllActiveAsync(cancellation));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving active service prices");
            }
            return BadRequest();
        }

        [HttpGet("GetServicePrice/{id}")]
        public async Task<IActionResult> GetServicePriceById(int id, CancellationToken cancellation)
        {
            try
            {
                return Ok(await _service.GetByIdAsync(id, cancellation));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving service price with id {id}");
            }
            return BadRequest();
        }

        [HttpGet("GetServicePriceByVersion/{versionId}")]
        public async Task<IActionResult> GetServicePriceByVersion(int versionId, CancellationToken cancellation)
        {
            try
            {
                return Ok(await _service.GetByVersionAsync(versionId, cancellation));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving service prices for version {versionId}");
            }
            return BadRequest();
        }

        [HttpPost("SaveOrUpdate")]
        public async Task<IActionResult> SaveOrUpdate(GetServicePriceByVersionDto value, CancellationToken cancellation)
        {
            try
            {
                return Ok(await _service.CreateOrEditServicePrice(value, cancellation));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving or updating service price");
            }
            return BadRequest();
        }
    }
}
