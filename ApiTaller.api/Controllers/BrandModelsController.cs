using ApiTaller.Domain.Dtos.BrandModels;
using ApiTaller.Domain.Interfaces.Services.BrandModels;
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
    public class BrandModelsController : ControllerBase
    {
        private readonly ILogger<BrandModelsController> _logger;
        private readonly IBrandModelsService _brandModelService;

        public BrandModelsController(ILogger<BrandModelsController> logger, IBrandModelsService brandModelService)
        {
            _logger = logger;
            _brandModelService = brandModelService;
        }
        // GET: api/<BrandModelsController>
        [HttpGet("GetBrandModels")]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _brandModelService.GetAllBrandModelsAsync(cancellationToken));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener los modelos de marca.");
            }
            return BadRequest();
        }

        [HttpGet("GetBrandModelsActive")]
        public async Task<IActionResult> GetActive(CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _brandModelService.GetAllBrandModelsActiveAsync(cancellationToken));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener los modelos de marca activos.");
            }
            return BadRequest();
        }

        // GET api/<BrandModelsController>/5
        [HttpGet("GetBrandModel/{id}")]
        public async Task<IActionResult> Get(int id, CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _brandModelService.GetBrandModelByIdAsync(id, cancellationToken));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener el modelo de marca con ID {id}.");
            }
            return BadRequest();
        }

        // POST api/<BrandModelsController>
        [HttpPost("CreateOrEditBrandModel")]
        public async Task<IActionResult> Post(GetBrandModelsDto value, CancellationToken cancellationToken  )
        {
            try
            {
                return Ok(await _brandModelService.CreateOrEditBrandModel(value, cancellationToken));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear o editar el modelo de marca.");
            }
            return BadRequest();
        }

        //// PUT api/<BrandModelsController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<BrandModelsController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
