using System;
using System.Collections.Generic;
using Ncqrs.Spec;
using NUnit.Framework;

namespace ISIS.Schedule.SectionTests
{
    [Specification]
    public class when_a_new_section_is_created : 
        OneEventTestFixture<CreateSectionCommand, SectionCreatedEvent>
    {

        private readonly Guid CourseId = Guid.NewGuid();
        private readonly Guid TermId = Guid.NewGuid();
        private const string SectionNumber = "01";

        protected override IEnumerable<object> GivenEvents()
        {
            return new object[0];
        }

        protected override CreateSectionCommand WhenExecuting()
        {
            return new CreateSectionCommand()
            {
                CourseId = CourseId,
                TermId = TermId,
                Number = SectionNumber
            };
        }

        [Then]
        public void then_it_should_create_a_new_SectionCreatedEvent()
        {
            Assert.That(TheEvent.CourseId, Is.EqualTo(CourseId));
            Assert.That(TheEvent.TermId, Is.EqualTo(TermId));
            Assert.That(TheEvent.SectionNumber, Is.EqualTo(SectionNumber));

        }


    }
}
