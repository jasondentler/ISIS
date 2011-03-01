using System.Collections.Generic;
using System.Linq;
using Ncqrs.Spec;
using NUnit.Framework;

namespace ISIS.DomainTests.CourseTests
{
    [TestFixture]
    public class when_an_approval_number_is_assigned_after_a_CIP : 
        DomainFixture<ChangeApprovalNumberCommand>
    {

        private const string ApprovalNumber = "1234567890";


        protected override IEnumerable<object> GivenEvents()
        {
            yield return new CourseCreatedEvent(EventSourceId, "BIOL", "2302");
            yield return new CourseCIPChangedEvent(EventSourceId, "123456");
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
        public void it_should_change_the_CIP()
        {
            var cipEvent = PublishedEvents.Select(pe => pe.Payload)
                .OfType<CourseCIPChangedEvent>().SingleOrDefault();
            Assert.That(cipEvent.CourseId, Is.EqualTo(EventSourceId));
            Assert.That(cipEvent.CIP, Is.EqualTo(ApprovalNumber.Substring(0, 6)));
        }

        [Then]
        public void it_should_change_the_approval_number()
        {
            var approvalEvent = PublishedEvents.Select(pe => pe.Payload)
                .OfType<CourseApprovalNumberChangedEvent>().SingleOrDefault();
            Assert.That(approvalEvent.CourseId, Is.EqualTo(EventSourceId));
            Assert.That(approvalEvent.ApprovalNumber, Is.EqualTo(ApprovalNumber));
        }

        [Then]
        public void it_should_not_throw()
        {
            Assert.That(CaughtException, Is.Null);
        }

        [Then]
        public void it_should_do_no_more()
        {
            Assert.That(PublishedEvents.Count(), Is.EqualTo(2));
        }

    }
}
