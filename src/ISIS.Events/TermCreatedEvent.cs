using System;

namespace ISIS
{
    public class TermCreatedEvent 
    {
        public Guid TermId { get; set; }
        public string Name { get; set; }

    }
}