namespace ApiTaller.Domain.Dtos.Login
{
    public class Income
    {
        public required string Fullname { get; set; }
        public required string Token { get; set; }
        public bool Success { get; set; }
        public int IdUser { get; set; }
    }
}
