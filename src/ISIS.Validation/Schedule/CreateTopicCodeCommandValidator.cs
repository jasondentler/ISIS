using FluentValidation;

namespace ISIS.Schedule
{
    public class CreateTopicCodeCommandValidator : AbstractValidator<CreateTopicCodeCommand>
    {
        public CreateTopicCodeCommandValidator()
        {
            RuleFor(cmd => cmd.Abbreviation)
                .Length(1, 5)
                .WithMessage("Abbreviation must be between 1 and 5 characters in length")
                .Matches("^[A-Z0-9]+$")
                .WithMessage("Abbreviation must be all uppercase letters or numbers")
                .Matches("^[A-Z]")
                .WithMessage("Abbreviation must start with a letter");

            RuleFor(cmd => cmd.Description)
                .NotEmpty()
                .WithMessage("Description can't be empty")
                .Matches(@"^[^\s]")
                .WithMessage("Description can't start with a space");
        }
    }
}