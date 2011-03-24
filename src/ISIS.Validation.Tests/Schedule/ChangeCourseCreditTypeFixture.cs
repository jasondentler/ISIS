using System;
using FluentValidation;
using NUnit.Framework;

namespace ISIS.Schedule
{
    [TestFixture]
    public class ChangeCourseCreditTypeFixture : ValidationFixture<ChangeCourseCreditType>
    {
        protected override AbstractValidator<ChangeCourseCreditType> CreateValidator()
        {
            return new ChangeCourseCreditTypeValidator();
        }

        protected override ChangeCourseCreditType GetValidInstance()
        {
            return new ChangeCourseCreditType()
                       {
                           CourseId = Guid.NewGuid(),
                           Type = CreditTypes.WorkforceFunded
                       };
        }

        [Test]
        public void CourseId_must_be_specified()
        {
            GetFailure(new ChangeCourseCreditType()
            {
                CourseId = default(Guid)
            }, cmd => cmd.CourseId);
        }

        [Test]
        public void CreditType_must_be_a_defined_enum_value()
        {
            GetFailure(new ChangeCourseCreditType()
                           {
                               Type = (CreditTypes) Enum.Parse(typeof (CourseTypes), "-12308932")
                           }, cmd => cmd.CourseId);
        }





    }
}
