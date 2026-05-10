using ApiTaller.Domain.Dtos.Supplier;
using ApiTaller.Domain.Interfaces.Services.Suppliers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiTaller.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SupplierController : ControllerBase
    {
        private readonly ILogger<SupplierController> _logger;
        private readonly ISupplierService _supplierService;

        public SupplierController(ISupplierService supplierService, ILogger<SupplierController> logger)
        {
            _supplierService = supplierService;
            _logger = logger;
        }
        // GET: api/<SupplierController>
        [HttpGet("GetSuppliers")]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _supplierService.GetAllAsync(cancellationToken));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener los proveedores");
            }
            return BadRequest();
        }

        [HttpGet("GetSuppliersActive")]
        public async Task<IActionResult> GetActive(CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _supplierService.GetAllActiveAsync(cancellationToken));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener los proveedores activos");
            }
            return BadRequest();
        }

        // GET api/<SupplierController>/5
        [HttpGet("GetSupplier/{id}")]
        public async Task<IActionResult> Get(int id, CancellationToken cancellationToken)
        {
            try
            {
               return Ok(await _supplierService.GetByIdAsync(id, cancellationToken));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener el proveedor con id {id}");
            }
            return BadRequest();
        }
        // POST api/<SupplierController>
        [HttpPost("CreateOrEditSupplier")]
        public async Task<IActionResult> Post(GetSupplierDto value, CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _supplierService.CreateOrEditSupplier(value, cancellationToken));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear o editar el proveedor");
            }
            return BadRequest();
        }

        //// PUT api/<SupplierController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<SupplierController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
