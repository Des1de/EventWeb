namespace EventWeb.API.DTOs
{
    public record ParticipationRequestDTO
    (
        Guid UserId, 
        Guid EventId, 
        string EventRegistrationDate
    );
}