using System.Collections.Generic;
using Ninject;
using NUnit.Framework;

namespace ISIS.Schedule.CourseDetailsTests
{
    [TestFixture]
    public class when_a_course_is_deactivated
        : DenormalizerFixture<CourseDetailsDenormalizer, CourseDeactivatedEvent>
    {

        protected override CourseDetailsDenormalizer CreateDenormalizer()
        {
            return Kernel.Get<CourseDetailsDenormalizer>();
        }

        protected override IEnumerable<object> Given()
        {
            yield return new CourseCreatedEvent(EventSourceId, "BIOL", "2302");
        }

        protected override CourseDeactivatedEvent WhenHandling()
        {
            return new CourseDeactivatedEvent(EventSourceId);
        }

        [Test]
        public void it_sets_status()
        {
            var row = Repository.Single<CourseDetails>(EventSourceId);
            Assert.That(row.Status, Is.EqualTo(CourseStatuses.Inactive));
        }

    }
}
