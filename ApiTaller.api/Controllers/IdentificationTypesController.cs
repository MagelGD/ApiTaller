using ApiTaller.Domain.Dtos.IdentificationTypes;
using ApiTaller.Domain.Interfaces.Services.IdentificationTypes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using ApiTaller.Core.Services.IdentificationTypes;

namespace ApiTaller.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class IdentificationTypesController : ControllerBase
    {
        private readonly ILogger<IdentificationTypesController> _logger;
        private readonly IIdentificationTypesService _service;
 
        public IdentificationTypesController(ILogger<IdentificationTypesController> logger, IIdentificationTypesService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet("GetIdentificationTypes")]
        public async Task<IActionResult> GetIdentificationTypes(CancellationToken cancellation)
        {
            try
            {
                return Ok(await _service.GetAllAsync(cancellation));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting identification types");
            }
            return BadRequest();
        }

        [HttpGet("GetIdentificationTypesActive")]
        public async Task<IActionResult> GetIdentificationTypesActive(CancellationToken cancellation)
        {
            try
            {
                return Ok(await _service.GetAllActiveAsync(cancellation));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting active identification types");
            }
            return BadRequest();
        }

        [HttpGet("GetIdentificationType/{id}")]
        public async Task<IActionResult> GetIdentificationTypeById(int id, CancellationToken cancellation)
        {
            try
            {
                return Ok(await _service.GetByIdAsync(id, cancellation));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting identification type by id");
            }
            return BadRequest();
        }

        [HttpPost("SaveOrEditIdentificationTypes")]
        public async Task<IActionResult> SaveOrEditIdentificationTypes(GetIdentificationTypeDto saveData, CancellationToken cancellation)
        {
            try
            {
                return Ok(await _service.CreateOrEditIdentificationType(saveData, cancellation));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving or editing identification type");
            }
            return BadRequest();
        }
    }
}
