using System.Security.Claims;
using EventWeb.Core.Models;
using EventWeb.Infrastructure;
using Microsoft.AspNetCore.Identity;

namespace EventWeb.Application.UseCases
{
    public class LoginUserUseCase
    {
        private readonly UserManager<User> _userManager;
        private readonly IJwtProvider _jwtProvider; 

        public LoginUserUseCase(UserManager<User> userManager, IJwtProvider jwtProvider)
        {
            _userManager = userManager; 
            _jwtProvider = jwtProvider; 
        }

        public async Task<string> Login(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if(user == null)
            {
                throw new Exception("Failed to login"); 
            }
            
            var result = await _userManager.CheckPasswordAsync(user, password);
            
            if(result == false)
            {
                throw new Exception("Failed to login"); 
            }
            var userRoles = await _userManager.GetRolesAsync(user);
            List<Claim> claims = [new("userId", user.Id.ToString())]; 
            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var token = _jwtProvider.GenerateToken(claims);

            return token;
        }
    }
}