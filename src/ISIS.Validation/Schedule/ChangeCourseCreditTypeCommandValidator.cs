using System;
using FluentValidation;

namespace ISIS.Schedule
{

    public class ChangeCourseCreditTypeCommandValidator : AbstractValidator<ChangeCourseCreditTypeCommand>
    {
        public ChangeCourseCreditTypeCommandValidator()
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
