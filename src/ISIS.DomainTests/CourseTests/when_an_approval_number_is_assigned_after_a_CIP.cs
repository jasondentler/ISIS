using System.Collections.Generic;
using NUnit.Framework;

namespace ISIS.DomainTests.CourseTests
{
    [TestFixture]
    public class when_an_approval_number_is_assigned_after_a_CIP : 
        ExceptionTestFixture<AssignApprovalNumberCommand, InvalidStateException>
    {

        private const string ApprovalNumber = "1234567890";


        protected override IEnumerable<object> GivenEvents()
        {
            yield return new CourseCreatedEvent(EventSourceId, "BIOL", "2302");
            yield return new CourseCIPAssignedEvent(EventSourceId, "123456");
        }

        protected override AssignApprovalNumberCommand WhenExecuting()
        {
            return new AssignApprovalNumberCommand()
            {
                CourseId = EventSourceId,
                ApprovalNumber = ApprovalNumber
            };
        }
        
    }
}
