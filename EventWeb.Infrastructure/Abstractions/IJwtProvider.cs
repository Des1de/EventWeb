using System.Security.Claims;

namespace EventWeb.Infrastructure
{
    public interface IJwtProvider
    {
        string GenerateToken(List<Claim> claims);
    }
}