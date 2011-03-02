using FluentValidation;
using ISIS.Validation.Tests;
using NUnit.Framework;

namespace ISIS.Schedule
{
    [TestFixture]
    public class CreateDepartmentCommandFixture : ValidationFixture<CreateDepartmentCommand>
    {
        protected override AbstractValidator<CreateDepartmentCommand> CreateValidator()
        {
            return new CreateDepartmentCommandValidator();
        }

        protected override CreateDepartmentCommand GetValidInstance()
        {
            return new CreateDepartmentCommand()
                       {
                           Name = "Biology"
                       };
        }

        [Test]
        public void Name_cant_be_empty()
        {
            GetFailure(new CreateDepartmentCommand()
            {
                Name = ""
            }, cmd => cmd.Name);
        }

        [Test]
        public void Name_cant_be_null()
        {
            GetFailure(new CreateDepartmentCommand()
            {
                Name = null
            }, cmd => cmd.Name);
        }
        
    }
}
