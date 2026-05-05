using ApiTaller.Domain.Dtos.BrandModelVersion;
using ApiTaller.Domain.Dtos.ServiceCatalogs;
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
        public GetServiceCatalogDto? ServiceCatalog { get; set; }
        public GetBrandModelVersionDto? BrandModelVersion { get; set; }

    }
}
