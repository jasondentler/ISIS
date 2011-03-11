using System;
using FluentValidation;
using NUnit.Framework;

namespace ISIS.Schedule
{
    [TestFixture]
    public class MakeCourseObsoleteCommandFixture : ValidationFixture<MakeCourseObsoleteCommand>
    {
        protected override AbstractValidator<MakeCourseObsoleteCommand> CreateValidator()
        {
            return new MakeCourseObsoleteCommandValidator();
        }

        protected override MakeCourseObsoleteCommand GetValidInstance()
        {
            return new MakeCourseObsoleteCommand()
                       {
                           CourseId = Guid.NewGuid()
                       };
        }

        [Test]
        public void CourseId_must_be_specified()
        {
            GetFailure(new MakeCourseObsoleteCommand()
            {
                CourseId = default(Guid)
            }, cmd => cmd.CourseId);
        }





    }
}
