using System.Collections.Generic;
using NUnit.Framework;

namespace ISIS.DomainTests.CourseTests
{
    [TestFixture]
    public class when_a_course_description_is_changed : 
        SimpleDomainFixture<ChangeCourseDescriptionCommand, CourseDescriptionChangedEvent>
    {

        private const string Rubric = "BIOL";
        private const string CourseNumber = "1301";
        private const string Description = "My very very very very very very very very very very long introduction to biology title goes here";

        protected override IEnumerable<object> GivenEvents()
        {
            yield return new CourseCreatedEvent(EventSourceId, Rubric, CourseNumber);
        }

        protected override ChangeCourseDescriptionCommand WhenExecuting()
        {
            return new ChangeCourseDescriptionCommand()
            {
                CourseId = EventSourceId,
                NewDescription = Description
            };
        }

        [Test]
        public void then_it_should_create_a_new_CourseDescriptionChangedEvent()
        {
            Assert.That(TheEvent.CourseId, Is.EqualTo(EventSourceId));
            Assert.That(TheEvent.Description, Is.EqualTo(Description));
        }


    }
}
