using ApiTaller.Domain.Dtos.Supplier;
using ApiTaller.Domain.Interfaces.Services.Suppliers;
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
    public class SupplierController : ControllerBase
    {
        private readonly ILogger<SupplierController> _logger;
        private readonly ISupplierService _supplierService;

        public SupplierController(ILogger<SupplierController> logger, ISupplierService supplierService)
        {
            _logger = logger;
            _supplierService = supplierService;
        }

        [HttpGet("GetSuppliers")]
        public async Task<IActionResult> GetSuppliers(CancellationToken cancellation)
        {
            try
            {
                return Ok(await _supplierService.GetAllAsync(cancellation));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener los proveedores");
            }
            return BadRequest();
        }

        [HttpGet("GetSuppliersActive")]
        public async Task<IActionResult> GetSuppliersActive(CancellationToken cancellation)
        {
            try
            {
                return Ok(await _supplierService.GetAllActiveAsync(cancellation));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener los proveedores activos");
            }
            return BadRequest();
        }

        [HttpGet("GetSupplier/{id}")]
        public async Task<IActionResult> GetSupplierById(int id, CancellationToken cancellation)
        {
            try
            {
                return Ok(await _supplierService.GetByIdAsync(id, cancellation));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener el proveedor con id {id}");
            }
            return BadRequest();
        }

        [HttpPost("CreateOrEditSupplier")]
        public async Task<IActionResult> CreateOrEditSupplier(GetSupplierDto value, CancellationToken cancellation)
        {
            try
            {
                return Ok(await _supplierService.CreateOrEditSupplier(value, cancellation));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear o editar el proveedor");
            }
            return BadRequest();
        }
    }
}
