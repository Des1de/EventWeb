namespace EventWeb.API.DTOs
{
    public record ParticipationResponseDTO
    (
        Guid Id, 
        Guid UserId, 
        Guid EventId, 
        string EventRegistrationDate
    );
}