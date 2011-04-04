using System;
using FluentValidation;
using NUnit.Framework;

namespace ISIS.Schedule
{
    [TestFixture]
    public class CreateTermCommandFixture : ValidationFixture<CreateTermCommand>
    {
        protected override AbstractValidator<CreateTermCommand> CreateValidator()
        {
            return new CreateTermCommandValidator();
        }

        protected override CreateTermCommand GetValidInstance()
        {
            return new CreateTermCommand()
                       {
                           TermId = Guid.NewGuid(),
                           Abbreviation = "211FA",
                           Name = "Fall 2011",
                           StartDate = DateTime.Today.AddMonths(-2),
                           EndDate = DateTime.Today.AddMonths(2)
                       };
        }

        [Test]
        public void TermId_must_not_be_default_guid()
        {
            GetFailure(new CreateTermCommand()
            {
                TermId = default(Guid)
            }, cmd => cmd.TermId);
        }

        [Test]
        public void Abbreviation_must_not_be_null()
        {
            GetFailure(new CreateTermCommand()
            {
                Abbreviation = null
            }, cmd => cmd.Abbreviation);
        }

        [Test]
        public void Abbreviation_must_not_be_empty()
        {
            GetFailure(new CreateTermCommand()
            {
                Abbreviation = string.Empty
            }, cmd => cmd.Abbreviation);
        }

        [Test]
        public void Abbreviation_must_not_be_whitespace()
        {
            GetFailure(new CreateTermCommand()
            {
                Abbreviation = " "
            }, cmd => cmd.Abbreviation);
        }

        [Test]
        public void Abbreviation_must_contain_only_letters_and_numbers()
        {
            GetFailure(new CreateTermCommand()
            {
                Abbreviation = "01ABC."
            }, cmd => cmd.Abbreviation);
        }

        [Test]
        public void Abbreviation_must_contain_only_uppercase()
        {
            GetFailure(new CreateTermCommand()
            {
                Abbreviation = "01ABc"
            }, cmd => cmd.Abbreviation);
        }

        [Test]
        public void Abbreviation_must_not_contain_only_whitespace()
        {
            GetFailure(new CreateTermCommand()
            {
                Abbreviation = "01 AB"
            }, cmd => cmd.Abbreviation);
        }

        [Test]
        public void Abbreviation_must_be_10_characters_or_less()
        {
            GetFailure(new CreateTermCommand()
            {
                Abbreviation = "01234567890"
            }, cmd => cmd.Abbreviation);
        }


        [Test]
        public void Name_must_not_be_null()
        {
            GetFailure(new CreateTermCommand()
            {
                Name = null
            }, cmd => cmd.Name);
        }

        [Test]
        public void Name_must_not_be_empty()
        {
            GetFailure(new CreateTermCommand()
            {
                Name = string.Empty
            }, cmd => cmd.Name);
        }

        [Test]
        public void Name_must_not_be_whitespace()
        {
            GetFailure(new CreateTermCommand()
            {
                Name = " "
            }, cmd => cmd.Name);
        }

        [Test]
        public void StartDate_must_not_be_after_end_date()
        {
            GetFailure(new CreateTermCommand()
            {
                StartDate = DateTime.Today.AddDays(1),
                EndDate = DateTime.Today
            }, cmd => cmd.StartDate);
        }

        [Test]
        public void EndDate_must_not_be_before_start_date()
        {
            GetFailure(new CreateTermCommand()
            {
                StartDate = DateTime.Today.AddDays(1),
                EndDate = DateTime.Today
            }, cmd => cmd.EndDate);
        }

        [Test]
        public void EndDate_must_be_within_1_year_of_start_date()
        {
            GetFailure(new CreateTermCommand()
            {
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddYears(1).AddDays(1)
            }, cmd => cmd.EndDate);
        }

    }
}
