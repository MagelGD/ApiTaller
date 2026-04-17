using System;
using System.Collections.Generic;

namespace ApiTaller.Domain.Models;

public class Action : GeneralEntity
{
    public int AplicationModuleId { get; set; }
    public int OperationId { get; set; }
    public string Name { get; set; }
    public int UserResponsibleId { get; set; }
    public virtual Module ModuleIdNavigation { get; set; }
    public virtual Operation OperationIdNavigation { get; set; }
}
