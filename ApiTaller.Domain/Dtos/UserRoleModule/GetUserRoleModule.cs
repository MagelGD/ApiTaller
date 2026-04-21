using ApiTaller.Domain.Dtos.Module;
using ApiTaller.Domain.Dtos.UserRole;

namespace ApiTaller.Domain.Dtos.UserRoleModule
{
    public class GetUserRoleModule
    {
        public int id { get; set; }
        public GetUserRole Role { get; set; }
        public GetModule Module { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string ResponsibleUser { get; set; }
    }
}
