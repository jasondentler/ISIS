using System;
using FluentValidation;

namespace ISIS.Schedule
{
    public class ChangeSectionTermCommandValidator : AbstractValidator<ChangeSectionTermCommand>
    {
        public ChangeSectionTermCommandValidator()
        {
            RuleFor(cmd => cmd.SectionId)
                .NotEqual(default(Guid))
                .WithMessage("You must specify a section id");

            RuleFor(cmd => cmd.TermId)
                .NotEqual(default(Guid))
                .WithMessage("You must specify a term id");


        }
    }
}
