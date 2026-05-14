using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace ApiTaller.api.Hubs
{
    public class WorkOrderHub : Hub
    {
        // Se puede usar para unir a un grupo específico por Cliente si fuera necesario
        // Por ahora lo haremos broadcast o por UserId
        public async Task JoinOrderGroup(string orderId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, $"Order_{orderId}");
        }

        public async Task LeaveOrderGroup(string orderId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"Order_{orderId}");
        }
    }
}
