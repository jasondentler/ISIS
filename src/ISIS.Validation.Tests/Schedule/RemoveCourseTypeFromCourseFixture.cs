using System;
using FluentValidation;
using NUnit.Framework;

namespace ISIS.Schedule
{
    [TestFixture]
    public class RemoveCourseTypeFromCourseFixture : ValidationFixture<RemoveCourseTypeFromCourse>
    {
        protected override AbstractValidator<RemoveCourseTypeFromCourse> CreateValidator()
        {
            return new RemoveCourseTypeFromCourseCommandValidator();
        }

        protected override RemoveCourseTypeFromCourse GetValidInstance()
        {
            return new RemoveCourseTypeFromCourse()
                       {
                           CourseId = Guid.NewGuid(),
                           Type = CourseTypes.NROTC
                       };
        }

        [Test]
        public void CourseId_must_be_specified()
        {
            GetFailure(new RemoveCourseTypeFromCourse()
            {
                CourseId = default(Guid)
            }, cmd => cmd.CourseId);
        }

        [Test]
        public void CourseType_must_be_a_defined_enum_value()
        {
            GetFailure(new RemoveCourseTypeFromCourse()
                           {
                               Type = (CourseTypes) Enum.Parse(typeof (CourseTypes), "-12308932")
                           }, cmd => cmd.CourseId);
        }





    }
}
