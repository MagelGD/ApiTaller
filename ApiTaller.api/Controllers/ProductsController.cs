using ApiTaller.Domain.Dtos.Product;
using ApiTaller.Domain.Interfaces.Services.Products;
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
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly IProductService _service;

        public ProductsController(ILogger<ProductsController> logger, IProductService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet("GetProducts")]
        public async Task<IActionResult> GetProducts(CancellationToken cancellation)
        {
            try
            {
                return Ok(await _service.GetAllAsync(cancellation));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving products");
            }
            return BadRequest();
        }

        [HttpGet("GetProductsActive")]
        public async Task<IActionResult> GetProductsActive(CancellationToken cancellation)
        {
            try
            {
                return Ok(await _service.GetAllActiveAsync(cancellation));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving active products");
            }
            return BadRequest();
        }

        [HttpGet("GetProduct/{id}")]
        public async Task<IActionResult> GetProductById(int id, CancellationToken cancellation)
        {
            try
            {
                return Ok(await _service.GetByIdAsync(id, cancellation));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving product with id {id}");
            }
            return BadRequest();
        }

        [HttpPost("SaveOrEditProducts")]
        public async Task<IActionResult> SaveOrEditProducts(GetProductDto value, CancellationToken cancellation)
        {
            try
            {
                return Ok(await _service.CreateOrEditProductType(value, cancellation));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving or editing product");
            }
            return BadRequest();
        }
    }
}
