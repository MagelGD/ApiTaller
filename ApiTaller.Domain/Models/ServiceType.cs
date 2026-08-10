using System;
using System.Collections.Generic;

namespace ApiTaller.Domain.Models;

public partial class ServiceType : GeneralEntity
{
    public string Name { get; set; } = null!;
    /// <summary>SAAS-1: ID del taller al que pertenece este tipo de servicio</summary>
    public int WorkshopId { get; set; }
}
