namespace ApiTaller.Domain.Dtos.Product
{
    public class ProductComboItemDto
    {
        public int Id { get; set; }
        public int ParentProductId { get; set; }
        public int ChildProductId { get; set; }
        public string? ChildProductName { get; set; }
        public string? ChildProductCode { get; set; }
        public decimal ChildProductPrice { get; set; }
        public decimal ChildProductSalePrice { get; set; }
        public int AvailableStock { get; set; }
        public int Quantity { get; set; } = 1;
    }
}
