namespace ApiTaller.Domain.Dtos.Auth
{
    public class LoginResponseDto
    {
        public string Token { get; set; } = null!;
        public string Role { get; set; } = null!;
        public bool MustChangePassword { get; set; }
        public int UserId { get; set; }
        public int? CustomerId { get; set; }
    }
}
