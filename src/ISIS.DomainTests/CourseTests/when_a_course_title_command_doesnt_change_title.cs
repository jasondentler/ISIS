using System.Collections.Generic;
using System.Linq;
using Ncqrs.Spec;
using NUnit.Framework;

namespace ISIS.DomainTests.CourseTests
{
    [TestFixture]
    public class when_a_course_title_command_doesnt_change_title : 
        DomainFixture<ChangeCourseTitleCommand>
    {

        private const string Rubric = "BIOL";
        private const string CourseNumber = "1301";
        private const string NewTitle = "My short title here";

        protected override IEnumerable<object> GivenEvents()
        {
            yield return new CourseCreatedEvent(EventSourceId, Rubric, CourseNumber);
            yield return new CourseTitleChangedEvent(EventSourceId, NewTitle);
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
