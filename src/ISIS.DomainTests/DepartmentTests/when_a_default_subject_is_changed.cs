using System.Collections.Generic;
using System.Linq;
using Ncqrs.Eventing.Sourcing;
using NUnit.Framework;

namespace ISIS.DomainTests.DepartmentTests
{
    [TestFixture]
    public class when_a_default_subject_is_changed :
        SimpleCommandFixture<ChangeDefaultSubjectCommand, Department, DepartmentDefaultSubjectChangedEvent>
    {

        protected override IEnumerable<ISourcedEvent> Given()
        {
            return new[] { new DepartmentCreatedEvent { Name = "Biology" } };
        }

        protected override ChangeDefaultSubjectCommand WhenExecutingCommand()
        {
            return new ChangeDefaultSubjectCommand()
                       {
                           DefaultSubject = "BIOL",
                           DepartmentId = EventSourceId
                       };
        }

        [Test]
        public void it_has_correct_new_default_department()
        {
            Assert.That(TheEvent.NewDefaultSubject, Is.EqualTo(ExecutedCommand.DefaultSubject));
        }


    }
}
