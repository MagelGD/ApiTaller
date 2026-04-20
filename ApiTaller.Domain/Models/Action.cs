using System;
using System.Collections.Generic;

namespace ApiTaller.Domain.Models;

public class Action : GeneralEntity
{
    public int ModuleId { get; set; }
    public int OperationId { get; set; }
    public string Name { get; set; }
    public string Slug { get; set; }
    public virtual Module ModuleIdNavigation { get; set; }
    public virtual Operation OperationIdNavigation { get; set; }
}
