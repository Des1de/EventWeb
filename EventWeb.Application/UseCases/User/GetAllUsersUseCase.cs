using EventWeb.Core.Models;
using Microsoft.AspNetCore.Identity;

namespace EventWeb.Application.UseCases
{
    public class GetAllUsersUseCase
    {
        private readonly UserManager<User> _userManager; 
        public GetAllUsersUseCase(UserManager<User> userManager)
        {
            _userManager = userManager; 
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            var users = await _userManager.GetUsersInRoleAsync("User");
            return users; 
        }
    }
}