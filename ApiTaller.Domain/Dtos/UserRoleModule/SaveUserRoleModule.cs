using ApiTaller.Domain.Dtos.RoleActions;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiTaller.Domain.Dtos.UserRoleModule
{
    public class SaveUserRoleModule
    {
        public List<ActionsRole> actions { get; set; } 
        public bool isActive { get; set; }
        public int modulesRoleId { get; set; }
        public int userRoleId { get; set; }
    }
}
