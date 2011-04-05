using System;
using FluentValidation;
using NUnit.Framework;

namespace ISIS.Schedule
{
    [TestFixture]
    public class ChangeSectionCreditTypeFixture : ValidationFixture<ChangeSectionCreditTypeCommand>
    {
        protected override AbstractValidator<ChangeSectionCreditTypeCommand> CreateValidator()
        {
            return new ChangeSectionCreditTypeCommandValidator();
        }

        protected override ChangeSectionCreditTypeCommand GetValidInstance()
        {
            return new ChangeSectionCreditTypeCommand()
                       {
                           SectionId = Guid.NewGuid(),
                           CreditType = CreditTypes.WorkforceFunded
                       };
        }

        [Test]
        public void CourseId_must_be_specified()
        {
            GetFailure(new ChangeSectionCreditTypeCommand()
            {
                SectionId = default(Guid)
            }, cmd => cmd.SectionId);
        }

        [Test]
        public void CreditType_must_be_a_defined_enum_value()
        {
            GetFailure(new ChangeSectionCreditTypeCommand()
                           {
                               CreditType = (CreditTypes) Enum.Parse(typeof (CourseTypes), "-12308932")
                           }, cmd => cmd.CreditType);
        }





    }
}
