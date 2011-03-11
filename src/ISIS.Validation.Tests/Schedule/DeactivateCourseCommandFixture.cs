using System;
using FluentValidation;
using NUnit.Framework;

namespace ISIS.Schedule
{
    [TestFixture]
    public class DeactivateCourseCommandFixture : ValidationFixture<DeactivateCourseCommand>
    {
        protected override AbstractValidator<DeactivateCourseCommand> CreateValidator()
        {
            return new DeactivateCourseCommandValidator();
        }

        protected override DeactivateCourseCommand GetValidInstance()
        {
            return new DeactivateCourseCommand()
                       {
                           CourseId = Guid.NewGuid()
                       };
        }

        [Test]
        public void CourseId_must_be_specified()
        {
            GetFailure(new DeactivateCourseCommand()
            {
                CourseId = default(Guid)
            }, cmd => cmd.CourseId);
        }





    }
}
