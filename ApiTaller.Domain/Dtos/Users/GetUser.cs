namespace ApiTaller.Domain.Dtos.Users
{
    public class GetUser
    {
        public int Id { get; set; }
        public required string Fullname { get; set; }
        public required string UserName { get; set; }
        public required string Password { get; set; }
        public string? Token { get; set; }
        public int? IdUserRole { get; set; }
    }
}
