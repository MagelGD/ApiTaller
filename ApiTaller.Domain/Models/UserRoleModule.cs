using System;
using System.Collections.Generic;

namespace ApiTaller.Domain.Models;

public partial class UserRoleModule : GeneralEntity
{

    public int UserRoleId { get; set; }

    public int ModulesRoleId { get; set; }

    public virtual Module ModuleIdNavigation { get; set; } = null!;

    public virtual UserRole UserRoleIdNavigation { get; set; } = null!;
}
