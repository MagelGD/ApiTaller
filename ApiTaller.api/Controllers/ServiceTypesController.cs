using ApiTaller.Domain.Dtos.ServiceTypes;
using ApiTaller.Domain.Interfaces.Services.ServiceTypes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiTaller.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceTypesController : ControllerBase
    {
        private readonly ILogger<ServiceTypesController> _logger;
        private readonly IServiceTypeService _service;

        public ServiceTypesController(IServiceTypeService service, ILogger<ServiceTypesController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet("GetServiceType")]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _service.GetAllAsync(cancellationToken));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving service types");
            }
            return BadRequest();
        }

        [HttpGet("GetServiceTypeActive")]
        public async Task<IActionResult> GetActive(CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _service.GetAllActiveAsync(cancellationToken));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving active service types");
            }
            return BadRequest();
        }

        [HttpGet("GetServiceType/{id}")]
        public async Task<IActionResult> Get(int id, CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _service.GetByIdAsync(id, cancellationToken));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving service type with id {id}");
            }
            return BadRequest();
        }

        [HttpPost("SaveOrUpdate")]
        public async Task<IActionResult> Post(GetServiceTypeDto value, CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _service.CreateOrEditServiceType(value, cancellationToken));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving or updating service type");
            }
            return BadRequest();
        }
    }
}
