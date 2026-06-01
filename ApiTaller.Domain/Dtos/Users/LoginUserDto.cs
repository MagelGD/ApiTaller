namespace ApiTaller.Domain.Dtos.Users
{
    public class LoginUserDto
    {
        public int Id { get; set; }
        public string Fullname { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string? Token { get; set; }
        public int? IdUserRole { get; set; }
        public int? ExpireToken { get; set;  }
        // SAAS-1: ID del taller y tipo
        public int? WorkshopId { get; set; }
        public string? WorkshopType { get; set; }
    }
}
