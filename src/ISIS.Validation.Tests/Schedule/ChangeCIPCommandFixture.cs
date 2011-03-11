using System;
using FluentValidation;
using NUnit.Framework;

namespace ISIS.Schedule
{
    [TestFixture]
    public class ChangeCIPCommandFixture : ValidationFixture<ChangeCIPCommand>
    {
        protected override AbstractValidator<ChangeCIPCommand> CreateValidator()
        {
            return new ChangeCIPCommandValidator();
        }

        protected override ChangeCIPCommand GetValidInstance()
        {
            return new ChangeCIPCommand()
                       {
                           CIP = "123456",
                           CourseId = Guid.NewGuid()
                       };
        }

        [Test]
        public void CIP_must_not_be_null()
        {
            GetFailure(new ChangeCIPCommand()
            {
                CIP = null
            }, cmd => cmd.CIP);
        }

        [Test]
        public void CIP_must_not_be_empty()
        {
            GetFailure(new ChangeCIPCommand()
            {
                CIP = ""
            }, cmd => cmd.CIP);
        }

        [Test]
        public void CIP_must_be_6_digits_long()
        {
            GetFailure(new ChangeCIPCommand()
            {
                CIP = "12345"
            }, cmd => cmd.CIP);
        }

        [Test]
        public void CIP_must_be_all_numbers()
        {
            GetFailure(new ChangeCIPCommand()
            {
                CIP = "12345A"
            }, cmd => cmd.CIP);
        }

        [Test]
        public void CIP_must_not_contain_punctuation()
        {
            GetFailure(new ChangeCIPCommand()
            {
                CIP = "12.345"
            }, cmd => cmd.CIP);
        }

        [Test]
        public void CourseId_must_be_specified()
        {
            GetFailure(new ChangeCIPCommand()
            {
                CourseId = default(Guid)
            }, cmd => cmd.CourseId);
        }


    }
}
