using System;
using NUnit.Framework;

namespace ISIS.DomainTests.SectionTests
{
    [TestFixture]
    public class when_a_new_section_is_created : 
        SimpleCommandFixture<CreateSectionCommand, Section, SectionCreatedEvent>
    {

        private readonly Guid CourseId = Guid.NewGuid();
        private readonly Guid TermId = Guid.NewGuid();
        private const string SectionNumber = "01";

        protected override CreateSectionCommand WhenExecutingCommand()
        {
            return new CreateSectionCommand()
                       {
                           CourseId = CourseId,
                           TermId = TermId,
                           Number = SectionNumber
                       };
        }

        [Test]
        public void then_it_should_create_a_new_SectionCreatedEvent()
        {
            Assert.That(TheEvent.CourseId, Is.EqualTo(CourseId));
            Assert.That(TheEvent.TermId, Is.EqualTo(TermId));
            Assert.That(TheEvent.SectionNumber, Is.EqualTo(SectionNumber));

        }


    }
}
