using Ncqrs.Eventing.Sourcing;

namespace ISIS
{
    public class TermCreatedEvent : SourcedEvent
    {

        public string Name { get; set; }

    }
}