using System;
using FluentValidation;
using NUnit.Framework;

namespace ISIS.Schedule
{
    [TestFixture]
    public class CreateTopicCodeCommandFixture : ValidationFixture<CreateTopicCodeCommand>
    {
        protected override AbstractValidator<CreateTopicCodeCommand> CreateValidator()
        {
            return new CreateTopicCodeCommandValidator();
        }

        protected override CreateTopicCodeCommand GetValidInstance()
        {
            return new CreateTopicCodeCommand()
                       {
                           Abbreviation = "BANK",
                           Description = "Banking/Finance"
                       };
        }

        [Test]
        public void Abbreviation_must_start_with_a_letter()
        {
            GetFailure(new CreateTopicCodeCommand()
                           {
                               Abbreviation = "0BANK"
                           }, cmd => cmd.Abbreviation);
        }

        [Test]
        public void Abbreviation_must_be_all_uppercase()
        {
            GetFailure(new CreateTopicCodeCommand()
            {
                Abbreviation = "Bank"
            }, cmd => cmd.Abbreviation);
        }

        [Test]
        public void Abbreviation_cant_contain_period()
        {
            GetFailure(new CreateTopicCodeCommand()
            {
                Abbreviation = "BANK."
            }, cmd => cmd.Abbreviation);
        }

        [Test]
        public void Abbreviation_cant_contain_dash()
        {
            GetFailure(new CreateTopicCodeCommand()
            {
                Abbreviation = "BA-NK"
            }, cmd => cmd.Abbreviation);
        }

        [Test]
        public void Abbreviation_cant_contain_plus()
        {
            GetFailure(new CreateTopicCodeCommand()
            {
                Abbreviation = "BA+NK"
            }, cmd => cmd.Abbreviation);
        }

        [Test]
        public void Abbreviation_cant_contain_underscore()
        {
            GetFailure(new CreateTopicCodeCommand()
            {
                Abbreviation = "BA_NK"
            }, cmd => cmd.Abbreviation);
        }

        [Test]
        public void Abbreviation_cant_contain_spaces()
        {
            GetFailure(new CreateTopicCodeCommand()
            {
                Abbreviation = "BA NK"
            }, cmd => cmd.Abbreviation);
        }

        [Test]
        public void Abbreviation_must_be_5_characters_or_less()
        {
            GetFailure(new CreateTopicCodeCommand()
            {
                Abbreviation = "BANKBA"
            }, cmd => cmd.Abbreviation);
        }

        [Test]
        public void Abbreviation_must_not_be_null()
        {
            GetFailure(new CreateTopicCodeCommand()
            {
                Abbreviation = null
            }, cmd => cmd.Abbreviation);
        }

        [Test]
        public void Abbreviation_must_not_be_empty_string()
        {
            GetFailure(new CreateTopicCodeCommand()
            {
                Abbreviation = string.Empty
            }, cmd => cmd.Abbreviation);
        }

        [Test]
        public void Description_must_not_be_null()
        {
            GetFailure(new CreateTopicCodeCommand()
            {
                Description = null
            }, cmd => cmd.Description);
        }

        [Test]
        public void Description_must_not_be_empty_string()
        {
            GetFailure(new CreateTopicCodeCommand()
            {
                Description = string.Empty
            }, cmd => cmd.Description);
        }

        [Test]
        public void Description_must_not_be_whitespace()
        {
            GetFailure(new CreateTopicCodeCommand()
            {
                Description = "       "
            }, cmd => cmd.Description);
        }

        [Test]
        public void Description_must_not_start_with_whitespace()
        {
            GetFailure(new CreateTopicCodeCommand()
            {
                Description = " Banking / Finance"
            }, cmd => cmd.Description);
        }


    }
}
