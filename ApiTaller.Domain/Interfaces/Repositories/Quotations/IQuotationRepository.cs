using ApiTaller.Domain.Dtos.Quotations;
using ApiTaller.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ApiTaller.Domain.Interfaces.Repositories.Quotations
{
    public interface IQuotationRepository
    {
        Task<IEnumerable<QuotationDto>> GetAllAsync(string? status, DateTime? startDate, DateTime? endDate, CancellationToken cancellation);
        Task<QuotationDto?> GetByIdAsync(int id, CancellationToken cancellation);
        Task<QuotationDto?> GetByTokenAsync(string token, CancellationToken cancellation);
        Task<IEnumerable<QuotationDto>> GetByCustomerIdAsync(int customerId, CancellationToken cancellation);
        Task<Quotation> CreateAsync(QuotationCreateDto dto, CancellationToken cancellation);
        Task<bool> UpdateAsync(int id, QuotationCreateDto dto, CancellationToken cancellation);
        Task<bool> UpdateStatusAsync(int id, string status, string? reason, CancellationToken cancellation);
        Task<bool> ProcessApprovalAsync(int id, QuotationApprovalRequestDto approvalDto, CancellationToken cancellation);
        Task<int> ConvertToWorkOrderAsync(QuotationConvertToOrderDto dto, CancellationToken cancellation);
        Task<int> ConvertToDirectSaleAsync(int quotationId, int paymentMethodId, string? referenceCode, CancellationToken cancellation);
        Task<bool> DeleteAsync(int id, CancellationToken cancellation);
    }
}
