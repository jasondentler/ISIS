using System.Collections.Generic;
using Ninject;
using NUnit.Framework;

namespace ISIS.Denormalizers.Tests.CourseDetailsTests
{
    [TestFixture]
    public class when_a_course_is_made_obsolete
        : DenormalizerFixture<CourseDetailsDenormalizer, CourseMadeObsoleteEvent>
    {

        protected override CourseDetailsDenormalizer CreateDenormalizer()
        {
            return Kernel.Get<CourseDetailsDenormalizer>();
        }

        protected override IEnumerable<object> Given()
        {
            yield return new CourseCreatedEvent(EventSourceId, "BIOL", "2302");
        }

        protected override CourseMadeObsoleteEvent WhenHandling()
        {
            return new CourseMadeObsoleteEvent(EventSourceId);
        }

        [Test]
        public void it_sets_status()
        {
            var row = Repository.Single<CourseDetails>(EventSourceId);
            Assert.That(row.Status, Is.EqualTo(CourseStatuses.Obsolete));
        }

    }
}
