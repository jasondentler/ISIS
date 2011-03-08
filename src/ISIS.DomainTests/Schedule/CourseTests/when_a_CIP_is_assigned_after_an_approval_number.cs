using System.Collections.Generic;
using System.Linq;
using Ncqrs.Spec;
using NUnit.Framework;

namespace ISIS.Schedule.CourseTests
{
    [Specification]
    public class when_a_CIP_is_assigned_after_an_approval_number : 
        DomainFixture<ChangeCIPCommand>
    {

        private const string ApprovalNumber = "1234567890";
        private const string CIP = "235465";


        protected override IEnumerable<object> GivenEvents()
        {
            yield return new CreditCourseCreatedEvent(EventSourceId, "BIOL", "2302");
            yield return new CourseApprovalNumberChangedEvent(EventSourceId, ApprovalNumber);
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
        public void it_should_not_throw()
        {
            Assert.That(CaughtException, Is.Null);
        }

        [Then]
        public void it_should_so_no_more()
        {
            Assert.That(PublishedEvents.Count(), Is.EqualTo(2));
        }

        [Then]
        public void it_should_change_the_cip()
        {
            var cipEvent = PublishedEvents.Select(pe => pe.Payload)
                .OfType<CourseCIPChangedEvent>().SingleOrDefault();
            Assert.That(cipEvent.CourseId, Is.EqualTo(EventSourceId));
            Assert.That(cipEvent.CIP, Is.EqualTo(CIP));
        }

        [Then]
        public void it_should_remove_the_approval_number()
        {
            var approvalNumberEvent = PublishedEvents.Select(pe => pe.Payload)
                .OfType<CourseApprovalNumberChangedEvent>().SingleOrDefault();
            Assert.That(approvalNumberEvent.CourseId, Is.EqualTo(EventSourceId));
            Assert.That(approvalNumberEvent.ApprovalNumber, Is.EqualTo(null));
        }
        
    }
}
