using System;

namespace ApiTaller.Domain.Models
{
    public class EmailSettings : GeneralEntity
    {
        public string Host { get; set; } = null!;
        public int Port { get; set; }
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public bool EnableSsl { get; set; }
        public string SenderName { get; set; } = null!;
        public string SenderEmail { get; set; } = null!;
    }
}
