using Ncqrs.Eventing.Sourcing;

namespace ISIS
{
    public class DepartmentDefaultSubjectChangedEvent : SourcedEvent 
    {

        public string NewDefaultSubject { get; set; }

    }
}
