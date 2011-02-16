using FluentValidation;

namespace ISIS.Validation
{
    /// <summary>
    /// Validation rules for AssignApprovalNumberCommand
    /// </summary>
    public class AssignApprovalNumberCommandValidator : AbstractValidator<AssignApprovalNumberCommand>
    {
        public AssignApprovalNumberCommandValidator()
        {
            RuleFor(cmd => cmd.ApprovalNumber)
                .Matches(@"^\d{10}$")
                .WithMessage("Approval number must be 10 digits long");
        }
    }
}
