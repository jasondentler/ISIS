using System.Linq;
using Ninject;
using NUnit.Framework;

namespace ISIS.Schedule.CourseTypesListTests
{
    [TestFixture]
    public class when_a_course_type_is_added_to_a_course
        : DenormalizerFixture<CourseTypesListDenormalizer, CourseTypeAddedToCourseEvent>
    {

        protected override CourseTypesListDenormalizer CreateDenormalizer()
        {
            return Kernel.Get<CourseTypesListDenormalizer>();
        }

        protected override CourseTypeAddedToCourseEvent WhenHandling()
        {
            return new CourseTypeAddedToCourseEvent(EventSourceId,
                                                    CourseTypes.ACAD, new[] {CourseTypes.ACAD});
        }

        [Test]
        public void it_inserts_a_row()
        {
            var rows = Repository.Execute(new LookupCourseTypesList(EventSourceId));
            var row = rows.Single();
            var e = TheEvent;
            Assert.That(row.CourseId, Is.EqualTo(EventSourceId));
            Assert.That(row.CourseType, Is.EqualTo(CourseTypes.ACAD));
        }

    }
}
