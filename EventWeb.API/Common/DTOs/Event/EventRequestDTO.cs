namespace EventWeb.API.DTOs
{
    public record EventRequestDTO
    (
        string Name, 
        string Description, 
        Guid CategoryId, 
        string Location, 
        string EventTime, 
        int MaxParticipantsNumber,
        IFormFile Image
    );  
}