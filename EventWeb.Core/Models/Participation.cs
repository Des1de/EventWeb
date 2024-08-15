namespace EventWeb.Core.Models
{
    public class Participation : BaseEntity
    {
        public User User { get; set; } = null!;
        public Guid UserId { get; set; }
        public Event Event { get; set; } = null!;
        public Guid EventId { get; set; }
        public DateOnly EventRegistrationDate { get; set; }
    }
}