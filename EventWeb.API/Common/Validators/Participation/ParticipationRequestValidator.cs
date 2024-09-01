using EventWeb.API.DTOs;
using FluentValidation;

namespace EventWeb.API.Validators
{
    public class ParticipationRequestValidator : AbstractValidator<ParticipationRequestDTO>
    {
        public ParticipationRequestValidator()
        {
            RuleFor(p => p.UserId).NotEmpty();
            RuleFor(p => p.EventId).NotEmpty();
            RuleFor(p => p.EventRegistrationDate).NotEmpty();
        } 
    }
}