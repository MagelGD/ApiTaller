using System.Threading.Tasks;

namespace ApiTaller.Domain.Interfaces.Services.Session
{
    public interface ISessionNotificationService
    {
        Task NotifyForceLogoutAsync(int userId, string reason = "Se ha iniciado sesión desde otro dispositivo.");
    }
}
