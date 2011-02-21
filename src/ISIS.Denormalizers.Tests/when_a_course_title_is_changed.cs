using System.Collections.Generic;
using Ncqrs.Eventing.Sourcing;
using NUnit.Framework;

namespace ISIS.Denormalizers.Tests
{
    [TestFixture]
    public class when_a_course_title_is_changed
        : DenormalizerFixture<CourseListDenormalizer, CourseTitleChangedEvent>
    {

        private const string Rubric = "BIOL";
        private const string CourseNumber = "1301";
        private const string Title = "Introductory Biology";
        private const string NewTitle = "Introduction to Biology";


        protected override CourseListDenormalizer CreateDenormalizer()
        {
            return new CourseListDenormalizer();
        }

        protected override IEnumerable<object> Given()
        {
            yield return new CourseCreatedEvent()
                             {
                                 Rubric = Rubric,
                                 Number = CourseNumber,
                                 Title = Title
                             };

        }

        protected override CourseTitleChangedEvent WhenHandling()
        {
            return new CourseTitleChangedEvent()
                       {
                           NewTitle = NewTitle
                       };
        }

        [Test]
        public void it_updated_a_row()
        {
            var row = Repository.Single<CourseList>(EventSourceId);
            var e = TheEvent;

            Assert.That(row.CourseId, Is.EqualTo(e.CourseId));
            Assert.That(row.Rubric, Is.EqualTo(Rubric));
            Assert.That(row.Number, Is.EqualTo(CourseNumber));
            Assert.That(row.Title, Is.EqualTo(e.NewTitle));
        }

    }
}
