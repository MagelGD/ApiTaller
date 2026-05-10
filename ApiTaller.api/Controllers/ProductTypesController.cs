using ApiTaller.Domain.Dtos.ProductType;
using ApiTaller.Domain.Interfaces.Services.ProductTypes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiTaller.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductTypesController : ControllerBase
    {
        private readonly ILogger<ProductTypesController> _logger;
        private readonly IProductTypeService _service;

        public ProductTypesController(IProductTypeService service, ILogger<ProductTypesController> logger)
        {
            _service = service;
            _logger = logger;
        }


        [HttpGet("GetProductType")]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _service.GetAllAsync(cancellationToken));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving product types");
            }
            return BadRequest();
        }

        [HttpGet("GetProductTypeActive")]
        public async Task<IActionResult> GetActive(CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _service.GetAllActiveAsync(cancellationToken));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving active product types");
            }
            return BadRequest();
        }

        // GET api/<ProductTypesController>/5
        [HttpGet("GetProductType/{id}")]
        public async Task<IActionResult> Get(int id, CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _service.GetByIdAsync(id, cancellationToken));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving product type with id {id}");
            }
            return BadRequest();
        }

        // POST api/<ProductTypesController>
        [HttpPost("SaveOrUpdate")]
        public async Task<IActionResult> Post(GetProductTypeDto value, CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _service.CreateOrEditProductType(value, cancellationToken));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving or updating product type");
            }
            return BadRequest();
        }

        //// PUT api/<ProductTypesController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<ProductTypesController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
