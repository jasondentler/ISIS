using System.Collections.Generic;
using System.Linq;
using Ncqrs.Spec;
using NUnit.Framework;

namespace ISIS.Schedule.CourseTests
{
    [Specification]
    public class when_a_course_long_title_is_changed : 
        DomainFixture<ChangeCourseLongTitleCommand>
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
        public void then_it_should_not_throw()
        {
            Assert.That(CaughtException, Is.Null);
        }

        [Then]
        public void then_it_should_do_no_more()
        {
            Assert.That(PublishedEvents.Count(), Is.EqualTo(2));
        }

        [Then]
        public void then_it_should_create_a_new_CourseLongTitleChangedEvent()
        {
            var TheEvent = PublishedEvents
                .Select(e => e.Payload)
                .OfType<CourseLongTitleChangedEvent>()
                .Single();
            Assert.That(TheEvent.CourseId, Is.EqualTo(EventSourceId));
            Assert.That(TheEvent.LongTitle, Is.EqualTo(NewLongTitle));
        }

        [Then]
        public void then_it_should_create_a_new_CourseTitleChangedEvent()
        {
            var TheEvent = PublishedEvents
                .Select(e => e.Payload)
                .OfType<CourseTitleChangedEvent>()
                .Single();
            Assert.That(TheEvent.CourseId, Is.EqualTo(EventSourceId));
            Assert.That(TheEvent.Title, Is.EqualTo(NewLongTitle.Substring(0, 30)));
        }


    }
}
