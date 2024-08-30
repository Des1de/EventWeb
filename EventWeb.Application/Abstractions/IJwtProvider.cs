using System.Security.Claims;

namespace EventWeb.Application.Abstractions
{
    public interface IJwtProvider
    {
        string GenerateToken(List<Claim> claims);
    }
}