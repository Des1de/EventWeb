using EventWeb.API.DTOs;
using FluentValidation;

namespace EventWeb.API.Validators
{
    public class LoginRequestValidator : AbstractValidator<LoginRequestDTO>
    {
        private const int MAX_EMAIL_LENGTH = 40;
        private const int MIN_PASSWORD_LENGTH = 6; 
        private const int MAX_PASSWORD_LENGTH = 40; 

        public LoginRequestValidator()
        {
            RuleFor(u => u.Email).NotEmpty()
                .MaximumLength(MAX_EMAIL_LENGTH)
                .EmailAddress(); 

            RuleFor(u => u.Password).NotEmpty()
                .Length(MIN_PASSWORD_LENGTH, MAX_PASSWORD_LENGTH); 
        }
    }
}