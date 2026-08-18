using Microsoft.AspNetCore.SignalR;
using ApiTaller.Domain.Interfaces.Services.Session;
using ApiTaller.api.Hubs;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace ApiTaller.api.Services
{
    public class SessionNotificationService : ISessionNotificationService
    {
        private readonly IHubContext<SessionHub> _hubContext;
        private readonly ILogger<SessionNotificationService> _logger;

        public SessionNotificationService(IHubContext<SessionHub> hubContext, ILogger<SessionNotificationService> logger)
        {
            _hubContext = hubContext;
            _logger = logger;
        }

        public async Task NotifyForceLogoutAsync(int userId, string reason = "Se ha iniciado sesión desde otro dispositivo.")
        {
            try
            {
                _logger.LogInformation("Enviando ForceLogout via SignalR al grupo user_{UserId}", userId);
                await _hubContext.Clients.Group($"user_{userId}").SendAsync("ForceLogout", new
                {
                    userId,
                    reason,
                    timestamp = System.DateTime.UtcNow
                });
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "Error al enviar notificación SignalR de ForceLogout para usuario {UserId}", userId);
            }
        }
    }
}
