﻿using System;
using FluentValidation;

namespace ISIS.Schedule
{
    public class DeactivateCourseCommandValidator : AbstractValidator<DeactivateCourseCommand>
    {
        public DeactivateCourseCommandValidator()
        {
            RuleFor(cmd => cmd.CourseId)
                .NotEqual(default(Guid))
                .WithMessage("You must specify a course");
        }
    }
}
