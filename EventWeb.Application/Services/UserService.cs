using System.Security.Claims;
using System.Text;
using EventWeb.Application.Abstractions;
using EventWeb.Core.Abstractions;
using EventWeb.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace EventWeb.Application.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager; 
        private readonly ILogger<UserService> _logger; 
        private readonly IJwtProvider _jwtProvider; 
        public UserService(UserManager<User> userManager, ILogger<UserService> logger, IJwtProvider jwtProvider)
        {
            _jwtProvider = jwtProvider;
            _logger = logger; 
            _userManager = userManager; 
        }

        public async Task Register(User user, string password)
        {
            var res = await _userManager.CreateAsync(user, password);

            if(!res.Succeeded)
            {
                var errors = new StringBuilder();
                foreach (var error in res.Errors)
                {
                    errors.Append($"{error.Code}:{error.Description}\n");
                }

                _logger.LogError("Error occured while creating user: {error}",
                    errors);

                throw new Exception(
                    $"Error occured while creating user: {errors}");
            }

            await _userManager.AddToRoleAsync(user, "User");

            if((await _userManager.GetUsersInRoleAsync("Admin")).Count == 0)
            {
                await _userManager.AddToRoleAsync(user, "Admin");
            }
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

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            var users = await _userManager.GetUsersInRoleAsync("User");
            return users; 
        }
    }
}