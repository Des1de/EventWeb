using EventWeb.API.DTOs;
using FluentValidation;

namespace EventWeb.API.Validators
{
    public class UserResponseValidator : AbstractValidator<UserResponseDTO>
    {
        private const int MAX_EMAIL_LENGTH = 40;
        private const int MIN_NAME_LENGTH = 2; 
        private const int MAX_NAME_LENGTH = 40; 
        private const int MIN_SURNAME_LENGTH = 2; 
        private const int MAX_SURNAME_LENGTH = 40; 

        public UserResponseValidator()
        {
            RuleFor(u => u.Email).NotEmpty()
                .MaximumLength(MAX_EMAIL_LENGTH)
                .EmailAddress(); 

            RuleFor(u => u.Id).NotEmpty(); 
            
            RuleFor(u => u.Name).NotEmpty()
                .Length(MIN_NAME_LENGTH, MAX_NAME_LENGTH);
            
            RuleFor(u => u.Surname).NotEmpty()
                .Length(MIN_SURNAME_LENGTH, MAX_SURNAME_LENGTH); 

            RuleFor(u => u.BirthDay).NotEmpty();
        }
    }
}