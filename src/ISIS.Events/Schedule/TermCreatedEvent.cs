using System;

namespace ISIS.Schedule
{
    public class TermCreatedEvent : IEvent 
    {
        public Guid TermId { get; set; }
        public string Name { get; set; }

    }
}