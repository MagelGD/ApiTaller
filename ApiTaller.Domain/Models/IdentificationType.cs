using System;
using System.Collections.Generic;

namespace ApiTaller.Domain.Models;

public partial class IdentificationType : GeneralEntity
{
    public string Identification { get; set; } = null!;

    /// <summary>SAAS-2: ID del taller. NULL = dato compartido de plataforma.</summary>
    public int? WorkshopId { get; set; }
    public virtual Workshop? WorkshopNavigation { get; set; }
}
