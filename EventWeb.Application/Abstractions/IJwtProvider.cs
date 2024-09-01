using System.Security.Claims;
using EventWeb.Core.Models;

namespace EventWeb.Application.Abstractions
{
    public interface IJwtProvider
    {
        string GenerateToken(List<Claim> claims);
        string GenerateRefreshToken();
        int GetRefreshExpireDays();
        bool ValidateRefreshToken(User user, string token);
    }
}