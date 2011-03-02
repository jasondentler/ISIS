using System;
using FluentValidation;
using ISIS.Validation.Tests;
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
                           Number = "01L",
                           CourseId = Guid.NewGuid(),
                           TermId = Guid.NewGuid()
                       };
        }

        [Test]
        public void Number_cant_be_empty()
        {
            GetFailure(new CreateSectionCommand()
            {
                Number = ""
            }, cmd => cmd.Number);
        }

        [Test]
        public void Number_cant_be_null()
        {
            GetFailure(new CreateSectionCommand()
            {
                Number = null
            }, cmd => cmd.Number);
        }

        [Test]
        public void CourseId_cant_be_default()
        {
            GetFailure(new CreateSectionCommand()
            {
                CourseId = default(Guid)
            }, cmd => cmd.CourseId);
        }

        [Test]
        public void TermId_cant_be_default()
        {
            GetFailure(new CreateSectionCommand()
            {
                CourseId = default(Guid)
            }, cmd => cmd.TermId);
        }



    }
}
