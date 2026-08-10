using System;
using System.Collections.Generic;

namespace ApiTaller.Domain.Models;

public partial class ServiceCatalog : GeneralEntity
{
    public int ServiceTypeId { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal DefaultPrice { get; set; }
    public int DefaultMinutes { get; set; }
    public string? TimeUnit { get; set; }
    public string VehicleType { get; set; } = "both";
    /// <summary>SAAS-1: ID del taller al que pertenece este catálogo de servicio</summary>
    public int WorkshopId { get; set; }

    public virtual ServiceType ServiceTypeIdNavigation { get; set; } = null!;
}
