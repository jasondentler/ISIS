using System.Linq;
using NUnit.Framework;

namespace ISIS.DomainTests.CourseTests
{
    [TestFixture]
    public class when_a_new_course_is_created : 
        SimpleCommandFixture<CreateCourseCommand, Course, CourseCreatedEvent>
    {

        private const string Rubric = "BIOL";
        private const string CourseNumber = "1301";
        private const string Title = "Introductory Biology";

        protected override CreateCourseCommand WhenExecutingCommand()
        {
            return new CreateCourseCommand()
                       {
                           Rubric = Rubric,
                           CourseNumber = CourseNumber,
                           Title = Title
                       };
        }

        [Test]
        public void then_it_should_create_a_new_CourseCreatedEvent()
        {
            Assert.That(TheEvent.Rubric, Is.EqualTo(Rubric));
            Assert.That(TheEvent.Number, Is.EqualTo(CourseNumber));
            Assert.That(TheEvent.Title, Is.EqualTo(Title));
        }


    }
}
