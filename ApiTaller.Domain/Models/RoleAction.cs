using System;
using System.Collections.Generic;
using System.Text;

namespace ApiTaller.Domain.Models
{
    public class RoleAction : GeneralEntity
    {
        public int RoleId { get; set; }
        public int ActionId { get; set; }
        public virtual Action ActionIdNavigation { get; set; }
        public virtual UserRole RoleIdNavigation { get; set; }

    }
}
