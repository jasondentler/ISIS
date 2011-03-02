using System.Collections.Generic;
using Ncqrs.Spec;
using NUnit.Framework;

namespace ISIS.Schedule.CourseTests
{
    [Specification]
    public class when_a_course_is_made_obsolete : 
        SimpleDomainFixture<MakeCourseObsoleteCommand, CourseMadeObsoleteEvent>
    {

        protected override IEnumerable<object> GivenEvents()
        {
            yield return new CourseCreatedEvent(EventSourceId, "BIOL", "1304");
        }

        protected override MakeCourseObsoleteCommand WhenExecuting()
        {
            return new MakeCourseObsoleteCommand() { CourseId = EventSourceId };
        }

        [Then]
        public void it_should_make_the_course_obsolete()
        {
            Assert.That(TheEvent.CourseId, Is.EqualTo(EventSourceId));
        }

    }
}
