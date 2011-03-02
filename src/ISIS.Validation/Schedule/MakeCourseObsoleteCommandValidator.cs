using System;
using FluentValidation;

namespace ISIS.Schedule
{
    public class MakeCourseObsoleteCommandValidator : AbstractValidator<MakeCourseObsoleteCommand>
    {
        public MakeCourseObsoleteCommandValidator()
        {
            RuleFor(cmd => cmd.CourseId)
                .NotEqual(default(Guid))
                .WithMessage("You must specify a course");
        }
    }
}
