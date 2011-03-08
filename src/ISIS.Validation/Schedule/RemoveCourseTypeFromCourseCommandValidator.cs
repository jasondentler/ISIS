using System;
using FluentValidation;

namespace ISIS.Schedule
{

    public class RemoveCourseTypeFromCourseCommandValidator : AbstractValidator<RemoveCourseTypeFromCourse>
    {
        public RemoveCourseTypeFromCourseCommandValidator()
        {
            RuleFor(cmd => cmd.CourseId)
                .NotEqual(default(Guid))
                .WithMessage("You must specify a course");

            RuleFor(cmd => cmd.Type)
                .Must(type => Enum.IsDefined(typeof (CourseTypes), type))
                .WithMessage("Invalid course type value");
        }
    }
}
