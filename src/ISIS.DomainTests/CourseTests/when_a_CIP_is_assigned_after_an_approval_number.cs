using System.Collections.Generic;
using NUnit.Framework;

namespace ISIS.DomainTests.CourseTests
{
    [TestFixture]
    public class when_a_CIP_is_assigned_after_an_approval_number : 
        CommandFixture<AssignCIPCommand, Course>
    {

        private const string CIP = "123456";


        protected override IEnumerable<object> Given()
        {
            yield return new CourseCreatedEvent(EventSourceId, "BIOL", "2302");
            yield return new CourseApprovalNumberAssignedEvent(EventSourceId, "1234567890");
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
        public void then_it_should_throw_InvalidStateException()
        {
            Assert.That(CaughtException, Is.InstanceOf<InvalidStateException>());
        }


    }
}
