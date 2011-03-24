using System;
using FluentValidation;
using NUnit.Framework;

namespace ISIS.Schedule
{
    [TestFixture]
    public class ChangeCourseCEUsCommandFixture : ValidationFixture<ChangeCourseCEUsCommand>
    {
        protected override AbstractValidator<ChangeCourseCEUsCommand> CreateValidator()
        {
            return new ChangeCourseCEUsCommandValidator();
        }

        protected override ChangeCourseCEUsCommand GetValidInstance()
        {
            return new ChangeCourseCEUsCommand()
                       {
                           CourseId = Guid.NewGuid(),
                           CEUs = 0.7M
                       };
        }

        [Test]
        public void CourseId_must_be_specified()
        {
            GetFailure(new ChangeCourseCEUsCommand()
            {
                CourseId = default(Guid)
            }, cmd => cmd.CourseId);
        }

        [Test]
        public void CEUs_must_be_non_negative()
        {
            GetFailure(new ChangeCourseCEUsCommand()
            {
                CEUs = -0.1M
            }, cmd => cmd.CEUs);
        }

        [Test]
        public void CEUs_must_be_reasonable()
        {
            GetFailure(new ChangeCourseCEUsCommand()
                           {
                               CEUs = 1000
                           }, cmd => cmd.CEUs);
        }

    }
}
