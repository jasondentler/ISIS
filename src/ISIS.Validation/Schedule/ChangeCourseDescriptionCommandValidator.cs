using System;
using FluentValidation;

namespace ISIS.Schedule
{
    public class ChangeCourseDescriptionCommandValidator : AbstractValidator<ChangeCourseDescriptionCommand>
    {
        public ChangeCourseDescriptionCommandValidator()
        {
            RuleFor(cmd => cmd.CourseId)
                .NotEqual(default(Guid))
                .WithMessage("You must specify a course");

            RuleFor(cmd => cmd.NewDescription)
                .NotEmpty()
                .WithMessage("Description is required");

        }
    }
}