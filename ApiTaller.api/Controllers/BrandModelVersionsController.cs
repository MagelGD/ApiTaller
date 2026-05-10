using ApiTaller.Domain.Dtos.BrandModelVersion;
using ApiTaller.Domain.Interfaces.Services.BrandModelVersion;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiTaller.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [Authorize]
    public class BrandModelVersionsController : ControllerBase
    {
        private readonly ILogger<BrandModelVersionsController> _logger;
        private readonly IBrandModelVersionService _brandModelVersionService;

        public BrandModelVersionsController(ILogger<BrandModelVersionsController> logger, IBrandModelVersionService brandModelVersionService)
        {
            _logger = logger;
            _brandModelVersionService = brandModelVersionService;
        }
        // GET: api/<BrandModelVersionsController>
        [HttpGet("GetBrandModelVersions")]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _brandModelVersionService.GetBrandModelVersionAsync(cancellationToken));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener las versiones de modelos de marca.");
            }
            return BadRequest();
        }

        [HttpGet("GetBrandModelVersionsActive")]
        public async Task<IActionResult> GetActive(CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _brandModelVersionService.GetBrandModelVersionActiveAsync(cancellationToken));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener las versiones de modelos de marca activos.");
            }
            return BadRequest();
        }

        // GET api/<BrandModelVersionsController>/5
        [HttpGet("GetBrandModelVersion/{id}")]
        public async Task<IActionResult> Get(int id, CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _brandModelVersionService.GetBrandModelVersionByIdAsync(id, cancellationToken));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener la versión del modelo de marca con ID {id}.");
            }
            return BadRequest();
        }

        // POST api/<BrandModelVersionsController>
        [HttpPost("CreateOrEditBrandModelVersion")]
        public async Task<IActionResult> Post(GetBrandModelVersionDto value, CancellationToken cancellationToken  )
        {
            try
            {
                return Ok(await _brandModelVersionService.CreateOrEditBrandModelVersionAsync(value, cancellationToken));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear o editar la versión del modelo de marca.");
            }
            return BadRequest();
        }

        //// PUT api/<BrandModelVersionsController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<BrandModelVersionsController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
