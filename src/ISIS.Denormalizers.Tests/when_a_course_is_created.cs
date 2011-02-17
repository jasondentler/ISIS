using System.Collections.Generic;
using Ncqrs.Eventing.Sourcing;
using NUnit.Framework;

namespace ISIS.Denormalizers.Tests
{
    [TestFixture]
    public class when_a_course_is_created
        : DenormalizerFixture<CourseListDenormalizer, CourseCreatedEvent>
    {

        protected override CourseListDenormalizer CreateDenormalizer(IRepositoryFactory factory)
        {
            return new CourseListDenormalizer(factory.CreateRepository());
        }

        protected override IEnumerable<ISourcedEvent> Given()
        {
            return new ISourcedEvent[0];
        }

        protected override CourseCreatedEvent WhenHandling()
        {
            return new CourseCreatedEvent()
            {
                Rubric = "BIOL",
                Number = "2302",
                Title = "Anatomy & Physiology 2"
            };
        }

        [Test]
        public void it_inserts_a_row()
        {
            var row = Repository.Single<CourseList>(EventSourceId);
            var e = TheEvent;

            Assert.That(row.Id, Is.EqualTo(e.EventSourceId));
            Assert.That(row.Rubric, Is.EqualTo(e.Rubric));
            Assert.That(row.Number, Is.EqualTo(e.Number));
            Assert.That(row.Title, Is.EqualTo(e.Title));
        }

    }
}
