using System;
using FluentValidation;

namespace ISIS.Schedule
{
    public class ChangeSectionLocationCommandValidator : AbstractValidator<ChangeSectionLocationCommand>
    {
        public ChangeSectionLocationCommandValidator()
        {
            RuleFor(cmd => cmd.SectionId)
                .NotEqual(default(Guid))
                .WithMessage("You must specify a section");

            RuleFor(cmd => cmd.LocationId)
                .NotEqual(default(Guid))
                .WithMessage("You must specify a location");

        }
    }
}
