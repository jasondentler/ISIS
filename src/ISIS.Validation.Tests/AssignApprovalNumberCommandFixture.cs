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





    }
}
