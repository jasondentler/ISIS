using System;
using FluentValidation;

namespace ISIS.Schedule
{
    public class ChangeTopicCodeDescriptionCommandValidator : AbstractValidator<ChangeTopicCodeDescriptionCommand>
    {
        public ChangeTopicCodeDescriptionCommandValidator()
        {

            RuleFor(cmd => cmd.TopicCodeId)
                .NotEqual(default(Guid))
                .WithMessage("You must supply a valid topic code id.");

            RuleFor(cmd => cmd.Description)
                .NotEmpty()
                .WithMessage("Description can't be empty")
                .Matches(@"^[^\s]")
                .WithMessage("Description can't start with a space");
        }
    }
}