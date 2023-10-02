using Domain.Models.Request;
using Domain.Models.Request.Auth;
using FluentValidation;

namespace Domain.Validators;

public class SignUpRequestValidator : AbstractValidator<SignUpRequest>
{
    public SignUpRequestValidator()
    {
        RuleFor(request => request.FirstName)
            .NotEmpty()
            .Length(3, 50);
        
        RuleFor(request => request.LastName)
            .Length(3, 50);
        
        RuleFor(request => request.Username)
            .SetValidator(new UsernameValidator());
        
        RuleFor(request => request.Email)
            .SetValidator(new EmailValidator());
        
        RuleFor(request => request.Password)
            .NotEmpty()
            .Length(5, 20);
    }
}