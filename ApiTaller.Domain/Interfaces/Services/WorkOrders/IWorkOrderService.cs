using ApiTaller.Domain.Dtos;
using ApiTaller.Domain.Dtos.WorkOrder;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ApiTaller.Domain.Interfaces.Services.WorkOrders
{
    public interface IWorkOrderService
    {
        Task<IEnumerable<WorkOrderDto>> GetAllAsync(CancellationToken cancellation);
        Task<WorkOrderDto?> GetByIdAsync(int id, CancellationToken cancellation);
        Task<bool> SaveAsync(WorkOrderDto workOrderDto, CancellationToken cancellation);
        Task<bool> ChangeStatusAsync(int id, string status, CancellationToken cancellation);
        Task<IEnumerable<WorkOrderHistoryDto>> GetHistoryAsync(int workOrderId, CancellationToken cancellation);
    }
}
