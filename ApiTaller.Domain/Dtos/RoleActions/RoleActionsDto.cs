using ApiTaller.Domain.Dtos.Action;
using ApiTaller.Domain.Dtos.UserRole;

namespace ApiTaller.Domain.Dtos.RoleActions
{
    public class RoleActionsDto
    {
        public int Id { get; set; }
        public GetUserRoleDto Role { get; set; } = null!;
        public GetActionsDto Action { get; set; } = null!;
        public bool IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
