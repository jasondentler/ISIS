using System.Collections.Generic;
using Ncqrs.Spec;
using NUnit.Framework;

namespace ISIS.Schedule.CourseTests
{
    [Specification]
    public class when_the_same_approval_number_is_assigned : 
        DomainFixture<ChangeApprovalNumberCommand>
    {

        private const string ApprovalNumber = "1234567890";


        protected override IEnumerable<object> GivenEvents()
        {
            yield return new CreditCourseCreatedEvent(EventSourceId, "BIOL", "2302");
            yield return new CourseApprovalNumberChangedEvent(EventSourceId, ApprovalNumber);
        }

        protected override ChangeApprovalNumberCommand WhenExecuting()
        {
            return new ChangeApprovalNumberCommand()
            {
                CourseId = EventSourceId,
                ApprovalNumber = ApprovalNumber
            };
        }

        [Then]
        public void then_it_should_do_nothing()
        {
            Assert.That(PublishedEvents, Is.Empty);
        }

        [Then]
        public void then_it_should_not_throw()
        {
            Assert.That(CaughtException, Is.Null);
        }
        
    }
}
