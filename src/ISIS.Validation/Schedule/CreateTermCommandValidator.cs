using FluentValidation;

namespace ISIS.Schedule
{

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
