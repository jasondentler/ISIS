using System.Collections.Generic;
using System.Linq;
using Ncqrs.Spec;
using NUnit.Framework;

namespace ISIS.Schedule.CourseTests
{
    [Specification]
    public class when_an_approval_number_is_assigned : 
        DomainFixture<ChangeApprovalNumberCommand>
    {

        private const string ApprovalNumber = "1234567890";


        protected override IEnumerable<object> GivenEvents()
        {
            yield return new CreditCourseCreatedEvent(EventSourceId, "BIOL", "2302");
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
        public void then_it_should_do_nothing_more()
        {
            Assert.That(PublishedEvents.Count(), Is.EqualTo(2));
        }

        [Then]
        public void then_it_should_not_throw()
        {
            Assert.That(CaughtException, Is.Null);
        }

        [Then]
        public void then_it_should_publish_a_new_CourseCIPAssignedEvent()
        {
            var eventStream = PublishedEvents.Select(pe => pe.Payload);
            var TheEvent = eventStream.OfType<CourseCIPChangedEvent>().Single();
            Assert.That(TheEvent.CourseId, Is.EqualTo(EventSourceId));
            Assert.That(TheEvent.CIP, Is.EqualTo(ApprovalNumber.Substring(0, 6)));
        }

        [Then]
        public void then_it_should_create_a_new_CourseApprovalNumberAssignedEvent()
        {
            var eventStream = PublishedEvents.Select(pe => pe.Payload);
            var TheEvent = eventStream.OfType<CourseApprovalNumberChangedEvent>().Single();
            Assert.That(TheEvent.CourseId, Is.EqualTo(EventSourceId));
            Assert.That(TheEvent.ApprovalNumber, Is.EqualTo(ApprovalNumber));
        }


    }
}
