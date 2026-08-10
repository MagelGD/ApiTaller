using ApiTaller.Domain.Dtos.Options;
using ApiTaller.Domain.Dtos.Users;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ApiTaller.Infrastructure.Helpers.Jwt
{
    public static class TokenHelper
    {
        public static string CreateJwt(this LoginUserDto user, JwtOptions options)
        {
            SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(options.JwtSigningKey));
            SigningCredentials creds = new(key, SecurityAlgorithms.HmacSha256);
            string jti = NewToken();
            DateTime expiresUtc = DateTime.UtcNow.AddMinutes(options.AccessTokenMinutes);
            List<Claim> claims =
            [
                new(JwtRegisteredClaimNames.Sub, user.UserName.ToString()),
                new(ClaimTypes.Role, user.IdUserRole.ToString()),
                new(ClaimTypes.Sid, user.Id.ToString()),
                new(JwtRegisteredClaimNames.Jti, jti),
                new("workshop_id", user.WorkshopId?.ToString() ?? "0"),
                new("workshop_type", user.WorkshopType ?? "moto")
            ];
            JwtSecurityToken token = new(
                issuer: options.Issuer,
                audience: options.Audience,
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: expiresUtc,
                signingCredentials: creds
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public static string CreateJwt(this Domain.Models.User user, int? customerId, JwtOptions options)
        {
            SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(options.JwtSigningKey));
            SigningCredentials creds = new(key, SecurityAlgorithms.HmacSha256);
            string jti = NewToken();
            DateTime expiresUtc = DateTime.UtcNow.AddMinutes(options.AccessTokenMinutes);
            List<Claim> claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim(ClaimTypes.Role, user.UserRoleId.ToString()),
                new Claim(ClaimTypes.Sid, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, jti),
                new Claim("mustChangePassword", user.MustChangePassword.ToString().ToLower()),
                new Claim("workshop_id", user.WorkshopId?.ToString() ?? "0"),
                new Claim("workshop_type", user.WorkshopNavigation?.WorkshopType ?? "moto")
            };
            if (customerId.HasValue)
            {
                claims.Add(new Claim("customerId", customerId.Value.ToString()));
            }
            JwtSecurityToken token = new(
                issuer: options.Issuer,
                audience: options.Audience,
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: expiresUtc,
                signingCredentials: creds
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public static string NewToken()
        {
            byte[] bytes = RandomNumberGenerator.GetBytes(64);
            return Convert.ToBase64String(bytes)
                .TrimEnd('=').Replace('+', '-').Replace('/', '_');
        }

        public static string Sha256Base64(this string input)
        {
            using SHA256 sha = SHA256.Create();
            byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(input));
            return Convert.ToBase64String(bytes);
        }
    }
}
