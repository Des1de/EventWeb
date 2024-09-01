using System.Security.Claims;
using EventWeb.Application.Abstractions;
using EventWeb.Core.Models;
using Microsoft.AspNetCore.Identity;

namespace EventWeb.Application.UseCases
{
    public class RefreshUserUseCase
    {
        private readonly UserManager<User> _userManager;
        private readonly IJwtProvider _jwtProvider; 

        public RefreshUserUseCase(UserManager<User> userManager, IJwtProvider jwtProvider)
        {
            _userManager = userManager; 
            _jwtProvider = jwtProvider; 
        }

        public async Task<TokenModel> Refresh(string email, string refreshToken)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if(user is null)
            {
                throw new Exception("User not found"); 
            }

            if(!_jwtProvider.ValidateRefreshToken(user, refreshToken))
            {
                throw new Exception("Invalid refresh token"); 
            }

            var userRoles = await _userManager.GetRolesAsync(user);
            List<Claim> claims = [new("userId", user.Id.ToString())]; 
            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var token = _jwtProvider.GenerateToken(claims);

            var newRefreshToken = _jwtProvider.GenerateRefreshToken(); 
            var refreshExpire = _jwtProvider.GetRefreshExpireDays(); 

            user.RefreshToken = newRefreshToken;
            user.RefreshExpire = DateTime.UtcNow.AddDays(refreshExpire); 

            await _userManager.UpdateAsync(user); 

            return new TokenModel
            {
                AccessToken = token, 
                RefreshToken = newRefreshToken
            };
        }
    }
}