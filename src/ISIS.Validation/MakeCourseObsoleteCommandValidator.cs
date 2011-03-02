using System;
using FluentValidation;

namespace ISIS.Validation
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
