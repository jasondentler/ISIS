using FluentValidation;
using System;

namespace ISIS.Schedule
{
    /// <summary>
    /// Validation rules for CreateCourseCommand
    /// </summary>
    /// <remarks>Rules based on Texas Common Course Numbering System: http://www.tccns.org/ccn/taxonomy.asp </remarks>
    public class CreateCreditCourseCommandValidator : AbstractValidator<CreateCreditCourseCommand>
    {
        public CreateCreditCourseCommandValidator()
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
                .WithMessage("Course number must be a 4-digit number");

            RuleFor(cmd => cmd.Title)
                .NotEmpty().WithMessage("Title is required")
                .Matches(@"^.{0,30}$").WithMessage("Title must be no more than 30 characters long.");

            RuleFor(cmd => cmd.Types)
                .NotEmpty()
                .WithMessage("You must select at least one course type.");

        }
    }
}
