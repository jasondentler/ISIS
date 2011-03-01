using System;
using FluentValidation;

namespace ISIS.Validation
{

    public class ChangeCIPCommandValidator : AbstractValidator<ChangeCIPCommand>
    {
        public ChangeCIPCommandValidator()
        {
            RuleFor(cmd => cmd.CIP)
                .NotEmpty()
                .Matches(@"^\d{6}$")
                .WithMessage("CIP must be 6 digits long");

            RuleFor(cmd => cmd.CourseId)
                .NotEqual(default(Guid))
                .WithMessage("You must specify a course");
        }
    }
}
