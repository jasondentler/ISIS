using System;
using FluentValidation;
using NUnit.Framework;

namespace ISIS.Schedule
{
    [TestFixture]
    public class MakeCoursePendingCommandFixture : ValidationFixture<MakeCoursePendingCommand>
    {
        protected override AbstractValidator<MakeCoursePendingCommand> CreateValidator()
        {
            return new MakeCoursePendingCommandValidator();
        }

        protected override MakeCoursePendingCommand GetValidInstance()
        {
            return new MakeCoursePendingCommand()
                       {
                           CourseId = Guid.NewGuid()
                       };
        }

        [Test]
        public void CourseId_must_be_specified()
        {
            GetFailure(new MakeCoursePendingCommand()
            {
                CourseId = default(Guid)
            }, cmd => cmd.CourseId);
        }





    }
}
