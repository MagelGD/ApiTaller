using ApiTaller.Domain.Dtos.Accounting;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ApiTaller.Domain.Interfaces.Repositories.Accounting
{
    public interface IAccountingRepository
    {
        Task<IEnumerable<MechanicWithSettingsRawDto>> GetMechanicsWithSettingsRawAsync(CancellationToken ct);
        Task<bool> SavePaymentSettingsAsync(MechanicPaymentSettingsDto dto, CancellationToken ct);
        Task<IEnumerable<WorkOrderSalesRawDto>> GetWorkOrderSalesRawAsync(DateTime startDate, DateTime endDate, string status, CancellationToken ct);
        Task<IEnumerable<PendingServiceRawDto>> GetPendingServicesRawAsync(int mechanicId, DateTime startDate, DateTime endDate, CancellationToken ct);
        Task<bool> SettleServicesAsync(int mechanicId, DateTime startDate, DateTime endDate, decimal totalAmount, List<int> serviceIds, int responsibleUserId, CancellationToken ct);
        Task<IEnumerable<MechanicSettlementDto>> GetSettlementHistoryAsync(int? mechanicId, CancellationToken ct);
    }
}
