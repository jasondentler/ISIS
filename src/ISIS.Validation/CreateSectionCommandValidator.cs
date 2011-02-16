using System;
using FluentValidation;

namespace ISIS.Validation
{
    /// <summary>
    /// Validation rules for AssignCIPCommand
    /// </summary>
    public class CreateSectionCommandValidator : AbstractValidator<CreateSectionCommand>
    {
        public CreateSectionCommandValidator()
        {
            RuleFor(cmd => cmd.Number)
                .NotEmpty()
                .WithMessage("Number is required");

            RuleFor(cmd => cmd.CourseId)
                .NotEqual(default(Guid))
                .WithMessage("You must specify a course");

            RuleFor(cmd => cmd.TermId)
                .NotEqual(default(Guid))
                .WithMessage("You must specify a term");
        }
    }
}
