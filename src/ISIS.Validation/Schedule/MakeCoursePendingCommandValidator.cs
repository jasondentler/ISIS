﻿using System;
using FluentValidation;

namespace ISIS.Schedule
{
    public class MakeCoursePendingCommandValidator : AbstractValidator<MakeCoursePendingCommand>
    {
        public MakeCoursePendingCommandValidator()
        {
            RuleFor(cmd => cmd.CourseId)
                .NotEqual(default(Guid))
                .WithMessage("You must specify a course");
        }
    }
}
