using System;
using NUnit.Framework;

namespace ISIS.Denormalizers.Tests
{
    [TestFixture]
    public class CourseListDenormalizerFixture
        : DenormalizerFixture<CourseListDenormalizer, CourseList>
    {

        [Test]
        public void CourseCreatedEvent_inserts_row()
        {
            var id = Guid.NewGuid();
            var e = new CourseCreatedEvent()
                        {
                            Rubric = "BIOL",
                            Number = "2302",
                            Title = "Anatomy & Physiology 2"
                        };
            e.ClaimEvent(id, 0);

            Denormalizer.Handle(e);

            var data = Entity.Single(id);
            Assert.That(data.ID, Is.EqualTo(id));
            Assert.That(data.Rubric, Is.EqualTo(e.Rubric));
            Assert.That(data.Number, Is.EqualTo(e.Number));
            Assert.That(data.Title, Is.EqualTo(e.Title));
        }

    }
}
