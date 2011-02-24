using System;
using FluentValidation;
using NUnit.Framework;

namespace ISIS.Validation.Tests
{
    [TestFixture]
    public class ChangeCourseLongTitleCommandFixture : ValidationFixture<ChangeCourseLongTitleCommand>
    {
        protected override AbstractValidator<ChangeCourseLongTitleCommand> CreateValidator()
        {
            return new ChangeCourseLongTitleCommandValidator();
        }

        protected override ChangeCourseLongTitleCommand GetValidInstance()
        {
            return new ChangeCourseLongTitleCommand()
                       {
                           CourseId = Guid.NewGuid(),
                           NewLongTitle = "New Long Course Title"
                       };
        }

        [Test]
        public void CourseId_must_be_specified()
        {
            GetFailure(new ChangeCourseLongTitleCommand()
            {
                CourseId = default(Guid)
            }, cmd => cmd.CourseId);
        }


        [Test]
        public void LongTitle_cant_be_null()
        {
            GetFailure(new ChangeCourseLongTitleCommand()
            {
                NewLongTitle = null
            }, cmd => cmd.NewLongTitle);
        }

        [Test]
        public void LongTitle_cant_be_empty()
        {
            GetFailure(new ChangeCourseLongTitleCommand()
            {
                NewLongTitle = ""
            }, cmd => cmd.NewLongTitle);
        }



    }
}
