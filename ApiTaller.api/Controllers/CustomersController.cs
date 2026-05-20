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
                return Ok(await _customerService.GetAllAsync(cancellation));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener los clientes.");
            }
            return BadRequest();
        }

        [HttpGet("GetCustomersActive")]
        public async Task<IActionResult> GetCustomersActive(CancellationToken cancellation)
        {
            try
            {
                return Ok(await _customerService.GetAllActiveAsync(cancellation));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener los clientes activos.");
            }
            return BadRequest();
        }

        [HttpGet("GetCustomer/{id}")]
        public async Task<IActionResult> GetCustomerById(int id, CancellationToken cancellation)
        {
            try
            {
                return Ok(await _customerService.GetByIdAsync(id, cancellation));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener el cliente con ID {id}.");
            }
            return BadRequest();
        }

        [HttpPost("CreateOrEditCustomer")]
        public async Task<IActionResult> CreateOrEditCustomer(GetCustomerDto value, CancellationToken cancellation)
        {
            try
            {
                return Ok(await _customerService.CreateOrEditCustomer(value, cancellation));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al guardar o editar el cliente.");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("ResendWelcomeEmail/{id}")]
        public async Task<IActionResult> ResendWelcomeEmail(int id, CancellationToken cancellation)
        {
            try
            {
                bool success = await _customerService.ResendWelcomeEmailAsync(id, cancellation);
                if (success)
                {
                    return Ok(new { message = "Correo reenviado exitosamente." });
                }
                return BadRequest(new { message = "No se pudo reenviar el correo de bienvenida." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al reenviar correo de bienvenida para el cliente con ID {id}.");
            }
            return BadRequest();
        }
    }
}
