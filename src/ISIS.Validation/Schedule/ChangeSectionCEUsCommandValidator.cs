using System;
using FluentValidation;

namespace ISIS.Schedule
{

    public class ChangeSectionCEUsCommandValidator : AbstractValidator<ChangeSectionCEUsCommand>
    {
        public ChangeSectionCEUsCommandValidator()
        {
            RuleFor(cmd => cmd.SectionId)
                .NotEqual(default(Guid))
                .WithMessage("You must specify a section");

            RuleFor(cmd => cmd.CEUs)
                .GreaterThanOrEqualTo(0)
                .WithMessage("CEUs can't be negative")
                .LessThan(1000)
                .WithMessage("CEUs must be less than 1000");

        }
    }
}
