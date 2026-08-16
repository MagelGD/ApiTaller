namespace ApiTaller.Domain.Dtos.Billing
{
    public class SendInvoiceEmailDto
    {
        public int SaleId { get; set; }
        public string ToEmail { get; set; } = null!;
        public string PdfBase64 { get; set; } = null!;
        public string FileName { get; set; } = null!;
        public string? CustomerName { get; set; }
        public string? VehiclePlate { get; set; }
        public string? VehicleModel { get; set; }
        public decimal? TotalAmount { get; set; }
    }
}
