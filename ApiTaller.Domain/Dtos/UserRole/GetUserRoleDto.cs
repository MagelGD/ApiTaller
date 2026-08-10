namespace ApiTaller.Domain.Dtos.UserRole
{
    public class GetUserRoleDto
    {
        public int IdUserRol { get; set; }
        public string RoleName { get; set; } = null!;
        public bool IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
