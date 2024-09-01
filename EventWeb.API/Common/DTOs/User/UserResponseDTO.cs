namespace EventWeb.API.DTOs
{
    public record UserResponseDTO
    (
        Guid Id, 
        string Email, 
        string Name, 
        string Surname, 
        string BirthDay
    ); 
}