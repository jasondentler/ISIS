using System;
using FluentValidation;
using NUnit.Framework;

namespace ISIS.Schedule
{
    [TestFixture]
    public class RemoveCourseTypeFromCourseFixture : ValidationFixture<RemoveCourseTypeFromCourseCommand>
    {
        protected override AbstractValidator<RemoveCourseTypeFromCourseCommand> CreateValidator()
        {
            return new RemoveCourseTypeFromCourseCommandValidator();
        }

        protected override RemoveCourseTypeFromCourseCommand GetValidInstance()
        {
            return new RemoveCourseTypeFromCourseCommand()
                       {
                           CourseId = Guid.NewGuid(),
                           Type = CourseTypes.NROTC
                       };
        }

        [Test]
        public void CourseId_must_be_specified()
        {
            GetFailure(new RemoveCourseTypeFromCourseCommand()
            {
                CourseId = default(Guid)
            }, cmd => cmd.CourseId);
        }

        [Test]
        public void CourseType_must_be_a_defined_enum_value()
        {
            GetFailure(new RemoveCourseTypeFromCourseCommand()
                           {
                               Type = (CourseTypes) Enum.Parse(typeof (CourseTypes), "-12308932")
                           }, cmd => cmd.CourseId);
        }





    }
}
