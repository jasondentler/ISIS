using System.Collections.Generic;
using Ncqrs.Spec;
using NUnit.Framework;

namespace ISIS.DomainTests.CourseTests
{
    [TestFixture]
    public class when_a_pending_course_is_made_pending : 
        DomainFixture<MakeCoursePendingCommand>
    {

        protected override IEnumerable<object> GivenEvents()
        {
            yield return new CourseCreatedEvent(EventSourceId, "BIOL", "1304");
            yield return new CourseMadePendingEvent(EventSourceId);
        }

        protected override MakeCoursePendingCommand WhenExecuting()
        {
            return new MakeCoursePendingCommand() { CourseId = EventSourceId };
        }

        [Then]
        public void it_should_do_nothing()
        {
            Assert.That(PublishedEvents, Is.Empty);
        }

        [Then]
        public void it_should_not_throw()
        {
            Assert.That(CaughtException, Is.Null);
        }

    }
}
