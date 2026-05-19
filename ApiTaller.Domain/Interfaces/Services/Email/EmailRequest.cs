using System.Collections.Generic;

namespace ApiTaller.Domain.Interfaces.Services.Email
{
    public class EmailRequest
    {
        public string To { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public List<EmailAttachment> Attachments { get; set; } = new List<EmailAttachment>();
    }
}
