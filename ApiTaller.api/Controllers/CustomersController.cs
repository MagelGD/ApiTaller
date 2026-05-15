using ApiTaller.Domain.Dtos.Customer;
using ApiTaller.Domain.Interfaces.Services.Customers;
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
    public class CustomersController : ControllerBase
    {
        private readonly ILogger<CustomersController> _logger;
        private readonly ICustomerService _customerService;

        public CustomersController(ILogger<CustomersController> logger, ICustomerService customerService)
        {
            _logger = logger;
            _customerService = customerService;
        }

        [HttpGet("GetCustomers")]
        public async Task<IActionResult> GetCustomers(CancellationToken cancellation)
        {
            try
            {
                return Ok(await _customerService.GetAllCustomersAsync(cancellation));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener los clientes.");
            }
            return BadRequest();
        }

        [HttpGet("GetCustomer/{id}")]
        public async Task<IActionResult> GetCustomerById(int id, CancellationToken cancellation)
        {
            try
            {
                return Ok(await _customerService.GetCustomerByIdAsync(id, cancellation));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener el cliente con ID {id}.");
            }
            return BadRequest();
        }

        [HttpPost("SaveOrEditCustomer")]
        public async Task<IActionResult> SaveOrEditCustomer(GetCustomerDto value, CancellationToken cancellation)
        {
            try
            {
                return Ok(await _customerService.CreateOrEditCustomer(value, cancellation));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al guardar o editar el cliente.");
            }
            return BadRequest();
        }
    }
}
