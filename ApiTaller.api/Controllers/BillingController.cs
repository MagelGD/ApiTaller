using ApiTaller.Domain.Dtos.Billing;
using ApiTaller.Domain.Interfaces.Services.Billing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiTaller.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillingController : ControllerBase
    {
        private readonly ILogger<BillingController> _logger;
        private readonly IBillingService _billingService;

        public BillingController(IBillingService billingService, ILogger<BillingController> logger)
        {
            _billingService = billingService;
            _logger = logger;
        }

        [HttpPost("SaveSale")]
        public async Task<IActionResult> Post(SaleDto value, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _billingService.SaveSaleAsync(value, cancellationToken);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al guardar la factura");
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("GetByWorkOrder/{workOrderId}")]
        public async Task<IActionResult> GetByWorkOrder(int workOrderId, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _billingService.GetByWorkOrderAsync(workOrderId, cancellationToken);
                if (result == null) return NotFound();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la factura");
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
