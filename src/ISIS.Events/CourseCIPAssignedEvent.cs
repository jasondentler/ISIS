using Ncqrs.Eventing.Sourcing;

namespace ISIS
{
    public class CourseCIPAssignedEvent : SourcedEvent
    {
        public string CIP { get; set; }
    }
}