using ApiTaller.Domain.Dtos.PaymentMethod;
using ApiTaller.Domain.Interfaces.Services.PaymentMethods;
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
    public class PaymentMethodController : ControllerBase
    {
        private readonly ILogger<PaymentMethodController> _logger;
        private readonly IPaymentMethodService _paymentMethodService;

        public PaymentMethodController(ILogger<PaymentMethodController> logger, IPaymentMethodService paymentMethodService)
        {
            _logger = logger;
            _paymentMethodService = paymentMethodService;
        }

        [HttpGet("GetPaymentMethods")]
        public async Task<IActionResult> GetPaymentMethods(CancellationToken cancellation)
        {
            try
            {
                return Ok(await _paymentMethodService.GetAllAsync(cancellation));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener los métodos de pago");
            }
            return BadRequest();
        }

        [HttpGet("GetPaymentMethodsActive")]
        public async Task<IActionResult> GetPaymentMethodsActive(CancellationToken cancellation)
        {
            try
            {
                return Ok(await _paymentMethodService.GetAllActiveAsync(cancellation));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener los métodos de pago activos");
            }
            return BadRequest();
        }

        [HttpGet("GetPaymentMethod/{id}")]
        public async Task<IActionResult> GetPaymentMethodById(int id, CancellationToken cancellation)
        {
            try
            {
                return Ok(await _paymentMethodService.GetByIdAsync(id, cancellation));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener el método de pago con ID {id}");
            }
            return BadRequest();
        }

        [HttpPost("CreateOrEditPaymentMethod")]
        public async Task<IActionResult> CreateOrEditPaymentMethod(GetPaymentMethodDto value, CancellationToken cancellation)
        {
            try
            {
                return Ok(await _paymentMethodService.CreateOrEditPaymentMethod(value, cancellation));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear o editar el método de pago");
            }
            return BadRequest();
        }
    }
}
