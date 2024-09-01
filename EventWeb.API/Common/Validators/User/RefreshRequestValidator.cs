using EventWeb.API.DTOs;
using FluentValidation;

namespace EventWeb.API.Validators
{
    public class RefreshRequestValidator : AbstractValidator<RefreshRequestDTO>
    {
        private const int MAX_EMAIL_LENGTH = 40;

        public RefreshRequestValidator()
        {
            RuleFor(u => u.Email).NotEmpty()
                .MaximumLength(MAX_EMAIL_LENGTH)
                .EmailAddress(); 
        }
    }
}