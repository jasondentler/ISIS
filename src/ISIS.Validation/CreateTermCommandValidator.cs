using FluentValidation;

namespace ISIS.Validation
{
    /// <summary>
    /// Validation rules for CreateTermCommand
    /// </summary>
    public class CreateTermCommandValidator : AbstractValidator<CreateTermCommand>
    {
        public CreateTermCommandValidator()
        {
            RuleFor(cmd => cmd.Name)
                .NotEmpty()
                .WithMessage("Name is required");
        }
    }
}
