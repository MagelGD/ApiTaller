namespace ApiTaller.Domain.Dtos.UserRole
{
    public class GetUserRole
    {
        public int IdUserRol { get; set; }
        public required string RoleName { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
