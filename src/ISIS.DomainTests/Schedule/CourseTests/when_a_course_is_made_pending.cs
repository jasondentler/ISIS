using System.Collections.Generic;
using Ncqrs.Spec;
using NUnit.Framework;

namespace ISIS.Schedule.CourseTests
{
    [Specification]
    public class when_a_course_is_made_pending : 
        SimpleDomainFixture<MakeCoursePendingCommand, CourseMadePendingEvent>
    {

        protected override IEnumerable<object> GivenEvents()
        {
            yield return new CourseCreatedEvent(EventSourceId, "BIOL", "1304");
        }

        protected override MakeCoursePendingCommand WhenExecuting()
        {
            return new MakeCoursePendingCommand() { CourseId = EventSourceId };
        }

        [Then]
        public void it_should_make_the_course_pending()
        {
            Assert.That(TheEvent.CourseId, Is.EqualTo(EventSourceId));
        }

    }
}
