using ApiTaller.Domain.Dtos.Billing;
using ApiTaller.Domain.Interfaces.Services.Billing;
using ApiTaller.Domain.Interfaces.Services.Email;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ApiTaller.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BillingController : ControllerBase
    {
        private readonly ILogger<BillingController> _logger;
        private readonly IBillingService _billingService;
        private readonly IEmailService _emailService;

        public BillingController(ILogger<BillingController> logger, IBillingService billingService, IEmailService emailService)
        {
            _logger = logger;
            _billingService = billingService;
            _emailService = emailService;
        }

        [HttpPost("SendInvoiceEmail")]
        public async Task<IActionResult> SendInvoiceEmail([FromBody] SendInvoiceEmailDto request, CancellationToken cancellation)
        {
            try
            {
                var emailRequest = new EmailRequest
                {
                    To = request.ToEmail,
                    Subject = $"Factura de Venta - David Motos",
                    Body = $@"
                        <div style='font-family: sans-serif; max-width: 600px; margin: auto; border: 1px solid #eee; padding: 20px;'>
                            <h2 style='color: #0ea5e9;'>¡Hola!</h2>
                            <p>Te enviamos adjunta la factura de tu servicio en <strong>David Motos</strong>.</p>
                            <p>Gracias por confiar en nosotros.</p>
                            <hr style='border: 0; border-top: 1px solid #eee; margin: 20px 0;'>
                            <p style='font-size: 12px; color: #666;'>Este es un correo automático, por favor no respondas a este mensaje.</p>
                        </div>",
                    Attachments = new List<EmailAttachment>
                    {
                        new EmailAttachment
                        {
                            FileName = request.FileName ?? "Factura_David_Motos.pdf",
                            Content = Convert.FromBase64String(request.PdfBase64),
                            ContentType = "application/pdf"
                        }
                    }
                };

                await _emailService.SendEmailAsync(emailRequest, cancellation);
                return Ok(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al enviar el correo de la factura");
            }
            return BadRequest();
        }

        [HttpPost("SaveSale")]
        public async Task<IActionResult> SaveSale(SaleDto value, CancellationToken cancellation)
        {
            try
            {
                return Ok(await _billingService.SaveSaleAsync(value, cancellation));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al guardar la factura");
            }
            return BadRequest();
        }

        [HttpGet("GetByWorkOrder/{workOrderId}")]
        public async Task<IActionResult> GetByWorkOrder(int workOrderId, CancellationToken cancellation)
        {
            try
            {
                return Ok(await _billingService.GetByWorkOrderAsync(workOrderId, cancellation));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la factura");
            }
            return BadRequest();
        }
    }
}
