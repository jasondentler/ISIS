using System;
using FluentValidation;
using NUnit.Framework;

namespace ISIS.Schedule
{
    [TestFixture]
    public class CreateSectionCommandFixture : ValidationFixture<CreateSectionCommand>
    {
        protected override AbstractValidator<CreateSectionCommand> CreateValidator()
        {
            return new CreateSectionCommandValidator();
        }

        protected override CreateSectionCommand GetValidInstance()
        {
            return new CreateSectionCommand()
                       {
                           CourseId = Guid.NewGuid(),
                           TermId = Guid.NewGuid(),
                           SectionId = Guid.NewGuid(),
                           SectionNumber = "01ABC"
                       };
        }

        [Test]
        public void CourseId_must_be_specified()
        {
            GetFailure(new CreateSectionCommand()
            {
                CourseId = default(Guid)
            }, cmd => cmd.CourseId);
        }

        [Test]
        public void TermId_must_be_specified()
        {
            GetFailure(new CreateSectionCommand()
            {
                TermId = default(Guid)
            }, cmd => cmd.TermId);
        }

        [Test]
        public void SectionId_must_be_specified()
        {
            GetFailure(new CreateSectionCommand()
            {
                SectionId = default(Guid)
            }, cmd => cmd.SectionId);
        }

        [Test]
        public void SectionNumber_must_not_be_null()
        {
            GetFailure(new CreateSectionCommand()
            {
                SectionNumber = null
            }, cmd => cmd.SectionNumber);
        }

        [Test]
        public void SectionNumber_must_not_be_empty()
        {
            GetFailure(new CreateSectionCommand()
            {
                SectionNumber = string.Empty
            }, cmd => cmd.SectionNumber);
        }

        [Test]
        public void SectionNumber_must_not_be_whitespace()
        {
            GetFailure(new CreateSectionCommand()
            {
                SectionNumber = " "
            }, cmd => cmd.SectionNumber);
        }

        [Test]
        public void SectionNumber_must_contain_only_letters_and_numbers()
        {
            GetFailure(new CreateSectionCommand()
            {
                SectionNumber = "01ABC."
            }, cmd => cmd.SectionNumber);
        }

        [Test]
        public void SectionNumber_must_contain_only_uppercase()
        {
            GetFailure(new CreateSectionCommand()
            {
                SectionNumber = "01ABc"
            }, cmd => cmd.SectionNumber);
        }

        [Test]
        public void SectionNumber_must_not_contain_only_whitespace()
        {
            GetFailure(new CreateSectionCommand()
            {
                SectionNumber = "01 AB"
            }, cmd => cmd.SectionNumber);
        }

        [Test]
        public void SectionNumber_must_be_less_than_10_characters()
        {
            GetFailure(new CreateSectionCommand()
            {
                SectionNumber = "01234567890"
            }, cmd => cmd.SectionNumber);
        }






    }
}
