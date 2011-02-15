using System;
using Ncqrs.Eventing.Sourcing;

namespace ISIS
{
    public class SectionCreatedEvent : SourcedEvent
    {

        public Guid CourseId { get; set; }
        public Guid TermId { get; set; }
        public string SectionNumber { get; set; }

    }
}