using System;
using FluentValidation;
using NUnit.Framework;

namespace ISIS.Validation.Tests
{
    [TestFixture]
    public class AssignApprovalNumberCommandFixture : ValidationFixture<AssignApprovalNumberCommand>
    {
        protected override AbstractValidator<AssignApprovalNumberCommand> CreateValidator()
        {
            return new AssignApprovalNumberCommandValidator();
        }

        protected override AssignApprovalNumberCommand GetValidInstance()
        {
            return new AssignApprovalNumberCommand()
                       {
                           ApprovalNumber = "1234567890",
                           CourseId = Guid.NewGuid()
                       };
        }

        [Test]
        public void Approval_number_must_not_be_null()
        {
            GetFailure(new AssignApprovalNumberCommand()
            {
                ApprovalNumber = null
            }, cmd => cmd.ApprovalNumber);
        }

        [Test]
        public void Approval_number_must_be_empty()
        {
            GetFailure(new AssignApprovalNumberCommand()
            {
                ApprovalNumber = ""
            }, cmd => cmd.ApprovalNumber);
        }

        [Test]
        public void Approval_number_must_be_10_digits_long()
        {
            GetFailure(new AssignApprovalNumberCommand()
            {
                ApprovalNumber = "123456"
            }, cmd => cmd.ApprovalNumber);
        }

        [Test]
        public void Approval_number_must_be_all_numbers()
        {
            GetFailure(new AssignApprovalNumberCommand()
            {
                ApprovalNumber = "123456789A"
            }, cmd => cmd.ApprovalNumber);
        }

        [Test]
        public void Approval_number_must_not_contain_punctuation()
        {
            GetFailure(new AssignApprovalNumberCommand()
            {
                ApprovalNumber = "12.3456789"
            }, cmd => cmd.ApprovalNumber);
        }

        [Test]
        public void CourseId_must_be_specified()
        {
            GetFailure(new AssignApprovalNumberCommand()
            {
                CourseId = default(Guid)
            }, cmd => cmd.CourseId);
        }





    }
}
