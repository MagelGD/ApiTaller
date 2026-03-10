using System;
using System.Collections.Generic;

namespace ApiTaller.Domain.Models;

public partial class Login : GeneralEntity
{
    public int UserId { get; set; }
    public string Message { get; set; }
}
