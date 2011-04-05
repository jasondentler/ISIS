using System;
using FluentValidation;
using NUnit.Framework;

namespace ISIS.Schedule
{
    [TestFixture]
    public class ChangeSectionTitleCommandFixture : ValidationFixture<ChangeSectionTitleCommand>
    {
        protected override AbstractValidator<ChangeSectionTitleCommand> CreateValidator()
        {
            return new ChangeSectionTitleCommandValidator();
        }

        protected override ChangeSectionTitleCommand GetValidInstance()
        {
            return new ChangeSectionTitleCommand()
                       {
                           SectionId = Guid.NewGuid(),
                           NewTitle = "New Course Title"
                       };
        }

        [Test]
        public void SectionId_must_be_specified()
        {
            GetFailure(new ChangeSectionTitleCommand()
            {
                SectionId = default(Guid)
            }, cmd => cmd.SectionId);
        }


        [Test]
        public void Title_cant_be_null()
        {
            GetFailure(new ChangeSectionTitleCommand()
            {
                NewTitle = null
            }, cmd => cmd.NewTitle);
        }

        [Test]
        public void Title_cant_be_empty()
        {
            GetFailure(new ChangeSectionTitleCommand()
            {
                NewTitle = ""
            }, cmd => cmd.NewTitle);
        }


        [Test]
        public void Title_cant_be_over_30_characters()
        {
            GetFailure(new ChangeSectionTitleCommand()
            {
                NewTitle = "123456789012345678901234567890!"
            }, cmd => cmd.NewTitle);
        }

    }
}
