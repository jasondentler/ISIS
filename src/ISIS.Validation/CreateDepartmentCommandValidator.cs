using FluentValidation;

namespace ISIS.Validation
{
    /// <summary>
    /// Validation rules for CreateDepartmentCommand
    /// </summary>
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
