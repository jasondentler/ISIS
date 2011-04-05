using System;
using FluentValidation;
using NUnit.Framework;

namespace ISIS.Schedule
{
    [TestFixture]
    public class ChangeCourseCreditTypeFixture : ValidationFixture<ChangeCourseCreditTypeCommand>
    {
        protected override AbstractValidator<ChangeCourseCreditTypeCommand> CreateValidator()
        {
            return new ChangeCourseCreditTypeCommandValidator();
        }

        protected override ChangeCourseCreditTypeCommand GetValidInstance()
        {
            return new ChangeCourseCreditTypeCommand()
                       {
                           CourseId = Guid.NewGuid(),
                           Type = CreditTypes.WorkforceFunded
                       };
        }

        [Test]
        public void CourseId_must_be_specified()
        {
            GetFailure(new ChangeCourseCreditTypeCommand()
            {
                CourseId = default(Guid)
            }, cmd => cmd.CourseId);
        }

        [Test]
        public void CreditType_must_be_a_defined_enum_value()
        {
            GetFailure(new ChangeCourseCreditTypeCommand()
                           {
                               Type = (CreditTypes) Enum.Parse(typeof (CourseTypes), "-12308932")
                           }, cmd => cmd.Type);
        }





    }
}
