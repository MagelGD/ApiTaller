using ApiTaller.Domain.Dtos.PaymentMethod;
using ApiTaller.Domain.Interfaces.Services.PaymentMethods;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiTaller.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentMethodController : ControllerBase
    {
        private readonly ILogger<PaymentMethodController> _logger;
        private readonly IPaymentMethodService _paymentMethodService;

        public PaymentMethodController(IPaymentMethodService paymentMethodService, ILogger<PaymentMethodController> logger)
        {
            _paymentMethodService = paymentMethodService;
            _logger = logger;
        }
        [HttpGet("GetPaymentMethods")]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _paymentMethodService.GetAllAsync(cancellationToken));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener los métodos de pago");
            }
            return BadRequest();
        }

        [HttpGet("GetPaymentMethodsActive")]
        public async Task<IActionResult> GetActive(CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _paymentMethodService.GetAllActiveAsync(cancellationToken));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener los métodos de pago activos");
            }
            return BadRequest();
        }

        // GET api/<PaymentMethodController>/5
        [HttpGet("GetPaymentMethod/{id}")]
        public async Task<IActionResult> Get(int id, CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _paymentMethodService.GetByIdAsync(id, cancellationToken));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener el método de pago con id {id}");
            }
            return BadRequest();
        }

        // POST api/<PaymentMethodController>
        [HttpPost("CreateOrEditPaymentMethod")]
        public async Task<IActionResult> Post(GetPaymentMethodDto value, CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _paymentMethodService.CreateOrEditPaymentMethod(value, cancellationToken));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear o editar el método de pago");
            }
            return BadRequest();
        }

        //// PUT api/<PaymentMethodController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<PaymentMethodController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
