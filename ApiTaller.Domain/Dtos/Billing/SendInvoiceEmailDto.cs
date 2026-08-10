namespace ApiTaller.Domain.Dtos.Billing
{
    public class SendInvoiceEmailDto
    {
        public int SaleId { get; set; }
        public string ToEmail { get; set; } = null!;
        public string PdfBase64 { get; set; } = null!;
        public string FileName { get; set; } = null!;
    }
}
