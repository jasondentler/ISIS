using System.Collections.Generic;
using Ncqrs.Spec;
using NUnit.Framework;

namespace ISIS.Schedule.CourseTests
{
    [Specification]
    public class when_the_same_CIP_is_assigned_after_an_approval_number : 
        SimpleDomainFixture<ChangeCIPCommand, CourseApprovalNumberChangedEvent>
    {

        private const string ApprovalNumber = "1234567890";
        
        protected override IEnumerable<object> GivenEvents()
        {
            yield return new CreditCourseCreatedEvent(EventSourceId, "BIOL", "2302");
            yield return new CourseApprovalNumberChangedEvent(EventSourceId, ApprovalNumber);
            yield return new CourseCIPChangedEvent(EventSourceId, ApprovalNumber.Substring(0, 6));
        }

        protected override ChangeCIPCommand WhenExecuting()
        {
            return new ChangeCIPCommand()
                       {
                           CourseId = EventSourceId,
                           CIP = ApprovalNumber.Substring(0, 6)
                       };
        }


        [Then]
        public void it_should_remove_the_approval_number()
        {
            Assert.That(TheEvent.CourseId, Is.EqualTo(EventSourceId));
            Assert.That(TheEvent.ApprovalNumber, Is.EqualTo(null));
        }
        
    }
}
