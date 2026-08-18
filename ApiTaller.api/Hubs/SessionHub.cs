using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace ApiTaller.api.Hubs
{
    public class SessionHub : Hub
    {
        public async Task JoinSessionGroup(string userId)
        {
            if (!string.IsNullOrWhiteSpace(userId))
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, $"user_{userId}");
            }
        }

        public async Task LeaveSessionGroup(string userId)
        {
            if (!string.IsNullOrWhiteSpace(userId))
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"user_{userId}");
            }
        }
    }
}
