using System;
using System.Collections.Generic;

namespace ApiTaller.Domain.Models;

public partial class ServiceCatalog : GeneralEntity
{
    public int ServiceTypeId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal DefaultPrice { get; set; }
    public int DefaultMinutes { get; set; }
    public string? TimeUnit { get; set; }
    public string VehicleType { get; set; } = "both";

    public virtual ServiceType ServiceTypeIdNavigation { get; set; }
}
