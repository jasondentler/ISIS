using System;

namespace ISIS.Schedule
{
    public class TermCreatedEvent 
    {
        public Guid TermId { get; set; }
        public string Name { get; set; }

    }
}