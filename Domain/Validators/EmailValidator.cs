using FluentValidation;

namespace Domain.Validators;

public class EmailValidator : AbstractValidator<string>
{
    public EmailValidator()
    {
        RuleFor(x => x)
            .NotEmpty()
            .EmailAddress()
            .Length(5, 50)
            .WithMessage("Invalid email");
    }
}