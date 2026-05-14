using Microsoft.AspNetCore.SignalR;
using ApiTaller.Domain.Interfaces.Services.WorkOrders;
using ApiTaller.api.Hubs;
using System.Threading.Tasks;

namespace ApiTaller.api.Services
{
    public class WorkOrderNotificationService : IWorkOrderNotificationService
    {
        private readonly IHubContext<WorkOrderHub> _hubContext;
        private readonly Microsoft.Extensions.Logging.ILogger<WorkOrderNotificationService> _logger;

        public WorkOrderNotificationService(IHubContext<WorkOrderHub> hubContext, Microsoft.Extensions.Logging.ILogger<WorkOrderNotificationService> logger)
        {
            _hubContext = hubContext;
            _logger = logger;
        }

        public async Task NotifyWorkOrderUpdatedAsync(int workOrderId, int customerId)
        {
            _logger.LogInformation("Enviando notificación SignalR para Orden {OrderId} y Cliente {CustomerId}", workOrderId, customerId);
            // Notificamos el cambio. El frontend filtrará si le interesa este customerId o workOrderId
            await _hubContext.Clients.All.SendAsync("WorkOrderUpdated", workOrderId, customerId);
        }
    }
}
