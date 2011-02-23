using System.Collections.Generic;
using Ninject;
using NUnit.Framework;

namespace ISIS.Denormalizers.Tests.CourseDetailsTests
{
    [TestFixture]
    public class when_a_course_is_assigned_a_CIP
        : DenormalizerFixture<CourseDetailsDenormalizer, CourseCIPAssignedEvent>
    {

        private const string Rubric = "BIOL";
        private const string CourseNumber = "1301";
        private const string Title = "Introductory Biology";
        private const string CIP = "1234567890";

        protected override CourseDetailsDenormalizer CreateDenormalizer()
        {
            return Kernel.Get<CourseDetailsDenormalizer>();
        }

        protected override IEnumerable<object> Given()
        {
            yield return new CourseCreatedEvent(EventSourceId, Rubric, CourseNumber, Title);
        }

        protected override CourseCIPAssignedEvent WhenHandling()
        {
            return new CourseCIPAssignedEvent(EventSourceId, CIP);
        }

        [Test]
        public void it_inserts_a_row()
        {
            var row = Repository.Single<CourseDetails>(EventSourceId);
            var e = TheEvent;
            Assert.That(row, Is.Not.Null);
            Assert.That(e, Is.Not.Null);
            Assert.That(row.CourseId, Is.EqualTo(EventSourceId));
            Assert.That(row.Rubric, Is.EqualTo(Rubric));
            Assert.That(row.Number, Is.EqualTo(CourseNumber));
            Assert.That(row.Title, Is.EqualTo(Title));
            Assert.That(row.ApprovalNumber, Is.EqualTo(null));
            Assert.That(row.CIP, Is.EqualTo(CIP));
        }

    }
}
