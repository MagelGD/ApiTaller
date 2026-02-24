using System;
using System.Collections.Generic;

namespace ApiTaller.Domain.Models;

public partial class UserRoleModules : GeneralEntity
{

    public int UserRoleId { get; set; }

    public int ModulesRoleId { get; set; }

    public virtual AplicationModule AplicationModuleIdNavigation { get; set; }

    public virtual UserRole UserRoleIdNavigation { get; set; }
}
