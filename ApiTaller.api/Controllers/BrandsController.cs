using ApiTaller.Domain.Dtos.Brand;
using ApiTaller.Domain.Interfaces.Services.Brands;
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
    public class BrandsController : ControllerBase
    {
        private readonly ILogger<BrandsController> _logger;
        private readonly IBrandService _brandService;

        public BrandsController(ILogger<BrandsController> logger, IBrandService brandService)
        {
            _logger = logger;
            _brandService = brandService;
        }
        // GET: api/<BrandsController>
        [HttpGet("GetBrands")]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _brandService.GetAllBrandsAsync(cancellationToken));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener las marcas.");
            }
            return BadRequest();
        }

        [HttpGet("GetBrandsActive")]
        public async Task<IActionResult> GetActive(CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _brandService.GetAllBrandsActiveAsync(cancellationToken));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener las marcas activas.");
            }
            return BadRequest();
        }

        // GET api/<BrandsController>/5
        [HttpGet("GetBrands/{id}")]
        public async Task<IActionResult> Get(int id, CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _brandService.GetBrandByIdAsync(id, cancellationToken));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener la marca con ID {id}.");
            }
            return BadRequest();
        }

        // POST api/<BrandsController>
        [HttpPost("SaveOrEditBrands")]
        public async Task<IActionResult> Post(GetBrandDto value, CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _brandService.CreateOrEditBrand(value, cancellationToken));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al guardar o editar la marca.");
            }
            return BadRequest();
        }

        //// PUT api/<BrandsController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<BrandsController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
