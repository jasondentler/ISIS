using System.Collections.Generic;
using Ninject;
using NUnit.Framework;

namespace ISIS.Schedule.CourseDetailsTests
{
    [TestFixture]
    public class when_a_course_title_is_changed
        : DenormalizerFixture<CourseDetailsDenormalizer, CourseTitleChangedEvent>
    {

        private const string Rubric = "BIOL";
        private const string CourseNumber = "1301";
        private const string NewTitle = "Introduction to Biology";


        protected override CourseDetailsDenormalizer CreateDenormalizer()
        {
            return Kernel.Get<CourseDetailsDenormalizer>();
        }

        protected override IEnumerable<object> Given()
        {
            yield return new CreditCourseCreatedEvent(EventSourceId, Rubric, CourseNumber);
        }

        protected override CourseTitleChangedEvent WhenHandling()
        {
            return new CourseTitleChangedEvent(EventSourceId, NewTitle);
        }

        [Test]
        public void it_updated_a_row()
        {
            var row = Repository.Single<CourseDetails>(EventSourceId);
            var e = TheEvent;

            Assert.That(row.CourseId, Is.EqualTo(e.CourseId));
            Assert.That(row.Rubric, Is.EqualTo(Rubric));
            Assert.That(row.Number, Is.EqualTo(CourseNumber));
            Assert.That(row.Title, Is.EqualTo(e.Title));
            Assert.That(row.LongTitle, Is.EqualTo(null));
            Assert.That(row.ApprovalNumber, Is.EqualTo(null));
            Assert.That(row.CIP, Is.EqualTo(null));
        }

    }
}
