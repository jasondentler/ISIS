using System.Collections.Generic;
using Ncqrs.Spec;
using NUnit.Framework;

namespace ISIS.Schedule.CourseTests
{
    [Specification]
    public class when_a_CIP_is_assigned : 
        SimpleDomainFixture<ChangeCIPCommand, CourseCIPChangedEvent>
    {

        private const string CIP = "123456";

        protected override IEnumerable<object> GivenEvents()
        {
            yield return new CourseCreatedEvent(EventSourceId, "BIOL", "2302");
        }

        protected override ChangeCIPCommand WhenExecuting()
        {
            return new ChangeCIPCommand()
            {
                CourseId = EventSourceId,
                CIP = CIP
            };
        }

        [Then]
        public void then_it_should_create_a_new_CourseCIPAssignedEvent()
        {
            Assert.That(TheEvent.CourseId, Is.EqualTo(EventSourceId));
            Assert.That(TheEvent.CIP, Is.EqualTo(CIP));
        }


    }
}
