using System;
using System.Collections.Generic;

namespace ApiTaller.Domain.Models;

public partial class UserRole : GeneralEntity
{
    public string Role { get; set; }
}
