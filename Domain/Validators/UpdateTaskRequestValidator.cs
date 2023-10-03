using Domain.Models.Request.Task;
using FluentValidation;

namespace Domain.Validators;

public class UpdateTaskRequestValidator : AbstractValidator<UpdateTaskRequest>
{
    public UpdateTaskRequestValidator()
    {
        RuleFor(x => x.Id).NotNull().GreaterThan(0);
        RuleFor(x => x.Priority).IsInEnum();
        RuleFor(x => x.Description).SetValidator(new DescriptionValidator());
        RuleFor(x => x.Title).SetValidator(new TitleValidator());
    }
}