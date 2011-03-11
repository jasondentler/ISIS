using System;
using FluentValidation;
using NUnit.Framework;

namespace ISIS.Schedule
{
    [TestFixture]
    public class AddCourseTypeToCourseCommandFixture : ValidationFixture<AddCourseTypeToCourseCommand>
    {
        protected override AbstractValidator<AddCourseTypeToCourseCommand> CreateValidator()
        {
            return new AddCourseTypeToCourseCommandValidator();
        }

        protected override AddCourseTypeToCourseCommand GetValidInstance()
        {
            return new AddCourseTypeToCourseCommand()
                       {
                           CourseId = Guid.NewGuid(),
                           Type = CourseTypes.NROTC
                       };
        }

        [Test]
        public void CourseId_must_be_specified()
        {
            GetFailure(new AddCourseTypeToCourseCommand()
            {
                CourseId = default(Guid)
            }, cmd => cmd.CourseId);
        }

        [Test]
        public void CourseType_must_be_a_defined_enum_value()
        {
            GetFailure(new AddCourseTypeToCourseCommand()
                           {
                               Type = (CourseTypes) Enum.Parse(typeof (CourseTypes), "-12308932")
                           }, cmd => cmd.CourseId);
        }





    }
}
