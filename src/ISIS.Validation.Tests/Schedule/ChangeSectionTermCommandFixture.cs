using System;
using FluentValidation;
using NUnit.Framework;

namespace ISIS.Schedule
{
    [TestFixture]
    public class ChangeSectionTermCommandFixture : ValidationFixture<ChangeSectionTermCommand>
    {
        protected override AbstractValidator<ChangeSectionTermCommand> CreateValidator()
        {
            return new ChangeSectionTermCommandValidator();
        }

        protected override ChangeSectionTermCommand GetValidInstance()
        {
            return new ChangeSectionTermCommand()
                       {
                           SectionId = Guid.NewGuid(),
                           TermId = Guid.NewGuid()
                       };
        }

        [Test]
        public void SectionId_must_be_specified()
        {
            GetFailure(new ChangeSectionTermCommand()
            {
                SectionId = default(Guid)
            }, cmd => cmd.SectionId);
        }

        [Test]
        public void TermId_must_be_specified()
        {
            GetFailure(new ChangeSectionTermCommand()
            {
                TermId = default(Guid)
            }, cmd => cmd.TermId);
        }




    }
}
