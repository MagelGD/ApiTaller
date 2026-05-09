using ApiTaller.Domain.Dtos.CustomerPortal;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ApiTaller.Domain.Interfaces.Repositories.CustomerPortal
{
    public interface ICustomerPortalRepository
    {
        Task<IEnumerable<CustomerPortalVehicleDto>> GetMyVehiclesAsync(CancellationToken cancellation);
        Task<IEnumerable<CustomerPortalOrderSummaryDto>> GetMyOrdersByVehicleAsync(int vehicleId, CancellationToken cancellation);
        Task<CustomerPortalOrderDetailDto?> GetMyOrderDetailAsync(int orderId, CancellationToken cancellation);
        Task<bool> ApproveItemAsync(CustomerPortalApprovalDto dto, CancellationToken cancellation);
    }
}
