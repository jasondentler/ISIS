using System;
using FluentValidation;

namespace ISIS.Schedule
{
    public class ChangeTopicCodeAbbreviationCommandValidator : AbstractValidator<ChangeTopicCodeAbbreviationCommand>
    {
        public ChangeTopicCodeAbbreviationCommandValidator()
        {

            RuleFor(cmd => cmd.TopicCodeId)
                .NotEqual(default(Guid))
                .WithMessage("You must supply a valid topic code id.");

            RuleFor(cmd => cmd.Abbreviation)
                .Length(1, 5)
                .WithMessage("Abbreviation must be between 1 and 5 characters in length")
                .Matches("^[A-Z0-9]+$")
                .WithMessage("Abbreviation must be all uppercase letters or numbers")
                .Matches("^[A-Z]")
                .WithMessage("Abbreviation must start with a letter");

        }
    }
}