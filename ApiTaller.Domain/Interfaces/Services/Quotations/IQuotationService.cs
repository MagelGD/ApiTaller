using ApiTaller.Domain.Dtos.Quotations;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ApiTaller.Domain.Interfaces.Services.Quotations
{
    public interface IQuotationService
    {
        Task<IEnumerable<QuotationDto>> GetAllAsync(string? status, DateTime? startDate, DateTime? endDate, CancellationToken cancellation);
        Task<QuotationDto?> GetByIdAsync(int id, CancellationToken cancellation);
        Task<QuotationDto?> GetByTokenAsync(string token, CancellationToken cancellation);
        Task<IEnumerable<QuotationDto>> GetMyQuotationsAsync(int customerId, CancellationToken cancellation);
        Task<QuotationDto> CreateAsync(QuotationCreateDto dto, CancellationToken cancellation);
        Task<bool> UpdateAsync(int id, QuotationCreateDto dto, CancellationToken cancellation);
        Task<bool> SendEmailAsync(SendQuotationEmailDto emailDto, CancellationToken cancellation);
        Task<bool> ProcessApprovalAsync(int id, QuotationApprovalRequestDto approvalDto, CancellationToken cancellation);
        Task<bool> ProcessPublicApprovalAsync(string token, QuotationApprovalRequestDto approvalDto, CancellationToken cancellation);
        Task<bool> RejectQuotationAsync(int id, string? reason, CancellationToken cancellation);
        Task<int> ConvertToWorkOrderAsync(QuotationConvertToOrderDto dto, CancellationToken cancellation);
        Task<int> ConvertToDirectSaleAsync(int quotationId, int paymentMethodId, string? referenceCode, CancellationToken cancellation);
        Task<int> ConvertToDirectSaleDtoAsync(QuotationConvertToSaleDto dto, CancellationToken cancellation);
        Task<bool> DeleteAsync(int id, CancellationToken cancellation);
    }
}
