namespace EventWeb.Core.Models
{
    public class Event : BaseEntity
    {
        public string Name { get; set; } = null!; 
        public string Description { get; set; } = null!; 
        public Guid CategoryId { get; set; }
        public Category Category { get; set; } = null!; 
        public string Location { get; set; } = null!;
        public DateTime EventTime { get; set; }
        public int MaxParticipantsNumber { get; set; }
        public IEnumerable<Participation> Participations { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
    }
}