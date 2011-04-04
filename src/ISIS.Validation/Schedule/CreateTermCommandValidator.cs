using System;
using FluentValidation;

namespace ISIS.Schedule
{

    public class CreateTermCommandValidator : AbstractValidator<CreateTermCommand>
    {
        public CreateTermCommandValidator()
        {
            RuleFor(cmd => cmd.TermId)
                .NotEqual(default(Guid))
                .WithMessage("You must specify a term id");

            RuleFor(cmd => cmd.Abbreviation)
                .NotEmpty()
                .WithMessage("You must specify a term abbreviation")
                .Length(1, 9)
                .WithMessage("The term abbreviation must be no more than 10 characters")
                .Matches(@"^[^\s]*$")
                .WithMessage("The term abbreviation can't have spaced.")
                .Matches(@"^[A-Z0-9]*$")
                .WithMessage("The term abbreviation must contain only uppercase letters and numbers.");

            RuleFor(cmd => cmd.Name)
                .NotEmpty()
                .WithMessage("You must specify a term name");

            RuleFor(cmd => cmd.StartDate)
                .LessThanOrEqualTo(cmd => cmd.EndDate)
                .WithMessage("The term start date must be on or before the end date.");

            RuleFor(cmd => cmd.EndDate)
                .GreaterThanOrEqualTo(cmd => cmd.StartDate)
                .WithMessage("The term end date must be on or after the start date.")
                .LessThanOrEqualTo(cmd => cmd.StartDate.AddYears(1))
                .WithMessage("The term can't be more than 1 year long.");



        }
    }
}
