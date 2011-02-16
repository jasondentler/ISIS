using FluentValidation;

namespace ISIS.Validation
{
    /// <summary>
    /// Validation rules for AssignCIPCommand
    /// </summary>
    public class AssignCIPCommandValidator : AbstractValidator<AssignCIPCommand>
    {
        public AssignCIPCommandValidator()
        {
            RuleFor(cmd => cmd.CIP)
                .Matches(@"^\d{6}$")
                .WithMessage("CIP must be 6 digits long");
        }
    }
}
