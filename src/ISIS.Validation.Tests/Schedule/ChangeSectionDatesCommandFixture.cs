using System;
using FluentValidation;
using NUnit.Framework;

namespace ISIS.Schedule
{
    [TestFixture]
    public class ChangeSectionDatesCommandFixture : ValidationFixture<ChangeSectionDatesCommand>
    {
        protected override AbstractValidator<ChangeSectionDatesCommand> CreateValidator()
        {
            return new ChangeSectionDatesCommandValidator();
        }

        protected override ChangeSectionDatesCommand GetValidInstance()
        {
            return new ChangeSectionDatesCommand()
                       {
                           SectionId = Guid.NewGuid(),
                           StartDate = DateTime.Today.AddMonths(-2),
                           EndDate = DateTime.Today.AddMonths(2)
                       };
        }

        [Test]
        public void SectionId_must_not_be_default_guid()
        {
            GetFailure(new ChangeSectionDatesCommand()
                           {
                               SectionId = default(Guid)
                           }, cmd => cmd.SectionId);
        }

        [Test]
        public void StartDate_must_not_be_after_end_date()
        {
            GetFailure(new ChangeSectionDatesCommand()
            {
                StartDate = DateTime.Today.AddDays(1),
                EndDate = DateTime.Today
            }, cmd => cmd.StartDate);
        }

        [Test]
        public void EndDate_must_not_be_before_start_date()
        {
            GetFailure(new ChangeSectionDatesCommand()
            {
                StartDate = DateTime.Today.AddDays(1),
                EndDate = DateTime.Today
            }, cmd => cmd.EndDate);
        }

        [Test]
        public void EndDate_must_be_within_1_year_of_start_date()
        {
            GetFailure(new ChangeSectionDatesCommand()
            {
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddYears(1).AddDays(1)
            }, cmd => cmd.EndDate);
        }

    }
}
