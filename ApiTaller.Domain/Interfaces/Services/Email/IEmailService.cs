using ApiTaller.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiTaller.Domain.Interfaces.Services.Email
{
    public interface IEmailService
    {
        Task SendEmailAsync(EmailRequest request, System.Threading.CancellationToken ct = default);
        Task<bool> TestConnectionAsync(EmailSettings settings, System.Threading.CancellationToken ct = default);
    }

    public class EmailRequest
    {
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public List<EmailAttachment> Attachments { get; set; } = new List<EmailAttachment>();
    }

    public class EmailAttachment
    {
        public string FileName { get; set; }
        public byte[] Content { get; set; }
        public string ContentType { get; set; }
    }
}
