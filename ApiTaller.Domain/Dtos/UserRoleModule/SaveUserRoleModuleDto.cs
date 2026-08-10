using ApiTaller.Domain.Dtos.RoleActions;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiTaller.Domain.Dtos.UserRoleModule
{
    public class SaveUserRoleModuleDto
    {
        public List<ActionsRoleDto> actions { get; set; } = null!; 
        public bool isActive { get; set; }
        public int modulesRoleId { get; set; }
        public int userRoleId { get; set; }
    }
}
