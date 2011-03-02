using FluentValidation;

namespace ISIS.Schedule
{

    public class CreateDepartmentCommandValidator : AbstractValidator<CreateDepartmentCommand>
    {
        public CreateDepartmentCommandValidator()
        {
            RuleFor(cmd => cmd.Name)
                .NotEmpty()
                .WithMessage("Name is required");
        }
    }
}
