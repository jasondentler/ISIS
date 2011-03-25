using System;
using FluentValidation;
using NUnit.Framework;

namespace ISIS.Schedule
{
    [TestFixture]
    public class ChangeCourseTopicCodeCommandFixture : ValidationFixture<ChangeCourseTopicCodeCommand>
    {
        protected override AbstractValidator<ChangeCourseTopicCodeCommand> CreateValidator()
        {
            return new ChangeCourseTopicCodeCommandValidator();
        }

        protected override ChangeCourseTopicCodeCommand GetValidInstance()
        {
            return new ChangeCourseTopicCodeCommand()
                       {
                           CourseId = Guid.NewGuid(),
                           TopicCodeId = Guid.NewGuid()
                       };
        }

        [Test]
        public void CourseId_must_not_be_default_guid()
        {
            GetFailure(new ChangeCourseTopicCodeCommand()
            {
                CourseId = default(Guid)
            }, cmd => cmd.CourseId);
        }

        [Test]
        public void TopicCodeId_must_not_be_default_guid()
        {
            GetFailure(new ChangeCourseTopicCodeCommand()
            {
                TopicCodeId = default(Guid)
            }, cmd => cmd.TopicCodeId);
        }

    }
}
