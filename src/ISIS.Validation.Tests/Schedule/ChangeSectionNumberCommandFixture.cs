using System;
using FluentValidation;
using NUnit.Framework;

namespace ISIS.Schedule
{
    [TestFixture]
    public class ChangeSectionNumberCommandFixture : ValidationFixture<ChangeSectionNumberCommand>
    {
        protected override AbstractValidator<ChangeSectionNumberCommand> CreateValidator()
        {
            return new ChangeSectionNumberCommandValidator();
        }

        protected override ChangeSectionNumberCommand GetValidInstance()
        {
            return new ChangeSectionNumberCommand()
                       {
                           SectionId = Guid.NewGuid(),
                           SectionNumber = "01ABC"
                       };
        }

        [Test]
        public void SectionId_must_be_specified()
        {
            GetFailure(new ChangeSectionNumberCommand()
            {
                SectionId = default(Guid)
            }, cmd => cmd.SectionId);
        }

        [Test]
        public void SectionNumber_must_not_be_null()
        {
            GetFailure(new ChangeSectionNumberCommand()
            {
                SectionNumber = null
            }, cmd => cmd.SectionNumber);
        }

        [Test]
        public void SectionNumber_must_not_be_empty()
        {
            GetFailure(new ChangeSectionNumberCommand()
            {
                SectionNumber = string.Empty
            }, cmd => cmd.SectionNumber);
        }

        [Test]
        public void SectionNumber_must_not_be_whitespace()
        {
            GetFailure(new ChangeSectionNumberCommand()
            {
                SectionNumber = " "
            }, cmd => cmd.SectionNumber);
        }

        [Test]
        public void SectionNumber_must_contain_only_letters_and_numbers()
        {
            GetFailure(new ChangeSectionNumberCommand()
            {
                SectionNumber = "01ABC."
            }, cmd => cmd.SectionNumber);
        }

        [Test]
        public void SectionNumber_must_contain_only_uppercase()
        {
            GetFailure(new ChangeSectionNumberCommand()
            {
                SectionNumber = "01ABc"
            }, cmd => cmd.SectionNumber);
        }

        [Test]
        public void SectionNumber_must_not_contain_only_whitespace()
        {
            GetFailure(new ChangeSectionNumberCommand()
            {
                SectionNumber = "01 AB"
            }, cmd => cmd.SectionNumber);
        }

        [Test]
        public void SectionNumber_must_be_less_than_10_characters()
        {
            GetFailure(new ChangeSectionNumberCommand()
            {
                SectionNumber = "01234567890"
            }, cmd => cmd.SectionNumber);
        }



    }
}
