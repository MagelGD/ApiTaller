using ApiTaller.Domain.Dtos.Accounting;
using ApiTaller.Domain.Interfaces.Services.Accounting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace ApiTaller.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AccountingController : ControllerBase
    {
        private readonly IAccountingService _accountingService;

        public AccountingController(IAccountingService accountingService)
        {
            _accountingService = accountingService;
        }

        [HttpGet("PaymentSettings")]
        [ProducesResponseType(typeof(IEnumerable<MechanicPaymentSettingsDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPaymentSettings(CancellationToken ct)
        {
            IEnumerable<MechanicPaymentSettingsDto> result = await _accountingService.GetPaymentSettingsAsync(ct);
            return Ok(result);
        }

        [HttpPut("PaymentSettings")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> SavePaymentSettings([FromBody] MechanicPaymentSettingsDto dto, CancellationToken ct)
        {
            bool result = await _accountingService.SavePaymentSettingsAsync(dto, ct);
            return Ok(result);
        }

        [HttpGet("SalesSummary")]
        [ProducesResponseType(typeof(SalesSummaryDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetSalesSummary(
            [FromQuery] DateTime startDate, 
            [FromQuery] DateTime endDate, 
            [FromQuery] string status, 
            [FromQuery] int? mechanicId,
            [FromQuery] string? vehicleType,
            CancellationToken ct)
        {
            SalesSummaryDto result = await _accountingService.GetSalesSummaryAsync(startDate, endDate, status, mechanicId, vehicleType, ct);
            return Ok(result);
        }

        [HttpGet("PendingServices/{mechanicId}")]
        [ProducesResponseType(typeof(IEnumerable<PendingServiceDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPendingServices(
            int mechanicId, 
            [FromQuery] DateTime startDate, 
            [FromQuery] DateTime endDate, 
            CancellationToken ct)
        {
            IEnumerable<PendingServiceDto> result = await _accountingService.GetPendingServicesAsync(mechanicId, startDate, endDate, ct);
            return Ok(result);
        }

        [HttpPost("SettleServices")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> SettleServices([FromBody] SettleServicesRequest request, CancellationToken ct)
        {
            // Resolver id de usuario responsable de la liquidación
            string? userIdStr = User.FindFirst(ClaimTypes.Sid)?.Value 
                            ?? User.FindFirst(ClaimTypes.NameIdentifier)?.Value 
                            ?? "1";

            int.TryParse(userIdStr, out int responsibleUserId);
            if (responsibleUserId <= 0) responsibleUserId = 1;

            bool result = await _accountingService.SettleServicesAsync(
                request.MechanicId, 
                request.StartDate, 
                request.EndDate, 
                request.TotalAmount, 
                request.ServiceIds, 
                responsibleUserId, 
                ct);

            return Ok(result);
        }

        [HttpGet("SettlementHistory")]
        [ProducesResponseType(typeof(IEnumerable<MechanicSettlementDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetSettlementHistory([FromQuery] int? mechanicId, CancellationToken ct)
        {
            IEnumerable<MechanicSettlementDto> result = await _accountingService.GetSettlementHistoryAsync(mechanicId, ct);
            return Ok(result);
        }
    }

    public class SettleServicesRequest
    {
        public int MechanicId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalAmount { get; set; }
        public List<int> ServiceIds { get; set; } = null!;
    }
}
