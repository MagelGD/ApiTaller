using ApiTaller.Domain.Dtos.Action;
using ApiTaller.Domain.Dtos.UserRole;

namespace ApiTaller.Domain.Dtos.RoleActions
{
    public class RoleActions
    {
        public int Id { get; set; }
        public required GetUserRole Role { get; set; }
        public required GetActions Action { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
