using System.Collections.Generic;
using NUnit.Framework;

namespace ISIS.DomainTests.CourseTests
{
    [TestFixture]
    public class when_a_course_title_is_changed : 
        SimpleCommandFixture<ChangeCourseTitleCommand, Course, CourseTitleChangedEvent>
    {

        private const string Rubric = "BIOL";
        private const string CourseNumber = "1301";
        private const string Title = "Introductory Biology";
        private const string NewTitle = "Introduction to Biology";

        protected override IEnumerable<object> Given()
        {
            yield return new CourseCreatedEvent()
                             {
                                 CourseId = EventSourceId,
                                 Rubric = Rubric,
                                 Number = CourseNumber,
                                 Title = Title
                             };
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
        public void then_it_should_create_a_new_CourseTitleChangedEvent()
        {
            Assert.That(TheEvent.CourseId, Is.EqualTo(EventSourceId));
            Assert.That(TheEvent.Title, Is.EqualTo(NewTitle));
        }


    }
}
