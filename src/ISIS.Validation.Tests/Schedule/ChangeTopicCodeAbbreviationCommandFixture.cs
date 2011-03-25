using System;
using FluentValidation;
using NUnit.Framework;

namespace ISIS.Schedule
{
    [TestFixture]
    public class ChangeTopicCodeAbbreviationCommandFixture : ValidationFixture<ChangeTopicCodeAbbreviationCommand>
    {
        protected override AbstractValidator<ChangeTopicCodeAbbreviationCommand> CreateValidator()
        {
            return new ChangeTopicCodeAbbreviationCommandValidator();
        }

        protected override ChangeTopicCodeAbbreviationCommand GetValidInstance()
        {
            return new ChangeTopicCodeAbbreviationCommand()
                       {
                           TopicCodeId = Guid.NewGuid(),
                           Abbreviation = "MONEY"
                       };
        }

        [Test]
        public void TopicCodeId_must_not_be_default_guid()
        {
            GetFailure(new ChangeTopicCodeAbbreviationCommand()
                           {
                               TopicCodeId = default(Guid)
                           }, cmd => cmd.TopicCodeId);
        }

        [Test]
        public void Abbreviation_must_start_with_a_letter()
        {
            GetFailure(new ChangeTopicCodeAbbreviationCommand()
            {
                Abbreviation = "0BANK"
            }, cmd => cmd.Abbreviation);
        }

        [Test]
        public void Abbreviation_must_be_all_uppercase()
        {
            GetFailure(new ChangeTopicCodeAbbreviationCommand()
            {
                Abbreviation = "Bank"
            }, cmd => cmd.Abbreviation);
        }

        [Test]
        public void Abbreviation_cant_contain_period()
        {
            GetFailure(new ChangeTopicCodeAbbreviationCommand()
            {
                Abbreviation = "BANK."
            }, cmd => cmd.Abbreviation);
        }

        [Test]
        public void Abbreviation_cant_contain_dash()
        {
            GetFailure(new ChangeTopicCodeAbbreviationCommand()
            {
                Abbreviation = "BA-NK"
            }, cmd => cmd.Abbreviation);
        }

        [Test]
        public void Abbreviation_cant_contain_plus()
        {
            GetFailure(new ChangeTopicCodeAbbreviationCommand()
            {
                Abbreviation = "BA+NK"
            }, cmd => cmd.Abbreviation);
        }

        [Test]
        public void Abbreviation_cant_contain_underscore()
        {
            GetFailure(new ChangeTopicCodeAbbreviationCommand()
            {
                Abbreviation = "BA_NK"
            }, cmd => cmd.Abbreviation);
        }

        [Test]
        public void Abbreviation_cant_contain_spaces()
        {
            GetFailure(new ChangeTopicCodeAbbreviationCommand()
            {
                Abbreviation = "BA NK"
            }, cmd => cmd.Abbreviation);
        }

        [Test]
        public void Abbreviation_must_be_5_characters_or_less()
        {
            GetFailure(new ChangeTopicCodeAbbreviationCommand()
            {
                Abbreviation = "BANKBA"
            }, cmd => cmd.Abbreviation);
        }

        [Test]
        public void Abbreviation_must_not_be_null()
        {
            GetFailure(new ChangeTopicCodeAbbreviationCommand()
            {
                Abbreviation = null
            }, cmd => cmd.Abbreviation);
        }

        [Test]
        public void Abbreviation_must_not_be_empty_string()
        {
            GetFailure(new ChangeTopicCodeAbbreviationCommand()
            {
                Abbreviation = string.Empty
            }, cmd => cmd.Abbreviation);
        }
        
    }
}
