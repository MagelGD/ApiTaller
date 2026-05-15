namespace ApiTaller.Domain.Dtos.Billing
{
    public class SendInvoiceEmailDto
    {
        public int SaleId { get; set; }
        public string ToEmail { get; set; }
        public string PdfBase64 { get; set; }
        public string FileName { get; set; }
    }
}
