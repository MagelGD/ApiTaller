using ApiTaller.Domain.Dtos.Module;
using ApiTaller.Domain.Dtos.UserRole;

namespace ApiTaller.Domain.Dtos.UserRoleModule
{
    public class GetUserRoleModuleDto
    {
        public int id { get; set; }
        public GetUserRoleDto Role { get; set; } = null!;
        public GetModuleDto Module { get; set; } = null!;
        public bool IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string ResponsibleUser { get; set; } = null!;
    }
}
