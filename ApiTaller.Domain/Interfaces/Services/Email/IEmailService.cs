using ApiTaller.Domain.Models;
using System.Threading.Tasks;

namespace ApiTaller.Domain.Interfaces.Services.Email
{
    public interface IEmailService
    {
        Task SendEmailAsync(EmailRequest request, System.Threading.CancellationToken ct = default);
        Task<bool> TestConnectionAsync(EmailSettings settings, System.Threading.CancellationToken ct = default);
    }
}
