namespace ApiTaller.Domain.Dtos.Billing
{
    public class SalePaymentDto
    {
        public int Id { get; set; }
        public int PaymentMethodId { get; set; }
        public string? PaymentMethodName { get; set; }
        public decimal Amount { get; set; }
        public string? ReferenceCode { get; set; }
    }
}
