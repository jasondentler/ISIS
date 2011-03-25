using System;
using FluentValidation;
using NUnit.Framework;

namespace ISIS.Schedule
{
    [TestFixture]
    public class ChangeTopicCodeDescriptionCommandFixture : ValidationFixture<ChangeTopicCodeDescriptionCommand>
    {
        protected override AbstractValidator<ChangeTopicCodeDescriptionCommand> CreateValidator()
        {
            return new ChangeTopicCodeDescriptionCommandValidator();
        }

        protected override ChangeTopicCodeDescriptionCommand GetValidInstance()
        {
            return new ChangeTopicCodeDescriptionCommand()
                       {
                           TopicCodeId = Guid.NewGuid(),
                           Description = "Banking/Finance"
                       };
        }


        [Test]
        public void TopicCodeId_must_not_be_default_guid()
        {
            GetFailure(new ChangeTopicCodeDescriptionCommand()
            {
                TopicCodeId = default(Guid)
            }, cmd => cmd.TopicCodeId);
        }

        [Test]
        public void Description_must_not_be_null()
        {
            GetFailure(new ChangeTopicCodeDescriptionCommand()
            {
                Description = null
            }, cmd => cmd.Description);
        }

        [Test]
        public void Description_must_not_be_empty_string()
        {
            GetFailure(new ChangeTopicCodeDescriptionCommand()
            {
                Description = string.Empty
            }, cmd => cmd.Description);
        }

        [Test]
        public void Description_must_not_be_whitespace()
        {
            GetFailure(new ChangeTopicCodeDescriptionCommand()
            {
                Description = "       "
            }, cmd => cmd.Description);
        }

        [Test]
        public void Description_must_not_start_with_whitespace()
        {
            GetFailure(new ChangeTopicCodeDescriptionCommand()
            {
                Description = " Banking / Finance"
            }, cmd => cmd.Description);
        }


    }
}
