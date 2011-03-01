using System;
using FluentValidation;

namespace ISIS.Validation
{
    public class ActivateCourseCommandValidator : AbstractValidator<ActivateCourseCommand>
    {
        public ActivateCourseCommandValidator()
        {
            RuleFor(cmd => cmd.CourseId)
                .NotEqual(default(Guid))
                .WithMessage("You must specify a course");
        }
    }
}
