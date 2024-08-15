using EventWeb.Core.Models;

namespace EventWeb.Core.Abstractions
{
    public interface IUserService
    {
        Task<string> Login(string email, string password);
        Task Register(User user, string password);
        Task<IEnumerable<User>> GetAllUsers();
    }
}