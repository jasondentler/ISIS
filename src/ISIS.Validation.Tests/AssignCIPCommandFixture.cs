using System;
using FluentValidation;
using NUnit.Framework;

namespace ISIS.Validation.Tests
{
    [TestFixture]
    public class AssignCIPCommandFixture : ValidationFixture<AssignCIPCommand>
    {
        protected override AbstractValidator<AssignCIPCommand> CreateValidator()
        {
            return new AssignCIPCommandValidator();
        }

        protected override AssignCIPCommand GetValidInstance()
        {
            return new AssignCIPCommand()
                       {
                           CIP = "123456",
                           CourseId = Guid.NewGuid()
                       };
        }

        [Test]
        public void CIP_must_not_be_null()
        {
            GetFailure(new AssignCIPCommand()
            {
                CIP = null
            }, cmd => cmd.CIP);
        }

        [Test]
        public void CIP_must_not_be_empty()
        {
            GetFailure(new AssignCIPCommand()
            {
                CIP = ""
            }, cmd => cmd.CIP);
        }

        [Test]
        public void CIP_must_be_6_digits_long()
        {
            GetFailure(new AssignCIPCommand()
            {
                CIP = "12345"
            }, cmd => cmd.CIP);
        }

        [Test]
        public void CIP_must_be_all_numbers()
        {
            GetFailure(new AssignCIPCommand()
            {
                CIP = "12345A"
            }, cmd => cmd.CIP);
        }

        [Test]
        public void CIP_must_not_contain_punctuation()
        {
            GetFailure(new AssignCIPCommand()
            {
                CIP = "12.345"
            }, cmd => cmd.CIP);
        }

        [Test]
        public void CourseId_must_be_specified()
        {
            GetFailure(new AssignCIPCommand()
            {
                CourseId = default(Guid)
            }, cmd => cmd.CourseId);
        }


    }
}
