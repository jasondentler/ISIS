using System.Collections.Generic;
using FluentValidation;
using System;
using System.Linq;

namespace ISIS.Schedule
{
    /// <summary>
    /// Validation rules for CreateCourseCommand
    /// </summary>
    /// <remarks>Rules based on Texas Common Course Numbering System: http://www.tccns.org/ccn/taxonomy.asp </remarks>
    public class CreateContinuingEducationCourseCommandValidator : AbstractValidator<CreateContinuingEducationCourseCommand>
    {

        private static IEnumerable<CreditTypes> _creditTypesValues = Enum.GetValues(typeof (CreditTypes)).Cast<CreditTypes>();

        public CreateContinuingEducationCourseCommandValidator()
        {

            RuleFor(cmd => cmd.CourseId)
                .NotEqual(default(Guid))
                .WithMessage("You must supply a valid course id.");

            RuleFor(cmd => cmd.Rubric)
                .NotEmpty()
                .Matches(@"^[A-Z]{4}$")
                .WithMessage("Rubric must be a 4 character code, all uppercase");

            RuleFor(cmd => cmd.CourseNumber)
                .NotEmpty()
                .Matches(@"^\d{4}$")
                .WithMessage("Course number must be a 4-digit number")
                .Matches(@"^\d0\d{2}$")
                .WithMessage("For continuing education courses, the 2nd digit of the course number must be zero.");

            RuleFor(cmd => cmd.Title)
                .NotEmpty().WithMessage("Title is required");

            RuleFor(cmd => cmd.Type)
                .Must((cmd, type) => _creditTypesValues.Contains(type))
                .WithMessage("You must specify a valid credit type.");

        }
    }
}
