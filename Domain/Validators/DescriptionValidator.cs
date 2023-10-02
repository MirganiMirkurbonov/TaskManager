using FluentValidation;

namespace Domain.Validators;

public class DescriptionValidator : AbstractValidator<string>
{
    public DescriptionValidator()
    {
        RuleFor(x => x).MaximumLength(500); // any other validations
    }
}