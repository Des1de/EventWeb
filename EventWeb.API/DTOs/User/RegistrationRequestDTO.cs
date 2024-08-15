namespace EventWeb.API.DTOs
{
    public record RegistrationRequestDTO
    (
        string Email, 
        string Password, 
        string Name, 
        string Surname, 
        string BirthDay
    ); 
}