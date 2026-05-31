using ApiTaller.Domain.Dtos.Accounting;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ApiTaller.Domain.Interfaces.Services.Accounting
{
    public interface IAccountingService
    {
        Task<IEnumerable<MechanicPaymentSettingsDto>> GetPaymentSettingsAsync(CancellationToken ct);
        Task<bool> SavePaymentSettingsAsync(MechanicPaymentSettingsDto dto, CancellationToken ct);
        Task<SalesSummaryDto> GetSalesSummaryAsync(DateTime startDate, DateTime endDate, string status, int? mechanicId, string? vehicleType, CancellationToken ct);
        Task<IEnumerable<PendingServiceDto>> GetPendingServicesAsync(int mechanicId, DateTime startDate, DateTime endDate, CancellationToken ct);
        Task<bool> SettleServicesAsync(int mechanicId, DateTime startDate, DateTime endDate, decimal totalAmount, List<int> serviceIds, int responsibleUserId, CancellationToken ct);
        Task<IEnumerable<MechanicSettlementDto>> GetSettlementHistoryAsync(int? mechanicId, CancellationToken ct);
    }
}
