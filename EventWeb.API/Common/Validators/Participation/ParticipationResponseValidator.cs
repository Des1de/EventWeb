using EventWeb.API.DTOs;
using FluentValidation;

namespace EventWeb.API.Validators
{
    public class ParticipationResponseValidator : AbstractValidator<ParticipationResponseDTO>
    {
        public ParticipationResponseValidator()
        {
            RuleFor(p => p.Id).NotEmpty();
            RuleFor(p => p.UserId).NotEmpty();
            RuleFor(p => p.EventId).NotEmpty();
            RuleFor(p => p.EventRegistrationDate).NotEmpty();
        } 
    }
}