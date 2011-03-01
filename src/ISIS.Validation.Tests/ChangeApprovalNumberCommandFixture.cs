using System;
using FluentValidation;
using NUnit.Framework;

namespace ISIS.Validation.Tests
{
    [TestFixture]
    public class ChangeApprovalNumberCommandFixture : ValidationFixture<ChangeApprovalNumberCommand>
    {
        protected override AbstractValidator<ChangeApprovalNumberCommand> CreateValidator()
        {
            return new ChangeApprovalNumberCommandValidator();
        }

        protected override ChangeApprovalNumberCommand GetValidInstance()
        {
            return new ChangeApprovalNumberCommand()
                       {
                           ApprovalNumber = "1234567890",
                           CourseId = Guid.NewGuid()
                       };
        }

        [Test]
        public void Approval_number_must_not_be_null()
        {
            GetFailure(new ChangeApprovalNumberCommand()
            {
                ApprovalNumber = null
            }, cmd => cmd.ApprovalNumber);
        }

        [Test]
        public void Approval_number_must_be_empty()
        {
            GetFailure(new ChangeApprovalNumberCommand()
            {
                ApprovalNumber = ""
            }, cmd => cmd.ApprovalNumber);
        }

        [Test]
        public void Approval_number_must_be_10_digits_long()
        {
            GetFailure(new ChangeApprovalNumberCommand()
            {
                ApprovalNumber = "123456"
            }, cmd => cmd.ApprovalNumber);
        }

        [Test]
        public void Approval_number_must_be_all_numbers()
        {
            GetFailure(new ChangeApprovalNumberCommand()
            {
                ApprovalNumber = "123456789A"
            }, cmd => cmd.ApprovalNumber);
        }

        [Test]
        public void Approval_number_must_not_contain_punctuation()
        {
            GetFailure(new ChangeApprovalNumberCommand()
            {
                ApprovalNumber = "12.3456789"
            }, cmd => cmd.ApprovalNumber);
        }

        [Test]
        public void CourseId_must_be_specified()
        {
            GetFailure(new ChangeApprovalNumberCommand()
            {
                CourseId = default(Guid)
            }, cmd => cmd.CourseId);
        }





    }
}
