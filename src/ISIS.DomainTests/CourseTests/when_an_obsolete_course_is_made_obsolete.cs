using System.Collections.Generic;
using Ncqrs.Spec;
using NUnit.Framework;

namespace ISIS.DomainTests.CourseTests
{
    [TestFixture]
    public class when_an_obsolete_course_is_made_obsolete : 
        DomainFixture<MakeCourseObsoleteCommand>
    {

        protected override IEnumerable<object> GivenEvents()
        {
            yield return new CourseCreatedEvent(EventSourceId, "BIOL", "1304");
            yield return new CourseMadeObsoleteEvent(EventSourceId);
        }

        protected override MakeCourseObsoleteCommand WhenExecuting()
        {
            return new MakeCourseObsoleteCommand() { CourseId = EventSourceId };
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
