using System;
using System.Collections.Generic;

namespace ApiTaller.Domain.Models;

public class Action : GeneralEntity
{
    public int ModuleId { get; set; }
    public int OperationId { get; set; }
    public string Name { get; set; } = null!;
    public string Slug { get; set; } = null!;
    public virtual Module ModuleIdNavigation { get; set; } = null!;
    public virtual Operation OperationIdNavigation { get; set; } = null!;
}
