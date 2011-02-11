using Ncqrs.Eventing.Sourcing;

namespace ISIS
{
    public class DepartmentCreatedEvent : SourcedEvent
    {

        public string Name { get; set; }
        
    }
}
