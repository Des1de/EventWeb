using Microsoft.AspNetCore.Identity;

namespace EventWeb.Core.Models
{
    public class User : IdentityUser<Guid>
    {
        public string Name { get; set; } = null!; 
        public string Surname { get; set; } = null!; 
        public DateOnly BirthDay { get; set; }
        public IEnumerable<Participation> Participations { get; set; } = null!; 
    }
}