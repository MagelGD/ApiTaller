using ApiTaller.Domain.Dtos.Portal;
using ApiTaller.Domain.Interfaces.Repositories.Portal;
using ApiTaller.Domain.Interfaces.Services.Portal;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ApiTaller.Core.Services.Portal
{
    public class PortalService : IPortalService
    {
        private readonly IPortalRepository _repository;

        public PortalService(IPortalRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<PortalOrderListDto>> GetMyOrdersAsync(int customerId, CancellationToken ct)
        {
            return await _repository.GetMyOrdersAsync(customerId, ct);
        }

        public async Task<IEnumerable<PortalVehicleDto>> GetMyVehiclesAsync(int customerId, CancellationToken ct)
        {
            return await _repository.GetMyVehiclesAsync(customerId, ct);
        }

        public async Task<PortalOrderDetailDto?> GetOrderDetailAsync(int orderId, int customerId, CancellationToken ct)
        {
            return await _repository.GetOrderDetailAsync(orderId, customerId, ct);
        }

        public async Task<bool> ApproveOrderItemsAsync(int orderId, int customerId, PortalApproveItemsDto dto, CancellationToken ct)
        {
            return await _repository.ApproveOrderItemsAsync(orderId, customerId, dto, ct);
        }
    }
}
