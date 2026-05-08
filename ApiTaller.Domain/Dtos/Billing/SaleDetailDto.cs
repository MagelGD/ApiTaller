namespace ApiTaller.Domain.Dtos.Billing
{
    public class SaleDetailDto
    {
        public int Id { get; set; }
        public int? ProductId { get; set; }
        public int? ServiceCatalogId { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Total { get; set; }
    }
}
