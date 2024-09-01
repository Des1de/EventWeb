using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using EventWeb.Application.Abstractions;
using EventWeb.Core.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace EventWeb.Infrastructure
{
    public class JwtProvider : IJwtProvider
    {
        private readonly JwtOptions _options; 

        public JwtProvider(IOptions<JwtOptions> options)
        {
            _options = options.Value; 
        }
        public string GenerateToken(List<Claim> claims)
        {
            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey)), 
                SecurityAlgorithms.HmacSha256Signature
            );

            var token = new JwtSecurityToken(
                claims: claims,
                signingCredentials: signingCredentials,
                expires: DateTime.UtcNow.AddHours(_options.ExpiresHours)
            );

            var tokenValue = new JwtSecurityTokenHandler().WriteToken(token); 

            return tokenValue; 
        }

        public string GenerateRefreshToken()
        {
            var randomBytes = new byte[64];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
                return Convert.ToBase64String(randomBytes);
            }
        }

        public int GetRefreshExpireDays()
        {
            return _options.RefreshTokenExpiresDays; 
        }

        public bool ValidateRefreshToken(User user, string token)
        {
            if(user.RefreshToken is null)
            {
                return false; 
            }
            return user.RefreshToken.Equals(token) && user.RefreshExpire > DateTime.UtcNow;
        }
    }
}