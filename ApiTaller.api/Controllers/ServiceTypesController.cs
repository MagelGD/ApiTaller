using ApiTaller.Domain.Dtos.ServiceTypes;
using ApiTaller.Domain.Interfaces.Services.ServiceTypes;
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
    public class ServiceTypesController : ControllerBase
    {
        private readonly ILogger<ServiceTypesController> _logger;
        private readonly IServiceTypeService _service;

        public ServiceTypesController(ILogger<ServiceTypesController> logger, IServiceTypeService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet("GetServiceType")]
        public async Task<IActionResult> GetServiceType(CancellationToken cancellation)
        {
            try
            {
                return Ok(await _service.GetAllAsync(cancellation));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving service types");
            }
            return BadRequest();
        }

        [HttpGet("GetServiceTypeActive")]
        public async Task<IActionResult> GetServiceTypeActive(CancellationToken cancellation)
        {
            try
            {
                return Ok(await _service.GetAllActiveAsync(cancellation));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving active service types");
            }
            return BadRequest();
        }

        [HttpGet("GetServiceType/{id}")]
        public async Task<IActionResult> GetServiceTypeById(int id, CancellationToken cancellation)
        {
            try
            {
                return Ok(await _service.GetByIdAsync(id, cancellation));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving service type with id {id}");
            }
            return BadRequest();
        }

        [HttpPost("SaveOrUpdate")]
        public async Task<IActionResult> SaveOrUpdate(GetServiceTypeDto value, CancellationToken cancellation)
        {
            try
            {
                return Ok(await _service.CreateOrEditServiceType(value, cancellation));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving or updating service type");
            }
            return BadRequest();
        }
    }
}
