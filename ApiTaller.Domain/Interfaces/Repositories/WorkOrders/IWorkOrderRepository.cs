using ApiTaller.Domain.Dtos.WorkOrder;
using ApiTaller.Domain.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ApiTaller.Domain.Interfaces.Repositories.WorkOrders
{
    public interface IWorkOrderRepository
    {
        Task<IEnumerable<WorkOrderDto>> GetAllAsync(CancellationToken cancellation);
        Task<WorkOrderDto?> GetByIdAsync(int id, CancellationToken cancellation);
        Task<bool> CreateAsync(WorkOrder create, CancellationToken cancellation);
        Task<bool> UpdateAsync(WorkOrder update, CancellationToken cancellation);
        Task<bool> ChangeStatusAsync(int id, string status, CancellationToken cancellation);
    }
}
