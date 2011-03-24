using System;
using FluentValidation;

namespace ISIS.Schedule
{

    public class ChangeCourseCEUsCommandValidator : AbstractValidator<ChangeCourseCEUsCommand>
    {
        public ChangeCourseCEUsCommandValidator()
        {
            RuleFor(cmd => cmd.CourseId)
                .NotEqual(default(Guid))
                .WithMessage("You must specify a course");

            RuleFor(cmd => cmd.CEUs)
                .GreaterThanOrEqualTo(0)
                .WithMessage("CEUs can't be negative")
                .LessThan(1000)
                .WithMessage("CEUs must be less than 1000");

        }
    }
}
