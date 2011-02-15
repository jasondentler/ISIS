using System.Collections.Generic;
using System.Linq;
using Ncqrs.Eventing.Sourcing;
using NUnit.Framework;

namespace ISIS.DomainTests.CourseTests
{
    [TestFixture]
    public class when_a_CIP_is_assigned : 
        SimpleCommandFixture<AssignCIPCommand, Course, CourseCIPAssignedEvent>
    {

        private const string CIP = "123456";


        protected override IEnumerable<ISourcedEvent> Given()
        {
            yield return new CourseCreatedEvent()
                             {
                                 Subject = "BIOL",
                                 Number = "2302",
                                 Title = "Anatomy & Physiology II"
                             };
        }

        protected override AssignCIPCommand WhenExecutingCommand()
        {
            return new AssignCIPCommand()
                       {
                           CourseId = EventSourceId,
                           CIP = CIP
                       };
        }

        [Test]
        public void then_it_should_create_a_new_CourseCIPAssignedEvent()
        {
            Assert.That(TheEvent.CIP, Is.EqualTo(CIP));
        }


    }
}
