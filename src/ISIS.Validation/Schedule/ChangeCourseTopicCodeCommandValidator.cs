using FluentValidation;
using System;

namespace ISIS.Schedule
{

    public class ChangeCourseTopicCodeCommandValidator : AbstractValidator<ChangeCourseTopicCodeCommand>
    {

        public ChangeCourseTopicCodeCommandValidator()
        {

            RuleFor(cmd => cmd.CourseId)
                .NotEqual(default(Guid))
                .WithMessage("You must supply a valid course id.");

            RuleFor(cmd => cmd.TopicCodeId)
                .NotEqual(default(Guid))
                .WithMessage("You must supply a valid topic code id.");

        }
    }
}
