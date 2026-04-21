using System;
using System.Collections.Generic;
using System.Text;

namespace ApiTaller.Domain.Dtos.RoleActions
{
    public class ActionsRole
    {
        public int ActionId { get; set; }
        public int ModuleId { get; set; }
        public bool IsActive { get; set; }
    }
}
