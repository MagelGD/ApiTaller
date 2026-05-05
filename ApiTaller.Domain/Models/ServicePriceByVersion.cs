using System;
using System.Collections.Generic;

namespace ApiTaller.Domain.Models;

public partial class ServicePriceByVersion : GeneralEntity
{
    public int ServiceCatalogId { get; set; }
    public int BrandModelVersionId { get; set; }
    public decimal Price { get; set; }
    public int EstimatedMinutes { get; set; }

    public virtual ServiceCatalog ServiceCatalogIdNavigation { get; set; }
    public virtual BrandModelVersion BrandModelVersionIdNavigation { get; set; }
}
