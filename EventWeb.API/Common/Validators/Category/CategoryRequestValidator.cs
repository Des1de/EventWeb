using EventWeb.API.DTOs;
using FluentValidation;

namespace EventWeb.API.Validators
{
    public class CategoryRequestValidator : AbstractValidator<CategoryRequestDTO>
    {
        private const int MAX_NAME_LENGTH = 40;
        private const int MIN_NAME_LENGTH = 4;
        public CategoryRequestValidator()
        {
            RuleFor(c => c.Name).NotEmpty()
                .Length(MIN_NAME_LENGTH, MAX_NAME_LENGTH);
        } 
    }
}