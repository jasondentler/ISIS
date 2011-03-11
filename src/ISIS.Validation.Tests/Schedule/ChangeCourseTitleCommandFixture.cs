using System;
using FluentValidation;
using NUnit.Framework;

namespace ISIS.Schedule
{
    [TestFixture]
    public class ChangeCourseTitleCommandFixture : ValidationFixture<ChangeCourseTitleCommand>
    {
        protected override AbstractValidator<ChangeCourseTitleCommand> CreateValidator()
        {
            return new ChangeCourseTitleCommandValidator();
        }

        protected override ChangeCourseTitleCommand GetValidInstance()
        {
            return new ChangeCourseTitleCommand()
                       {
                           CourseId = Guid.NewGuid(),
                           NewTitle = "New Course Title"
                       };
        }

        [Test]
        public void CourseId_must_be_specified()
        {
            GetFailure(new ChangeCourseTitleCommand()
            {
                CourseId = default(Guid)
            }, cmd => cmd.CourseId);
        }


        [Test]
        public void Title_cant_be_null()
        {
            GetFailure(new ChangeCourseTitleCommand()
            {
                NewTitle = null
            }, cmd => cmd.NewTitle);
        }

        [Test]
        public void Title_cant_be_empty()
        {
            GetFailure(new ChangeCourseTitleCommand()
            {
                NewTitle = ""
            }, cmd => cmd.NewTitle);
        }


        [Test]
        public void Title_cant_be_over_30_characters()
        {
            GetFailure(new ChangeCourseTitleCommand()
            {
                NewTitle = "123456789012345678901234567890!"
            }, cmd => cmd.NewTitle);
        }

    }
}
