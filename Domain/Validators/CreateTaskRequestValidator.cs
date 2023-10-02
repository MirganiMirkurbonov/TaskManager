using Domain.Models.Request.Task;
using FluentValidation;

namespace Domain.Validators;

public class CreateTaskRequestValidator : AbstractValidator<CreateNewTaskRequest>
{
    public CreateTaskRequestValidator()
    {
        RuleFor(x => x.Title).SetValidator(new TitleValidator());
        RuleFor(x => x.Description).NotNull().SetValidator(new DescriptionValidator());
    }
}