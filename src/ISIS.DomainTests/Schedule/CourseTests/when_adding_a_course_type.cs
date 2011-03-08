using System.Collections.Generic;
using Ncqrs.Spec;
using NUnit.Framework;

namespace ISIS.Schedule.CourseTests
{
    [Specification]
    public class when_adding_a_course_type : 
        SimpleDomainFixture<AddCourseTypeToCourseCommand, CourseTypeAddedToCourseEvent>
    {

        private const CourseTypes Type = CourseTypes.ACAD;

        protected override IEnumerable<object> GivenEvents()
        {
            yield return new CreditCourseCreatedEvent(EventSourceId, "BIOL", "2302");
        }

        protected override AddCourseTypeToCourseCommand WhenExecuting()
        {
            return new AddCourseTypeToCourseCommand()
                       {
                           CourseId = EventSourceId,
                           Type = Type
                       };
        }

        [Then]
        public void then_it_should_create_a_new_CourseTypeAddedToCourseEvent()
        {
            Assert.That(TheEvent.CourseId, Is.EqualTo(EventSourceId));
            Assert.That(TheEvent.Type, Is.EqualTo(Type));
        }


    }
}
