using System;
using FluentValidation;

namespace ISIS.Schedule
{

    public class ChangeSectionCreditTypeCommandValidator : AbstractValidator<ChangeSectionCreditTypeCommand>
    {
        public ChangeSectionCreditTypeCommandValidator()
        {
            RuleFor(cmd => cmd.SectionId)
                .NotEqual(default(Guid))
                .WithMessage("You must specify a section");

            RuleFor(cmd => cmd.CreditType)
                .Must(type => Enum.IsDefined(typeof(CreditTypes), type))
                .WithMessage("Invalid credit type value");
        }
    }
}
