using ApiTaller.Domain.Dtos.Module;
using ApiTaller.Domain.Dtos.UserRole;

namespace ApiTaller.Domain.Dtos.UserRoleModule
{
    public class GetUserRoleModule
    {
        public int id { get; set; }
        public required GetUserRole Role { get; set; }
        public required GetModule Module { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public required string ResponsibleUser { get; set; }
    }
}
