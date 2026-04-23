namespace ApiTaller.Domain.Dtos.Login
{
    public class IncomeDto
    {
        public string Fullname { get; set; }
        public string Token { get; set; }
        public bool Success { get; set; }
        public int IdUser { get; set; }
        public int IdRoleUser { get; set; }
    }
}
