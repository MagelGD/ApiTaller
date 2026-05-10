using ApiTaller.Domain.Dtos.Product;
using ApiTaller.Domain.Interfaces.Services.Products;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiTaller.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly IProductService _service;

        public ProductsController(ILogger<ProductsController> logger, IProductService service)
        {
            _logger = logger;
            _service = service;
        }
        // GET: api/<ProductsController>
        [HttpGet("GetProducts")]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _service.GetAllAsync(cancellationToken));
            }
            catch (Exception EX)
            {
                _logger.LogError(EX, "Error retrieving products");
            }
            return BadRequest();
        }

        [HttpGet("GetProductsActive")]
        public async Task<IActionResult> GetActive(CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _service.GetAllActiveAsync(cancellationToken));
            }
            catch (Exception EX)
            {
                _logger.LogError(EX, "Error retrieving active products");
            }
            return BadRequest();
        }

        // GET api/<ProductsController>/5
        [HttpGet("GetProduct/{id}")]
        public async Task<IActionResult> Get(int id, CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _service.GetByIdAsync(id, cancellationToken));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving product with id {id}");
            }
            return BadRequest();
        }

        // POST api/<ProductsController>
        [HttpPost("SaveOrEditProducts")]
        public async Task<IActionResult> Post(GetProductDto value, CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _service.CreateOrEditProductType(value, cancellationToken));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving or editing product");
            }
            return BadRequest();
        }

        //// PUT api/<ProductsController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<ProductsController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
