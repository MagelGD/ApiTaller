using System;

namespace ApiTaller.Domain.Dtos.ServicePrices
{
    public class GetServicePriceByVersionDto
    {
        public int Id { get; set; }
        public int ServiceCatalogId { get; set; }
        public int BrandModelVersionId { get; set; }
        public decimal Price { get; set; }
        public int EstimatedMinutes { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
