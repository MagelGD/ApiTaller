using ApiTaller.Domain.Dtos.Portal;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ApiTaller.Domain.Interfaces.Services.Portal
{
    public interface IPortalService
    {
        Task<IEnumerable<PortalOrderListDto>> GetMyOrdersAsync(int customerId, CancellationToken ct);
        Task<IEnumerable<PortalVehicleDto>> GetMyVehiclesAsync(int customerId, CancellationToken ct);
        Task<PortalOrderDetailDto?> GetOrderDetailAsync(int orderId, int customerId, CancellationToken ct);
        Task<bool> ApproveOrderItemsAsync(int orderId, int customerId, PortalApproveItemsDto dto, CancellationToken ct);
    }
}
