using System;
using System.Collections.Generic;

namespace ApiTaller.Domain.Models;

public partial class ServiceCatalog : GeneralEntity
{
    public int ServiceTypeId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public virtual ServiceType ServiceTypeIdNavigation { get; set; }
}
