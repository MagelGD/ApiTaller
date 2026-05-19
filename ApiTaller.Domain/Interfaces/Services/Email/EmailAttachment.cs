namespace ApiTaller.Domain.Interfaces.Services.Email
{
    public class EmailAttachment
    {
        public string FileName { get; set; } = string.Empty;
        public byte[] Content { get; set; } = [];
        public string ContentType { get; set; } = string.Empty;
    }
}
