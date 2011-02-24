using System.Collections.Generic;
using Ninject;
using NUnit.Framework;

namespace ISIS.Denormalizers.Tests.CourseListTests
{
    [TestFixture]
    public class when_a_course_title_is_changed
        : DenormalizerFixture<CourseListDenormalizer, CourseTitleChangedEvent>
    {

        private const string Rubric = "BIOL";
        private const string CourseNumber = "1301";
        private const string NewTitle = "Introduction to Biology";


        protected override CourseListDenormalizer CreateDenormalizer()
        {
            return Kernel.Get<CourseListDenormalizer>();
        }

        protected override IEnumerable<object> Given()
        {
            yield return new CourseCreatedEvent(EventSourceId, Rubric, CourseNumber);
        }

        protected override CourseTitleChangedEvent WhenHandling()
        {
            return new CourseTitleChangedEvent(EventSourceId, NewTitle);
        }

        [Test]
        public void it_updated_a_row()
        {
            var row = Repository.Single<CourseList>(EventSourceId);
            var e = TheEvent;

            Assert.That(row.CourseId, Is.EqualTo(e.CourseId));
            Assert.That(row.Rubric, Is.EqualTo(Rubric));
            Assert.That(row.Number, Is.EqualTo(CourseNumber));
            Assert.That(row.Title, Is.EqualTo(e.Title));
        }

    }
}
