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

        public ServiceCatalogsController(IServiceCatalogService service, ILogger<ServiceCatalogsController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet("GetServiceCatalog")]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _service.GetAllAsync(cancellationToken));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving service catalogs");
            }
            return BadRequest();
        }

        [HttpGet("GetServiceCatalogActive")]
        public async Task<IActionResult> GetActive(CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _service.GetAllActiveAsync(cancellationToken));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving active service catalogs");
            }
            return BadRequest();
        }

        [HttpGet("GetServiceCatalog/{id}")]
        public async Task<IActionResult> Get(int id, CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _service.GetByIdAsync(id, cancellationToken));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving service catalog with id {id}");
            }
            return BadRequest();
        }

        [HttpPost("SaveOrUpdate")]
        public async Task<IActionResult> Post(GetServiceCatalogDto value, CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _service.CreateOrEditServiceCatalog(value, cancellationToken));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving or updating service catalog");
            }
            return BadRequest();
        }
    }
}
