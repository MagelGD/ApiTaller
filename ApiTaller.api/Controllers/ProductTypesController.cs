using ApiTaller.Domain.Dtos.ProductType;
using ApiTaller.Domain.Interfaces.Services.ProductTypes;
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
    public class ProductTypesController : ControllerBase
    {
        private readonly ILogger<ProductTypesController> _logger;
        private readonly IProductTypeService _service;

        public ProductTypesController(ILogger<ProductTypesController> logger, IProductTypeService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll(CancellationToken cancellation)
        {
            try
            {
                return Ok(await _service.GetProductType(cancellation));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all product types");
            }
            return BadRequest();
        }

        [HttpGet("GetActive")]
        public async Task<IActionResult> GetActive(CancellationToken cancellation)
        {
            try
            {
                return Ok(await _service.GetProductTypeActive(cancellation));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving active product types");
            }
            return BadRequest();
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellation)
        {
            try
            {
                return Ok(await _service.GetProductTypeById(id, cancellation));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving product type with id {id}");
            }
            return BadRequest();
        }

        [HttpPost("SaveOrUpdate")]
        public async Task<IActionResult> SaveOrUpdate(GetProductTypeDto value, CancellationToken cancellation)
        {
            try
            {
                return Ok(await _service.CreateOrEditProductType(value, cancellation));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving or updating product type");
            }
            return BadRequest();
        }
    }
}
