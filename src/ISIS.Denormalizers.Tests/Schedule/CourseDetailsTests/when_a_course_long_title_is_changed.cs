using System.Collections.Generic;
using Ninject;
using NUnit.Framework;

namespace ISIS.Schedule.CourseDetailsTests
{
    [TestFixture]
    public class when_a_course_long_title_is_changed
        : DenormalizerFixture<CourseDetailsDenormalizer, CourseLongTitleChangedEvent>
    {

        private const string Rubric = "BIOL";
        private const string CourseNumber = "1301";
        private const string NewLongTitle = "Introduction to Biology";

        protected override CourseDetailsDenormalizer CreateDenormalizer()
        {
            return Kernel.Get<CourseDetailsDenormalizer>();
        }

        protected override IEnumerable<object> Given()
        {
            yield return new CourseCreatedEvent(EventSourceId, Rubric, CourseNumber);
        }

        protected override CourseLongTitleChangedEvent WhenHandling()
        {
            return new CourseLongTitleChangedEvent(EventSourceId, NewLongTitle);
        }

        [Test]
        public void it_updated_a_row()
        {
            var row = Repository.Single<CourseDetails>(EventSourceId);
            var e = TheEvent;

            Assert.That(row.CourseId, Is.EqualTo(e.CourseId));
            Assert.That(row.Rubric, Is.EqualTo(Rubric));
            Assert.That(row.Number, Is.EqualTo(CourseNumber));
            Assert.That(row.Title, Is.EqualTo(null));
            Assert.That(row.LongTitle, Is.EqualTo(NewLongTitle));
            Assert.That(row.ApprovalNumber, Is.EqualTo(null));
            Assert.That(row.CIP, Is.EqualTo(null));
        }

    }
}
