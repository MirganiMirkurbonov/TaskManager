using Domain.Models.Request;
using Domain.Models.Request.Auth;
using FluentValidation;

namespace Domain.Validators;

public class SignInRequestValidator : AbstractValidator<SignInRequest>
{
    public SignInRequestValidator()
    {
        RuleFor(x => x.Username)
            .SetValidator(new UsernameValidator());
        
        RuleFor(x => x.Password)
            .NotEmpty()
            .Length(5, 20);
    }
}