using System.Collections.Generic;
using NUnit.Framework;

namespace ISIS.DomainTests.CourseTests
{
    [TestFixture]
    public class when_an_approval_number_is_assigned_after_a_CIP : 
        CommandFixture<AssignApprovalNumberCommand, Course>
    {

        private const string ApprovalNumber = "123456";


        protected override IEnumerable<object> Given()
        {
            yield return new CourseCreatedEvent(EventSourceId, "BIOL", "2302", "Anatomy & Physiology II");
            yield return new CourseCIPAssignedEvent(EventSourceId, "123456");
        }

        protected override AssignApprovalNumberCommand WhenExecutingCommand()
        {
            return new AssignApprovalNumberCommand()
                       {
                           CourseId = EventSourceId,
                           ApprovalNumber = ApprovalNumber
                       };
        }

        [Test]
        public void then_it_should_throw_InvalidStateException()
        {
            Assert.That(CaughtException, Is.InstanceOf<InvalidStateException>());
        }


    }
}
