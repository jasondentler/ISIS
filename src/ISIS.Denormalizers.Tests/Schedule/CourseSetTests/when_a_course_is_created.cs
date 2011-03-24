using Ninject;
using NUnit.Framework;

namespace ISIS.Schedule.CourseSetTests
{
    [TestFixture]
    public class when_a_course_is_created
        : DenormalizerFixture<CourseSetDenormalizer, CourseCreatedEvent>
    {

        protected override CourseSetDenormalizer CreateDenormalizer()
        {
            return Kernel.Get<CourseSetDenormalizer>();
        }

        protected override CourseCreatedEvent WhenHandling()
        {
            return new CourseCreatedEvent(EventSourceId, "BIOL", "2302");
        }

        [Test]
        public void it_inserts_a_row()
        {
            var row = Repository.Single<CourseSet>(EventSourceId);
            var e = TheEvent;
            Assert.That(row, Is.Not.Null);
            Assert.That(e, Is.Not.Null);
            Assert.That(row.CourseId, Is.EqualTo(e.CourseId));
            Assert.That(row.Rubric, Is.EqualTo(e.Rubric));
            Assert.That(row.Number, Is.EqualTo(e.Number));
        }

    }
}
