using ApiTaller.Domain.Dtos.ServicePrices;
using ApiTaller.Domain.Interfaces.Services.ServicePrices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiTaller.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicePricesController : ControllerBase
    {
        private readonly ILogger<ServicePricesController> _logger;
        private readonly IServicePriceByVersionService _service;

        public ServicePricesController(IServicePriceByVersionService service, ILogger<ServicePricesController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet("GetServicePrice")]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _service.GetAllAsync(cancellationToken));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving service prices");
            }
            return BadRequest();
        }

        [HttpGet("GetServicePriceActive")]
        public async Task<IActionResult> GetActive(CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _service.GetAllActiveAsync(cancellationToken));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving active service prices");
            }
            return BadRequest();
        }

        [HttpGet("GetServicePrice/{id}")]
        public async Task<IActionResult> Get(int id, CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _service.GetByIdAsync(id, cancellationToken));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving service price with id {id}");
            }
            return BadRequest();
        }

        [HttpPost("SaveOrUpdate")]
        public async Task<IActionResult> Post(GetServicePriceByVersionDto value, CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _service.CreateOrEditServicePrice(value, cancellationToken));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving or updating service price");
            }
            return BadRequest();
        }
    }
}
