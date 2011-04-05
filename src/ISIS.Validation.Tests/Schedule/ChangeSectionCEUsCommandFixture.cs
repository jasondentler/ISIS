using System;
using FluentValidation;
using NUnit.Framework;

namespace ISIS.Schedule
{
    [TestFixture]
    public class ChangeSectionCEUsCommandFixture : ValidationFixture<ChangeSectionCEUsCommand>
    {
        protected override AbstractValidator<ChangeSectionCEUsCommand> CreateValidator()
        {
            return new ChangeSectionCEUsCommandValidator();
        }

        protected override ChangeSectionCEUsCommand GetValidInstance()
        {
            return new ChangeSectionCEUsCommand()
                       {
                           SectionId = Guid.NewGuid(),
                           CEUs = 0.7M
                       };
        }

        [Test]
        public void SectionId_must_be_specified()
        {
            GetFailure(new ChangeSectionCEUsCommand()
            {
                SectionId = default(Guid)
            }, cmd => cmd.SectionId );
        }

        [Test]
        public void CEUs_must_be_non_negative()
        {
            GetFailure(new ChangeSectionCEUsCommand()
            {
                CEUs = -0.1M
            }, cmd => cmd.CEUs);
        }

        [Test]
        public void CEUs_must_be_reasonable()
        {
            GetFailure(new ChangeSectionCEUsCommand()
                           {
                               CEUs = 1000
                           }, cmd => cmd.CEUs);
        }

    }
}
