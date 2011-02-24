using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace ISIS.DomainTests.CourseTests
{
    [TestFixture]
    public class when_a_course_title_is_changed_and_long_title_matches : 
        CommandFixture<ChangeCourseTitleCommand, Course>
    {

        private const string Rubric = "BIOL";
        private const string CourseNumber = "1301";
        private const string NewTitle = "Introduction to Biology";

        protected override IEnumerable<object> Given()
        {
            yield return new CourseCreatedEvent(EventSourceId, Rubric, CourseNumber);
        }

        protected override ChangeCourseTitleCommand WhenExecutingCommand()
        {
            return new ChangeCourseTitleCommand()
                       {
                           CourseId = EventSourceId,
                           NewTitle = NewTitle
                       };
        }

        [Test]
        public void it_should_do_no_more()
        {
            Assert.That(PublishedEvents.Count(), Is.EqualTo(2));
        }

        [Test]
        public void then_it_should_create_a_new_CourseTitleChangedEvent()
        {
            var TheEvent = PublishedEvents.Select(pe => pe.Payload)
                .OfType<CourseTitleChangedEvent>().Single();
            Assert.That(TheEvent.CourseId, Is.EqualTo(EventSourceId));
            Assert.That(TheEvent.Title, Is.EqualTo(NewTitle));
        }

        [Test]
        public void then_it_should_create_a_new_CourseLongTitleChangedEvent()
        {
            var TheEvent = PublishedEvents.Select(pe => pe.Payload)
                .OfType<CourseLongTitleChangedEvent>().Single();
            Assert.That(TheEvent.CourseId, Is.Not.EqualTo(default(Guid)));
            Assert.That(TheEvent.LongTitle, Is.EqualTo(NewTitle));
        }
    }
}
