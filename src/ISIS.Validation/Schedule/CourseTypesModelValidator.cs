using System;
using FluentValidation;

namespace ISIS.Schedule
{
    public class CourseTypesModelValidator : AbstractValidator<CourseTypesModel>
    {
        
        public CourseTypesModelValidator()
        {
            RuleFor(cmd => cmd.CourseId)
                .NotEqual(default(Guid))
                .WithMessage("You must specify a course");

            RuleFor(cmd => cmd.CourseTypes)
                .NotEmpty()
                .WithMessage("Each course must have at least one course type.");
        }

    }

}
