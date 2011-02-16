using FluentValidation;
using NUnit.Framework;

namespace ISIS.Validation.Tests
{
    [TestFixture]
    public class CreateTermCommandFixture : ValidationFixture<CreateTermCommand>
    {
        protected override AbstractValidator<CreateTermCommand> CreateValidator()
        {
            return new CreateTermCommandValidator();
        }

        protected override CreateTermCommand GetValidInstance()
        {
            return new CreateTermCommand()
                       {
                           Name = "Spring 2011"
                       };
        }

        [Test]
        public void Name_cant_be_empty()
        {
            GetFailure(new CreateTermCommand()
            {
                Name = ""
            }, cmd => cmd.Name);
        }

        [Test]
        public void Name_cant_be_null()
        {
            GetFailure(new CreateTermCommand()
            {
                Name = null
            }, cmd => cmd.Name);
        }
        
    }
}
