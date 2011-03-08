using System.Collections.Generic;
using Ncqrs.Spec;
using NUnit.Framework;

namespace ISIS.Schedule.CourseTests
{
    [Specification]
    public class when_a_course_long_title_is_changed : 
        SimpleDomainFixture<ChangeCourseLongTitleCommand, CourseLongTitleChangedEvent>
    {

        private const string Rubric = "BIOL";
        private const string CourseNumber = "1301";
        private const string NewLongTitle = "My very very very very very very very very very very long introduction to biology title goes here";

        protected override IEnumerable<object> GivenEvents()
        {
            yield return new CreditCourseCreatedEvent(EventSourceId, Rubric, CourseNumber);
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
        public void then_it_should_create_a_new_CourseLongTitleChangedEvent()
        {
            Assert.That(TheEvent.CourseId, Is.EqualTo(EventSourceId));
            Assert.That(TheEvent.LongTitle, Is.EqualTo(NewLongTitle));
        }


    }
}
