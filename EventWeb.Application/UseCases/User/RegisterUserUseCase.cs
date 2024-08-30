using System.Text;
using EventWeb.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace EventWeb.Application.UseCases
{
    public class RegisterUserUseCase
    {
        private readonly UserManager<User> _userManager;
        private readonly ILogger<RegisterUserUseCase> _logger; 

        public RegisterUserUseCase(UserManager<User> userManager, ILogger<RegisterUserUseCase> logger)
        {
            _userManager = userManager; 
            _logger = logger; 
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
    }
}