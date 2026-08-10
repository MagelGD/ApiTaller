using System;
using System.Collections.Generic;
namespace ApiTaller.Domain.Models;

public partial class ServicePriceByVersion : GeneralEntity
{
    public int ServiceCatalogId { get; set; }
    public int BrandModelVersionId { get; set; }
    public decimal Price { get; set; }
    public int EstimatedMinutes { get; set; }
    public string? TimeUnit { get; set; }
    /// <summary>SAAS-1: ID del taller al que pertenece esta precio de servicio</summary>
    public int WorkshopId { get; set; }

    public virtual ServiceCatalog ServiceCatalogIdNavigation { get; set; } = null!;
    public virtual BrandModelVersion BrandModelVersionIdNavigation { get; set; } = null!;
}
