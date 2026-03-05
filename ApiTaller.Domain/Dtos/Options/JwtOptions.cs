namespace ApiTaller.Domain.Dtos.Options
{
    public class JwtOptions
    {
        public string Issuer { get; set; } = default!;
        public string Audience { get; set; } = default!;
        public string JwtSigningKey { get; set; } = default!;
        public int AccessTokenMinutes { get; set; }
    }
}
