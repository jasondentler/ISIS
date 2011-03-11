using System;
using FluentValidation;
using NUnit.Framework;

namespace ISIS.Schedule
{
    [TestFixture]
    public class ActivateCourseCommandFixture : ValidationFixture<ActivateCourseCommand>
    {
        protected override AbstractValidator<ActivateCourseCommand> CreateValidator()
        {
            return new ActivateCourseCommandValidator();
        }

        protected override ActivateCourseCommand GetValidInstance()
        {
            return new ActivateCourseCommand()
                       {
                           CourseId = Guid.NewGuid()
                       };
        }

        [Test]
        public void CourseId_must_be_specified()
        {
            GetFailure(new ActivateCourseCommand()
            {
                CourseId = default(Guid)
            }, cmd => cmd.CourseId);
        }





    }
}
