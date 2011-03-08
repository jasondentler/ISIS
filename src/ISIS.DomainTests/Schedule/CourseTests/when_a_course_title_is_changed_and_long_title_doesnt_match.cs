using System.Collections.Generic;
using Ncqrs.Spec;
using NUnit.Framework;

namespace ISIS.Schedule.CourseTests
{
    [Specification]
    public class when_a_course_title_is_changed_and_long_title_doesnt_match :
        SimpleDomainFixture<ChangeCourseTitleCommand, CourseTitleChangedEvent>
    {

        private const string Rubric = "BIOL";
        private const string CourseNumber = "1301";
        private const string NewTitle = "Introduction to Biology";

        protected override IEnumerable<object> GivenEvents()
        {
            yield return new CreditCourseCreatedEvent(EventSourceId, Rubric, CourseNumber);
            yield return new CourseLongTitleChangedEvent(EventSourceId, "Some other long title goes here.");
        }

        protected override ChangeCourseTitleCommand WhenExecuting()
        {
            return new ChangeCourseTitleCommand()
            {
                CourseId = EventSourceId,
                NewTitle = NewTitle
            };
        }

        [Then]
        public void then_it_should_create_a_new_CourseTitleChangedEvent()
        {
            Assert.That(TheEvent.CourseId, Is.EqualTo(EventSourceId));
            Assert.That(TheEvent.Title, Is.EqualTo(NewTitle));
        }

    }
}
