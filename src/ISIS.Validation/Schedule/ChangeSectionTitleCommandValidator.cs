using System;
using FluentValidation;

namespace ISIS.Schedule
{

    public class ChangeSectionTitleCommandValidator : AbstractValidator<ChangeSectionTitleCommand>
    {
        public ChangeSectionTitleCommandValidator()
        {
            RuleFor(cmd => cmd.SectionId)
                .NotEqual(default(Guid))
                .WithMessage("You must specify a section");

            RuleFor(cmd => cmd.NewTitle)
                .NotEmpty().WithMessage("Title is required")
                .Matches(@"^.{0,30}$").WithMessage("Title must be no more than 30 characters long.");

        }
    }
}
