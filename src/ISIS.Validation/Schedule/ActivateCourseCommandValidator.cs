using System;
using FluentValidation;

namespace ISIS.Schedule
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
