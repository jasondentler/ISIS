using System;
using FluentValidation;

namespace ISIS.Schedule
{
    public class CreateSectionCommandValidator : AbstractValidator<CreateSectionCommand>
    {
        public CreateSectionCommandValidator()
        {
            RuleFor(cmd => cmd.CourseId)
                .NotEqual(default(Guid))
                .WithMessage("You must specify a course");
            
            RuleFor(cmd => cmd.TermId)
                .NotEqual(default(Guid))
                .WithMessage("You must specify a term");

            RuleFor(cmd => cmd.SectionId)
                .NotEqual(default(Guid))
                .WithMessage("You must specify a section id");

            RuleFor(cmd => cmd.SectionNumber)
                .NotEmpty()
                .WithMessage("You must specify a section number")
                .Matches("^[A-Z0-9]{1,10}$")
                .WithMessage("Section number can contain only uppercase letters and numbers");

        }
    }
}
