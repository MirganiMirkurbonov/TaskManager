using FluentValidation;

namespace Domain.Validators;

public class TitleValidator : AbstractValidator<string>
{
    public TitleValidator()
    {
        RuleFor(x => x).NotEmpty().Length(5, 100);
    }
}