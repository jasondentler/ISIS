using System;
using FluentValidation;

namespace ISIS.Schedule
{

    public class ChangeSectionDatesCommandValidator : AbstractValidator<ChangeSectionDatesCommand>
    {
        public ChangeSectionDatesCommandValidator()
        {
            RuleFor(cmd => cmd.SectionId)
                .NotEqual(default(Guid))
                .WithMessage("You must specify a section id");

            RuleFor(cmd => cmd.StartDate)
                .LessThanOrEqualTo(cmd => cmd.EndDate)
                .WithMessage("The section start date must be on or before the end date.");

            RuleFor(cmd => cmd.EndDate)
                .GreaterThanOrEqualTo(cmd => cmd.StartDate)
                .WithMessage("The section end date must be on or after the start date.")
                .LessThanOrEqualTo(cmd => cmd.StartDate.AddYears(1))
                .WithMessage("The section can't be more than 1 year long.");

        }
    }
}
