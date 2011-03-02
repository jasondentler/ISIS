using Ninject;
using NUnit.Framework;

namespace ISIS.Denormalizers.Tests.CourseDetailsTests
{
    [TestFixture]
    public class when_a_course_is_created
        : DenormalizerFixture<CourseDetailsDenormalizer, CourseCreatedEvent>
    {

        protected override CourseDetailsDenormalizer CreateDenormalizer()
        {
            return Kernel.Get<CourseDetailsDenormalizer>();
        }

        protected override CourseCreatedEvent WhenHandling()
        {
            return new CourseCreatedEvent(EventSourceId, "BIOL", "2302");
        }

        [Test]
        public void it_inserts_a_row()
        {
            var row = Repository.Single<CourseDetails>(EventSourceId);
            var e = TheEvent;
            Assert.That(row, Is.Not.Null);
            Assert.That(e, Is.Not.Null);
            Assert.That(row.CourseId, Is.EqualTo(e.CourseId));
            Assert.That(row.Rubric, Is.EqualTo(e.Rubric));
            Assert.That(row.Number, Is.EqualTo(e.Number));
            Assert.That(row.Title, Is.EqualTo(null));
            Assert.That(row.LongTitle, Is.EqualTo(null));
            Assert.That(row.ApprovalNumber, Is.EqualTo(null));
            Assert.That(row.CIP, Is.EqualTo(null));
            Assert.That(row.Status, Is.EqualTo(CourseStatuses.Active));
        }

    }
}
