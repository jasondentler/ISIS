using System;
using FluentValidation;
using NUnit.Framework;

namespace ISIS.Schedule
{
    [TestFixture]
    public class ChangeSectionLocationCommandFixture : ValidationFixture<ChangeSectionLocationCommand>
    {
        protected override AbstractValidator<ChangeSectionLocationCommand> CreateValidator()
        {
            return new ChangeSectionLocationCommandValidator();
        }

        protected override ChangeSectionLocationCommand GetValidInstance()
        {
            return new ChangeSectionLocationCommand()
                       {
                           LocationId = Guid.NewGuid(),
                           SectionId = Guid.NewGuid()
                       };
        }

        [Test]
        public void SectionId_must_be_specified()
        {
            GetFailure(new ChangeSectionLocationCommand()
            {
                SectionId = default(Guid)
            }, cmd => cmd.SectionId);
        }

        [Test]
        public void LocationId_must_be_specified()
        {
            GetFailure(new ChangeSectionLocationCommand()
            {
                LocationId = default(Guid)
            }, cmd => cmd.LocationId);
        }



    }
}
