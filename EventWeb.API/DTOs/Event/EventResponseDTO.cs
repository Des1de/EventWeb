namespace EventWeb.API.DTOs
{
    public record EventResponseDTO
    {
        public Guid Id {get;set;}
        public string Name {get;set;} = null!;
        public string Description {get;set;} = null!;
        public CategoryResponseDTO Category {get;set;} = null!;
        public string Location {get;set;} = null!;
        public string EventTime {get;set;} = null!;
        public int MaxParticipantsNumber {get;set;}
        public string ImageBase64 { get; set; } = null!;
    }
}   