using System;
using System.Collections.Generic;

namespace ApiTaller.Domain.Models;

public partial class UserRole : GeneralEntity
{
    public string Role { get; set; }

    /// <summary>SAAS-1: ID del taller al que pertenece este rol. NULL = Rol de plataforma (Ej: SuperAdmin)</summary>
    public int? WorkshopId { get; set; }
    
    public virtual Workshop? WorkshopNavigation { get; set; }
}
