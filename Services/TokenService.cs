using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SleepAidTrackerApi.Services
{
    public class TokenService
    {
        private readonly IConfiguration config;

        public TokenService(IConfiguration config)
        {
            this.config = config;
        }

        public string GenerateToken(IdentityUser user)
        {
            SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(config["JwtSettings:Key"]));
            SigningCredentials credentials = new(securityKey, SecurityAlgorithms.HmacSha256);

            TimeZoneInfo swedenTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time");
            string currentTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();

            List<Claim> claims = new()
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Iat, currentTime, ClaimValueTypes.Integer64),
                new Claim("uid", user.Id)
            };

            JwtSecurityToken token = new(
                issuer: config["JwtSettings:Issuer"],
                audience: config["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToInt32(config["JwtSettings:DurationInMinutes"])),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
