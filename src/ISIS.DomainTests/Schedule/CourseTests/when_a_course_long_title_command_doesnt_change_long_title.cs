using System.Collections.Generic;
using System.Linq;
using Ncqrs.Spec;
using NUnit.Framework;

namespace ISIS.Schedule.CourseTests
{
    [Specification]
    public class when_a_course_long_title_command_doesnt_change_long_title : 
        DomainFixture<ChangeCourseLongTitleCommand>
    {

        private const string Rubric = "BIOL";
        private const string CourseNumber = "1301";
        private const string NewLongTitle = "My very very very very very very very very very very long introduction to biology title goes here";

        protected override IEnumerable<object> GivenEvents()
        {
            yield return new CourseCreatedEvent(EventSourceId, Rubric, CourseNumber);
            yield return new CourseLongTitleChangedEvent(EventSourceId, NewLongTitle);
        }

        protected override ChangeCourseLongTitleCommand WhenExecuting()
        {
            return new ChangeCourseLongTitleCommand()
            {
                CourseId = EventSourceId,
                NewLongTitle = NewLongTitle
            };
        }

        [Then]
        public void it_should_do_nothing()
        {
            Assert.That(PublishedEvents.Count(), Is.EqualTo(0));
        }

        [Then]
        public void it_should_not_throw()
        {
            Assert.That(CaughtException, Is.Null);
        }


    }
}
