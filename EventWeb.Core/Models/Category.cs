namespace EventWeb.Core.Models
{
    public class Category : BaseEntity
    {
        public string Name { get; set; } = null!; 
        public IEnumerable<Event> Events { get; set; } = null!; 
    }
}