using ApiTaller.Domain.Dtos.Billing;
using ApiTaller.Domain.Interfaces.Services.Billing;
using ApiTaller.Domain.Interfaces.Services.Email;
using ApiTaller.Domain.Interfaces.Services.WorkshopSettings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly IWorkshopSettingsService _workshopSettingsService;

        public BillingController(
            ILogger<BillingController> logger, 
            IBillingService billingService, 
            IEmailService emailService,
            IWorkshopSettingsService workshopSettingsService)
        {
            _logger = logger;
            _billingService = billingService;
            _emailService = emailService;
            _workshopSettingsService = workshopSettingsService;
        }

        [HttpPost("SendInvoiceEmail")]
        public async Task<IActionResult> SendInvoiceEmail([FromBody] SendInvoiceEmailDto request, CancellationToken cancellation)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.ToEmail))
                {
                    return BadRequest(new { message = "Debe proporcionar un correo electrónico válido." });
                }

                // 1. Obtener configuraciones de marca del taller (Logo, Nombre, Eslogan)
                var settingsList = await _workshopSettingsService.GetAllAsync(cancellation);
                var settingsMap = settingsList.ToDictionary(s => s.SettingKey, s => s.SettingValue);

                string workshopName = settingsMap.TryGetValue("workshop_name", out var wn) && !string.IsNullOrWhiteSpace(wn) ? wn : "DAVID MOTOS";
                string workshopSlogan = settingsMap.TryGetValue("workshop_slogan", out var ws) && !string.IsNullOrWhiteSpace(ws) ? ws : "SERVICIO TÉCNICO ESPECIALIZADO";
                string? logoBase64 = settingsMap.TryGetValue("logo", out var lg) && !string.IsNullOrWhiteSpace(lg) ? lg : null;

                string customerName = !string.IsNullOrWhiteSpace(request.CustomerName) ? request.CustomerName : "Estimado(a) Cliente";
                string vehiclePlate = !string.IsNullOrWhiteSpace(request.VehiclePlate) ? request.VehiclePlate.ToUpper() : "N/A";
                string vehicleModel = !string.IsNullOrWhiteSpace(request.VehicleModel) ? request.VehicleModel : "Vehículo";
                string totalFormatted = request.TotalAmount.HasValue ? $"${request.TotalAmount.Value:N0}" : "";
                string invoiceNo = request.SaleId > 0 ? request.SaleId.ToString("D3") : "001";

                string logoHtml = "";
                if (!string.IsNullOrWhiteSpace(logoBase64))
                {
                    string src = logoBase64.StartsWith("data:image") ? logoBase64 : $"data:image/png;base64,{logoBase64}";
                    logoHtml = $"<div style='margin-bottom: 12px;'><img src='{src}' alt='{workshopName}' style='max-height: 65px; max-width: 200px; object-fit: contain;' /></div>";
                }

                // 2. Procesar PDF adjunto
                byte[]? pdfContent = null;
                if (!string.IsNullOrWhiteSpace(request.PdfBase64))
                {
                    pdfContent = Convert.FromBase64String(request.PdfBase64);
                }

                string pdfFileName = !string.IsNullOrWhiteSpace(request.FileName) 
                    ? request.FileName 
                    : $"Factura_{vehiclePlate}_N{invoiceNo}.pdf";

                List<EmailAttachment> attachments = new List<EmailAttachment>();
                if (pdfContent != null && pdfContent.Length > 0)
                {
                    attachments.Add(new EmailAttachment
                    {
                        FileName = pdfFileName,
                        Content = pdfContent,
                        ContentType = "application/pdf"
                    });
                }

                // 3. Construir plantilla HTML profesional y responsiva
                string htmlBody = $@"
