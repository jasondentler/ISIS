using System.Collections.Generic;
using System.Linq;
using Ncqrs.Eventing.Sourcing;
using NUnit.Framework;

namespace ISIS.DomainTests.CourseTests
{
    [TestFixture]
    public class when_an_approval_number_is_assigned : 
        CommandFixture<AssignApprovalNumberCommand, Course>
    {

        private const string ApprovalNumber = "1234567890";


        protected override IEnumerable<ISourcedEvent> Given()
        {
            yield return new CourseCreatedEvent()
                             {
                                 Rubric = "BIOL",
                                 Number = "2302",
                                 Title = "Anatomy & Physiology II"
                             };
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
        public void then_it_should_do_nothing_more()
        {
            Assert.That(PublishedEvents.Count(), Is.EqualTo(2));
        }

        [Test]
        public void then_it_should_not_throw()
        {
            Assert.That(CaughtException, Is.Null);
        }

        [Test]
        public void then_it_should_publish_a_new_CourseCIPAssignedEvent()
        {
            var TheEvent = PublishedEvents.OfType<CourseCIPAssignedEvent>().Single();
            Assert.That(TheEvent.CIP, Is.EqualTo(ApprovalNumber.Substring(0, 6)));
        }

        [Test]
        public void then_it_should_create_a_new_CourseApprovalNumberAssignedEvent()
        {
            var TheEvent = PublishedEvents.OfType<CourseApprovalNumberAssignedEvent>().Single();
            Assert.That(TheEvent.ApprovalNumber, Is.EqualTo(ApprovalNumber));
        }


    }
}
