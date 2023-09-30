using FluentValidation;

namespace Domain.Validators;

public class UsernameValidator : AbstractValidator<string>
{
    public UsernameValidator()
    {
        RuleFor(x => x)
            .NotNull()
            .NotEmpty()
            .Length(5, 35)
            .WithMessage("Invalid username!");
    }
}