<div style=""background-color: #f1f5f9; padding: 30px 15px; font-family: 'Segoe UI', Roboto, Helvetica, Arial, sans-serif; color: #1e293b;"">
    <table align=""center"" border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""max-width: 600px; background-color: #ffffff; border-radius: 16px; overflow: hidden; box-shadow: 0 10px 25px rgba(0,0,0,0.08); border: 1px solid #e2e8f0;"">
        <!-- HEADER CORPORATIVO -->
        <tr>
            <td style=""background: linear-gradient(135deg, #0f172a 0%, #1e293b 100%); padding: 32px 25px; text-align: center; color: #ffffff;"">
                {logoHtml}
                <h1 style=""margin: 0; font-size: 22px; font-weight: 800; letter-spacing: 0.5px; color: #ffffff; text-transform: uppercase;"">{workshopName}</h1>
                <p style=""margin: 6px 0 0 0; font-size: 11px; color: #38bdf8; text-transform: uppercase; letter-spacing: 2px; font-weight: 700;"">{workshopSlogan}</p>
            </td>
        </tr>

        <!-- CONTENIDO PRINCIPAL -->
        <tr>
            <td style=""padding: 30px 28px;"">
                <h2 style=""margin: 0 0 12px 0; font-size: 19px; color: #0f172a; font-weight: 700;"">¡Hola, {customerName}!</h2>
                <p style=""margin: 0 0 22px 0; font-size: 14px; line-height: 1.6; color: #475569;"">
                    Te compartimos el comprobante y la <strong>factura oficial</strong> correspondiente a los servicios realizados a tu vehículo en nuestro taller.
                </p>

                <!-- TARJETA RESUMEN DEL VEHÍCULO -->
                <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""background-color: #f8fafc; border: 1px solid #e2e8f0; border-radius: 12px; margin-bottom: 24px; overflow: hidden;"">
                    <tr>
                        <td style=""background-color: #0ea5e9; height: 4px;"" colspan=""2""></td>
                    </tr>
                    <tr>
                        <td style=""padding: 16px 20px 8px 20px; font-size: 12px; color: #64748b; text-transform: uppercase; font-weight: 700; letter-spacing: 0.5px;"">Detalles del Servicio</td>
                        <td style=""padding: 16px 20px 8px 20px; text-align: right; font-size: 13px; font-weight: 700; color: #0ea5e9;"">Factura #{invoiceNo}</td>
                    </tr>
                    <tr>
                        <td style=""padding: 8px 20px; font-size: 14px; color: #334155;""><strong>🏷️ Placa:</strong></td>
                        <td style=""padding: 8px 20px; text-align: right; font-size: 15px; font-weight: 800; color: #0f172a; letter-spacing: 1px;"">{vehiclePlate}</td>
                    </tr>
                    <tr>
                        <td style=""padding: 8px 20px; font-size: 14px; color: #334155;""><strong>🏍️ Vehículo / Modelo:</strong></td>
                        <td style=""padding: 8px 20px; text-align: right; font-size: 14px; color: #334155; font-weight: 600;"">{vehicleModel}</td>
                    </tr>
                    <tr>
                        <td style=""padding: 8px 20px 16px 20px; font-size: 14px; color: #334155;""><strong>💰 Total Facturado:</strong></td>
                        <td style=""padding: 8px 20px 16px 20px; text-align: right; font-size: 17px; font-weight: 800; color: #10b981;"">{totalFormatted} COP</td>
                    </tr>
                </table>

                <!-- BANNER DE ARCHIVO ADJUNTO -->
                <div style=""background-color: #eff6ff; border: 1px dashed #3b82f6; border-radius: 10px; padding: 14px 18px; margin-bottom: 24px; text-align: center;"">
                    <p style=""margin: 0; font-size: 13px; color: #1e40af; font-weight: 600;"">
                        📎 El documento oficial con el desglose de mano de obra y repuestos está adjunto en formato PDF.
                    </p>
                </div>

                <p style=""margin: 0; font-size: 13px; color: #64748b; line-height: 1.5;"">
                    Agradecemos tu preferencia y confianza en <strong>{workshopName}</strong>. ¡Estamos siempre a tu disposición!
                </p>
            </td>
        </tr>

        <!-- FOOTER -->
        <tr>
            <td style=""background-color: #f8fafc; border-top: 1px solid #e2e8f0; padding: 18px 25px; text-align: center; font-size: 12px; color: #94a3b8;"">
                <p style=""margin: 0 0 4px 0; font-weight: 600; color: #64748b;"">{workshopName} — {workshopSlogan}</p>
                <p style=""margin: 0;"">Este es un correo automático de notificación y facturación. Por favor no responder a esta dirección.</p>
            </td>
        </tr>
    </table>
</div>";

                EmailRequest emailRequest = new EmailRequest
                {
                    To = request.ToEmail,
                    Subject = $"Factura de Servicio N° {invoiceNo} | Placa {vehiclePlate} - {workshopName}",
                    Body = htmlBody,
                    Attachments = attachments
                };

                await _emailService.SendEmailAsync(emailRequest, cancellation);
                return Ok(new { success = true, message = "Factura enviada exitosamente por correo." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al enviar el correo de la factura");
                return StatusCode(500, new { message = $"Error al enviar el correo: {ex.Message}" });
            }
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
                return StatusCode(500, new { message = "Error al guardar la factura", error = ex.Message });
            }
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
                return StatusCode(500, new { message = "Error al obtener la factura", error = ex.Message });
            }
        }
    }
}
