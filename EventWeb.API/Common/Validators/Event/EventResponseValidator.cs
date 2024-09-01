using EventWeb.API.DTOs;
using FluentValidation;

namespace EventWeb.API.Validators
{
    public class EventResponseValidator : AbstractValidator<EventResponseDTO>
    {
        private const int MAX_NAME_LENGTH = 40;
        private const int MIN_NAME_LENGTH = 4;
        private const int MAX_DESCRIPTION_LENGTH = 600;
        private const int MIN_DESCRIPTION_LENGTH = 4;
        private const int MAX_LOCATION_LENGTH = 50; 
        private const int MIN_LOCATION_LENGTH = 2; 
        private const int PARTICIPANTS_LOW_LIMIT = 1500; 
        private const int PARTICIPANTS_TOP_LIMIT = 1500; 
        public EventResponseValidator()
        {
            RuleFor(e => e.Id).NotEmpty(); 

            RuleFor(e => e.Name).NotEmpty()
                .Length(MIN_NAME_LENGTH, MAX_NAME_LENGTH);

            RuleFor(e => e.Description).NotEmpty()
                .Length(MIN_DESCRIPTION_LENGTH, MAX_DESCRIPTION_LENGTH); 

            RuleFor(e => e.Location).NotEmpty()
                .Length(MIN_LOCATION_LENGTH, MAX_LOCATION_LENGTH);

            RuleFor(e => e.EventTime).NotEmpty(); 

            RuleFor(e => e.MaxParticipantsNumber).NotEmpty()
                .GreaterThanOrEqualTo(PARTICIPANTS_LOW_LIMIT)
                .LessThanOrEqualTo(PARTICIPANTS_TOP_LIMIT); 

            RuleFor(e => e.ImageBase64).NotEmpty(); 
        } 
    }
}