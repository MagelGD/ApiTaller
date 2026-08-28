using ApiTaller.Domain.Dtos.Quotations;
using ApiTaller.Domain.Interfaces.Repositories.Quotations;
using ApiTaller.Domain.Interfaces.Services.Email;
using ApiTaller.Domain.Interfaces.Services.Quotations;
using ApiTaller.Domain.Interfaces.Services.WorkshopSettings;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiTaller.Core.Services.Quotations
{
    public class QuotationService : IQuotationService
    {
        private readonly IQuotationRepository _quotationRepository;
        private readonly IEmailService _emailService;
        private readonly IWorkshopSettingsService _workshopSettingsService;
        private readonly ILogger<QuotationService> _logger;

        public QuotationService(
            IQuotationRepository quotationRepository,
            IEmailService emailService,
            IWorkshopSettingsService workshopSettingsService,
            ILogger<QuotationService> logger)
        {
            _quotationRepository = quotationRepository;
            _emailService = emailService;
            _workshopSettingsService = workshopSettingsService;
            _logger = logger;
        }

        public async Task<IEnumerable<QuotationDto>> GetAllAsync(string? status, DateTime? startDate, DateTime? endDate, CancellationToken cancellation)
        {
            return await _quotationRepository.GetAllAsync(status, startDate, endDate, cancellation);
        }

        public async Task<QuotationDto?> GetByIdAsync(int id, CancellationToken cancellation)
        {
            return await _quotationRepository.GetByIdAsync(id, cancellation);
        }

        public async Task<QuotationDto?> GetByTokenAsync(string token, CancellationToken cancellation)
        {
            return await _quotationRepository.GetByTokenAsync(token, cancellation);
        }

        public async Task<IEnumerable<QuotationDto>> GetMyQuotationsAsync(int customerId, CancellationToken cancellation)
        {
            return await _quotationRepository.GetByCustomerIdAsync(customerId, cancellation);
        }

        public async Task<QuotationDto> CreateAsync(QuotationCreateDto dto, CancellationToken cancellation)
        {
            var created = await _quotationRepository.CreateAsync(dto, cancellation);
            var quoteDto = await _quotationRepository.GetByIdAsync(created.Id, cancellation);

            if (quoteDto == null)
            {
                quoteDto = new QuotationDto
                {
                    Id = created.Id,
                    QuotationNumber = created.QuotationNumber,
                    WorkshopId = created.WorkshopId,
                    CustomerId = created.CustomerId,
                    ProspectName = created.ProspectName,
                    ProspectEmail = created.ProspectEmail,
                    ProspectPhone = created.ProspectPhone,
                    ProspectVehicleInfo = created.ProspectVehicleInfo,
                    Status = created.Status,
                    Subtotal = created.Subtotal,
                    DiscountPercent = created.DiscountPercent,
                    DiscountAmount = created.DiscountAmount,
                    Total = created.Total,
                    ExpirationDate = created.ExpirationDate,
                    PublicToken = created.PublicToken,
                    Observations = created.Observations,
                    TermsAndConditions = created.TermsAndConditions,
                    CreatedAt = created.CreatedAt
                };
            }

            if (dto.SendEmailImmediately && !string.IsNullOrWhiteSpace(quoteDto.ClientDisplayEmail))
            {
                _ = Task.Run(async () =>
                {
                    try
                    {
                        using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(10));
                        await SendEmailAsync(new SendQuotationEmailDto
                        {
                            QuotationId = quoteDto.Id,
                            ToEmail = quoteDto.ClientDisplayEmail,
                            CustomerName = quoteDto.ClientDisplayName
                        }, cts.Token);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogWarning(ex, "No se pudo enviar el correo en segundo plano para la cotización {QuotationId}", quoteDto.Id);
                    }
                });
            }

            return quoteDto;
        }

        public async Task<bool> UpdateAsync(int id, QuotationCreateDto dto, CancellationToken cancellation)
        {
            return await _quotationRepository.UpdateAsync(id, dto, cancellation);
        }

        public async Task<bool> SendEmailAsync(SendQuotationEmailDto emailDto, CancellationToken cancellation)
        {
            var quote = await _quotationRepository.GetByIdAsync(emailDto.QuotationId, cancellation);
            if (quote == null) throw new InvalidOperationException("Cotización no encontrada");

            // 1. Obtener configuraciones del taller
            var settingsList = await _workshopSettingsService.GetAllAsync(cancellation);
            var settingsMap = settingsList.ToDictionary(s => s.SettingKey, s => s.SettingValue);

            string workshopName = settingsMap.TryGetValue("workshop_name", out var wn) && !string.IsNullOrWhiteSpace(wn) ? wn : "DAVID MOTOS";
            string workshopSlogan = settingsMap.TryGetValue("workshop_slogan", out var ws) && !string.IsNullOrWhiteSpace(ws) ? ws : "SERVICIO TÉCNICO ESPECIALIZADO";
            string? logoBase64 = settingsMap.TryGetValue("logo", out var lg) && !string.IsNullOrWhiteSpace(lg) ? lg : null;

            string clientName = !string.IsNullOrWhiteSpace(emailDto.CustomerName) ? emailDto.CustomerName : quote.ClientDisplayName;
            string vehicleInfo = quote.VehicleDisplayInfo;
            string totalFormatted = $"${quote.Total:N0} COP";
            string quoteNo = quote.QuotationNumber;

            string logoHtml = "";
            if (!string.IsNullOrWhiteSpace(logoBase64))
            {
                string src = logoBase64.StartsWith("data:image") ? logoBase64 : $"data:image/png;base64,{logoBase64}";
                logoHtml = $"<div style='margin-bottom: 12px;'><img src='{src}' alt='{workshopName}' style='max-height: 65px; max-width: 200px; object-fit: contain;' /></div>";
            }

            // 2. Generar tabla HTML de ítems
            string itemsRows = "";
            foreach (var item in quote.Details)
            {
                string typeLabel = item.ItemType == "Service" ? "🔧 Servicio" : "📦 Repuesto";
                itemsRows += $@"
                <tr style=""border-bottom: 1px solid #f1f5f9;"">
                    <td style=""padding: 10px 12px; font-size: 13px; color: #334155;"">
                        <span style=""display: inline-block; font-size: 10px; font-weight: 700; padding: 2px 6px; border-radius: 4px; background: #e2e8f0; color: #475569; margin-bottom: 4px;"">{typeLabel}</span><br/>
                        <strong>{item.Description}</strong>
                    </td>
                    <td style=""padding: 10px 12px; font-size: 13px; color: #475569; text-align: center;"">{item.Quantity}</td>
                    <td style=""padding: 10px 12px; font-size: 13px; color: #475569; text-align: right;"">${item.UnitPrice:N0}</td>
                    <td style=""padding: 10px 12px; font-size: 13px; font-weight: 700; color: #0f172a; text-align: right;"">${item.Total:N0}</td>
                </tr>";
            }

            // 3. Procesar adjuntos (PDF o Fotos)
            List<EmailAttachment> attachments = new List<EmailAttachment>();
            if (!string.IsNullOrWhiteSpace(emailDto.PdfBase64))
            {
                try
                {
                    byte[] pdfBytes = Convert.FromBase64String(emailDto.PdfBase64);
                    attachments.Add(new EmailAttachment
                    {
                        FileName = $"Cotizacion_{quoteNo}.pdf",
                        Content = pdfBytes,
                        ContentType = "application/pdf"
                    });
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, "No se pudo decodificar el PDF base64 para cotización {QuotationId}", quote.Id);
                }
            }

            // Si hay evidencias o fotos adjuntas en la cotización
            foreach (var att in quote.Attachments.Where(a => !string.IsNullOrWhiteSpace(a.DataBase64)))
            {
                try
                {
                    string rawBase64 = att.DataBase64!.Contains(",") ? att.DataBase64.Split(',')[1] : att.DataBase64;
                    byte[] bytes = Convert.FromBase64String(rawBase64);
                    attachments.Add(new EmailAttachment
                    {
                        FileName = att.FileName,
                        Content = bytes,
                        ContentType = att.ContentType ?? "image/jpeg"
                    });
                }
                catch { }
            }

            // 4. Plantilla de correo
            string htmlBody = $@"
<div style=""background-color: #f8fafc; padding: 30px 15px; font-family: 'Segoe UI', Roboto, Helvetica, Arial, sans-serif; color: #1e293b;"">
    <table align=""center"" border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""max-width: 650px; background-color: #ffffff; border-radius: 16px; overflow: hidden; box-shadow: 0 10px 25px rgba(0,0,0,0.06); border: 1px solid #e2e8f0;"">
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
                <div style=""display: flex; justify-content: space-between; align-items: center; margin-bottom: 18px;"">
                    <h2 style=""margin: 0; font-size: 19px; color: #0f172a; font-weight: 700;"">¡Hola, {clientName}!</h2>
                    <span style=""background: #e0f2fe; color: #0369a1; font-weight: 800; padding: 6px 12px; border-radius: 20px; font-size: 12px;"">Cotización {quoteNo}</span>
                </div>

                <p style=""margin: 0 0 20px 0; font-size: 14px; line-height: 1.6; color: #475569;"">
                    Te presentamos el presupuesto formal preparado por nuestro equipo para tu solicitud. A continuación encontrarás el desglose de productos y servicios:
                </p>

                <!-- TABLA DE DETALLE -->
                <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""border-collapse: collapse; background: #ffffff; border: 1px solid #e2e8f0; border-radius: 10px; overflow: hidden; margin-bottom: 20px;"">
                    <thead>
                        <tr style=""background: #f1f5f9; color: #475569; font-size: 12px; font-weight: 700; text-transform: uppercase; letter-spacing: 0.5px;"">
                            <th style=""padding: 10px 12px; text-align: left;"">Descripción</th>
                            <th style=""padding: 10px 12px; text-align: center;"">Cant.</th>
                            <th style=""padding: 10px 12px; text-align: right;"">Precio Unit.</th>
                            <th style=""padding: 10px 12px; text-align: right;"">Total</th>
                        </tr>
                    </thead>
                    <tbody>
                        {itemsRows}
                    </tbody>
                </table>

                <!-- RESUMEN DE TOTALES -->
                <table align=""right"" border=""0"" cellpadding=""0"" cellspacing=""0"" style=""width: 250px; margin-bottom: 25px;"">
                    <tr>
                        <td style=""padding: 4px 8px; font-size: 13px; color: #64748b;"">Subtotal:</td>
                        <td style=""padding: 4px 8px; font-size: 13px; color: #334155; text-align: right; font-weight: 600;"">${quote.Subtotal:N0}</td>
                    </tr>
                    {(quote.DiscountAmount > 0 ? $@"
                    <tr>
                        <td style=""padding: 4px 8px; font-size: 13px; color: #16a34a;"">Descuento ({quote.DiscountPercent}%):</td>
                        <td style=""padding: 4px 8px; font-size: 13px; color: #16a34a; text-align: right; font-weight: 600;"">-${quote.DiscountAmount:N0}</td>
                    </tr>" : "")}
                    <tr style=""border-top: 2px solid #e2e8f0;"">
                        <td style=""padding: 8px 8px; font-size: 15px; font-weight: 800; color: #0f172a;"">Total:</td>
                        <td style=""padding: 8px 8px; font-size: 17px; font-weight: 800; color: #0284c7; text-align: right;"">{totalFormatted}</td>
                    </tr>
                </table>
                <div style=""clear: both;""></div>

                <!-- OBSERVACIONES O TÉRMINOS -->
                {(!string.IsNullOrWhiteSpace(quote.Observations) ? $@"
                <div style=""background: #fffbeb; border-left: 4px solid #f59e0b; padding: 12px 16px; border-radius: 6px; margin-bottom: 20px; font-size: 13px; color: #92400e;"">
                    <strong>Observaciones:</strong> {quote.Observations}
                </div>" : "")}

                <div style=""background-color: #f8fafc; border: 1px dashed #cbd5e1; border-radius: 10px; padding: 14px 18px; margin-bottom: 24px; text-align: center;"">
                    <p style=""margin: 0; font-size: 13px; color: #475569; font-weight: 600;"">
                        📎 El documento oficial en formato PDF y las evidencias fotográficas están adjuntos a este correo.
                    </p>
                </div>

                <p style=""margin: 0; font-size: 13px; color: #64748b; line-height: 1.5;"">
                    Validez de la oferta: {(quote.ExpirationDate.HasValue ? quote.ExpirationDate.Value.ToString("dd/MM/yyyy") : "15 días")}. Agradecemos tu confianza en <strong>{workshopName}</strong>.
                </p>
            </td>
        </tr>

        <!-- FOOTER -->
        <tr>
            <td style=""background-color: #f8fafc; border-top: 1px solid #e2e8f0; padding: 18px 25px; text-align: center; font-size: 12px; color: #94a3b8;"">
                <p style=""margin: 0 0 4px 0; font-weight: 600; color: #64748b;"">{workshopName} — {workshopSlogan}</p>
                <p style=""margin: 0;"">Este es un correo generado automáticamente. Ante cualquier consulta puedes comunicarte con nuestro equipo.</p>
            </td>
        </tr>
    </table>
</div>";

            var emailRequest = new EmailRequest
            {
                To = emailDto.ToEmail,
                Subject = $"Cotización {quoteNo} | {workshopName}",
                Body = htmlBody,
                Attachments = attachments
            };

            await _emailService.SendEmailAsync(emailRequest, cancellation);
            await _quotationRepository.UpdateStatusAsync(quote.Id, "Sent", null, cancellation);

            return true;
        }

        public async Task<bool> ProcessApprovalAsync(int id, QuotationApprovalRequestDto approvalDto, CancellationToken cancellation)
        {
            return await _quotationRepository.ProcessApprovalAsync(id, approvalDto, cancellation);
        }

        public async Task<bool> ProcessPublicApprovalAsync(string token, QuotationApprovalRequestDto approvalDto, CancellationToken cancellation)
        {
            var quote = await _quotationRepository.GetByTokenAsync(token, cancellation);
            if (quote == null) return false;

            return await _quotationRepository.ProcessApprovalAsync(quote.Id, approvalDto, cancellation);
        }

        public async Task<bool> RejectQuotationAsync(int id, string? reason, CancellationToken cancellation)
        {
            return await _quotationRepository.UpdateStatusAsync(id, "Rejected", reason, cancellation);
        }

        public async Task<int> ConvertToWorkOrderAsync(QuotationConvertToOrderDto dto, CancellationToken cancellation)
        {
            return await _quotationRepository.ConvertToWorkOrderAsync(dto, cancellation);
        }

        public async Task<int> ConvertToDirectSaleAsync(int quotationId, int paymentMethodId, string? referenceCode, CancellationToken cancellation)
        {
            return await _quotationRepository.ConvertToDirectSaleAsync(quotationId, paymentMethodId, referenceCode, cancellation);
        }

        public async Task<int> ConvertToDirectSaleDtoAsync(QuotationConvertToSaleDto dto, CancellationToken cancellation)
        {
            return await _quotationRepository.ConvertToDirectSaleDtoAsync(dto, cancellation);
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellation)
        {
            return await _quotationRepository.DeleteAsync(id, cancellation);
        }
    }
}
