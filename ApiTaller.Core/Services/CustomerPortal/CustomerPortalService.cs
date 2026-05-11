using ApiTaller.Domain.Dtos.CustomerPortal;
using ApiTaller.Domain.Interfaces.Repositories.CustomerPortal;
using ApiTaller.Domain.Interfaces.Services.CustomerPortal;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ApiTaller.Core.Services.CustomerPortal
{
    public class CustomerPortalService : ICustomerPortalService
    {
        private readonly ICustomerPortalRepository _repository;

        public CustomerPortalService(ICustomerPortalRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<CustomerPortalVehicleDto>> GetMyVehiclesAsync(CancellationToken cancellation)
            => await _repository.GetMyVehiclesAsync(cancellation);

        public async Task<IEnumerable<CustomerPortalOrderSummaryDto>> GetMyOrdersByVehicleAsync(int vehicleId, CancellationToken cancellation)
            => await _repository.GetMyOrdersByVehicleAsync(vehicleId, cancellation);

        public async Task<CustomerPortalOrderDetailDto?> GetMyOrderDetailAsync(int orderId, CancellationToken cancellation)
            => await _repository.GetMyOrderDetailAsync(orderId, cancellation);

        public async Task<bool> ApproveItemAsync(CustomerPortalApprovalDto dto, CancellationToken cancellation)
            => await _repository.ApproveItemAsync(dto, cancellation);

        public async Task<bool> CreateMyVehicleAsync(CustomerPortalCreateVehicleDto dto, CancellationToken cancellation)
            => await _repository.CreateMyVehicleAsync(dto, cancellation);

        public async Task<IEnumerable<CustomerPortalAppointmentDto>> GetMyAppointmentsAsync(CancellationToken cancellation)
            => await _repository.GetMyAppointmentsAsync(cancellation);
    }
}
