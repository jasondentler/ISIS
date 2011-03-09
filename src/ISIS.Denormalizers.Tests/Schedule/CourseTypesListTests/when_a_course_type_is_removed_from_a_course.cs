using System.Collections.Generic;
using System.Linq;
using Ninject;
using NUnit.Framework;

namespace ISIS.Schedule.CourseTypesListTests
{
    [TestFixture]
    public class when_a_course_type_is_removed_from_a_course
        : DenormalizerFixture<CourseTypesListDenormalizer, CourseTypeRemovedFromCourseEvent>
    {

        protected override CourseTypesListDenormalizer CreateDenormalizer()
        {
            return Kernel.Get<CourseTypesListDenormalizer>();
        }

        protected override IEnumerable<object> Given()
        {
            yield return new CourseTypeAddedToCourseEvent(
                EventSourceId,
                CourseTypes.ACAD,
                new[] { CourseTypes.ACAD });
            yield return new CourseTypeAddedToCourseEvent(
                EventSourceId,
                CourseTypes.NF,
                new[] {CourseTypes.ACAD, CourseTypes.NF});
        }

        protected override CourseTypeRemovedFromCourseEvent WhenHandling()
        {
            return new CourseTypeRemovedFromCourseEvent(EventSourceId,
                                                        CourseTypes.ACAD, new[] {CourseTypes.NF});
        }

        [Test]
        public void it_deletes_a_row()
        {
            var rows = Repository.Execute(new LookupCourseTypesList(EventSourceId));
            var row = rows.Single();
            var e = TheEvent;
            Assert.That(row.CourseId, Is.EqualTo(EventSourceId));
            Assert.That(row.CourseType, Is.EqualTo(CourseTypes.NF));
        }

    }
}
