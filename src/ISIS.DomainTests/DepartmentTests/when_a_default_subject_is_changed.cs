using System.Collections.Generic;
using System.Linq;
using Ncqrs.Eventing.Sourcing;
using NUnit.Framework;

namespace ISIS.DomainTests.DepartmentTests
{
    [TestFixture]
    public class when_a_default_subject_is_changed :
        SimpleCommandFixture<ChangeDefaultSubjectCommand, Department, DepartmentCreatedEvent>
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


    }
}
