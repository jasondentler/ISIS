using System;
using System.Collections.Generic;
using System.Linq;
using Ninject;
using NUnit.Framework;

namespace ISIS.Schedule.CourseDetailsTests
{
    [TestFixture]
    public class when_a_course_type_is_removed
        : DenormalizerFixture<CourseDetailsDenormalizer, CourseTypeRemovedFromCourseEvent>
    {

        protected override CourseDetailsDenormalizer CreateDenormalizer()
        {
            return Kernel.Get<CourseDetailsDenormalizer>();
        }

        protected override IEnumerable<object> Given()
        {
            yield return new CreditCourseCreatedEvent(EventSourceId,
                                                      "BIOL", "1301");
            yield return new CourseTypeAddedToCourseEvent(
                EventSourceId,
                CourseTypes.NF,
                new[] { CourseTypes.NF });
            yield return new CourseTypeAddedToCourseEvent(
                EventSourceId,
                CourseTypes.ACAD,
                new[] {CourseTypes.NF, CourseTypes.ACAD});
        }

        protected override CourseTypeRemovedFromCourseEvent WhenHandling()
        {
            return new CourseTypeRemovedFromCourseEvent(
                EventSourceId,
                CourseTypes.NF,
                new[] {CourseTypes.ACAD});
        }
        
        [Test]
        public void it_updated_the_course_type()
        {
            var row = Repository.Single<CourseDetails>(EventSourceId);
            var e = TheEvent;

            var courseTypes = new[] {CourseTypes.ACAD}
                .Select(ct => (Enum) ct);
            var courseTypeNames = EnumData.GetNamesForValues(typeof (CourseTypes), courseTypes);
            var courseTypeString = string.Join(", ", courseTypeNames);

            Assert.That(row, Is.Not.Null);
            Assert.That(e, Is.Not.Null);
            Assert.That(row.CourseId, Is.EqualTo(e.CourseId));
            Assert.That(row.CourseTypes, Is.EqualTo(courseTypeString));
        }

    }
}
