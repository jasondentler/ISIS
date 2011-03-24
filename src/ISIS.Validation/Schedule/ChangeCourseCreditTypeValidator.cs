using System;
using FluentValidation;

namespace ISIS.Schedule
{

    public class ChangeCourseCreditTypeValidator : AbstractValidator<ChangeCourseCreditTypeCommand>
    {
        public ChangeCourseCreditTypeValidator()
        {
            RuleFor(cmd => cmd.CourseId)
                .NotEqual(default(Guid))
                .WithMessage("You must specify a course");

            RuleFor(cmd => cmd.Type)
                .Must(type => Enum.IsDefined(typeof (CreditTypes), type))
                .WithMessage("Invalid credit type value");
        }
    }
}
