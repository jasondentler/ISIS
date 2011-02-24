using System;
using FluentValidation;

namespace ISIS.Validation
{
    /// <summary>
    /// Validation rules for AssignCIPCommand
    /// </summary>
    public class ChangeCourseLongTitleCommandValidator : AbstractValidator<ChangeCourseLongTitleCommand>
    {
        public ChangeCourseLongTitleCommandValidator()
        {
            RuleFor(cmd => cmd.CourseId)
                .NotEqual(default(Guid))
                .WithMessage("You must specify a course");

            RuleFor(cmd => cmd.NewLongTitle)
                .NotEmpty()
                .WithMessage("Long Title is required");

        }
    }
}
