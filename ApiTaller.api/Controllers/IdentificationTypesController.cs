using ApiTaller.Domain.Dtos.IdentificationTypes;
using ApiTaller.Domain.Interfaces.Services.IdentificationTypes;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiTaller.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentificationTypesController : ControllerBase
    {
        private readonly ILogger<IdentificationTypesController> _logger;
        private readonly IIdentificationTypesService _service;

        public IdentificationTypesController(IIdentificationTypesService service, ILogger<IdentificationTypesController> logger)
        {
            _service = service;
            _logger = logger;
        }
        [HttpGet("GetIdentificationTypes")]
        public async Task<IActionResult> Get(CancellationToken cancellation)
        {
            try
            {
                var result = await _service.GetAllAsync(cancellation);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting identification types");
            }
            return BadRequest();
        }

        [HttpGet("GetIdentificationTypesActives")]
        public async Task<IActionResult> GetIdentificationTypesActives(CancellationToken cancellation)
        {
            try
            {
                var result = await _service.GetAllActiveAsync(cancellation);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting identification types");
            }
            return BadRequest();
        }

        [HttpGet("GetIdentificationType/{id}")]
        public async Task<IActionResult> Get(int id, CancellationToken cancellation)
        {
            try
            {
                return Ok(await _service.GetByIdAsync(id, cancellation));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting identification type with id {id}");
            }
            return BadRequest();
        }

        
        [HttpPost("SaveOrEditIdentificationTypes")]
        public async Task<IActionResult> Post(GetIdentificationTypeDto saveData, CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _service.CreateOrEditIdentificationType(saveData, cancellationToken));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving or editing identification type");
            }
            return BadRequest();
        }

        //// PUT api/<IdentificationTypesController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<IdentificationTypesController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
