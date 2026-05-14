using System.Threading.Tasks;

namespace ApiTaller.Domain.Interfaces.Services.WorkOrders
{
    public interface IWorkOrderNotificationService
    {
        Task NotifyWorkOrderUpdatedAsync(int workOrderId, int customerId);
    }
}
