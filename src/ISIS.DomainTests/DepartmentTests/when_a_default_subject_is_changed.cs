using System.Collections.Generic;
using System.Linq;
using Ncqrs.Eventing.Sourcing;
using NUnit.Framework;

namespace ISIS.DomainTests.DepartmentTests
{
    [TestFixture]
    public class when_a_default_subject_is_changed : CommandFixture<ChangeDefaultSubjectCommand, Department>
    {

        protected override IEnumerable<ISourcedEvent> Given()
        {
            return new[] {new DepartmentCreatedEvent {Name = "Biology"}};
        }

        protected override ChangeDefaultSubjectCommand WhenExecutingCommand()
        {
            return new ChangeDefaultSubjectCommand()
                       {
                           DefaultSubject = "BIOL"
                       };
        }

        [Test]
        public void it_should_do_nothing_more()
        {
            Assert.That(PublishedEvents.Count(), Is.EqualTo(1));
        }

    }
}
