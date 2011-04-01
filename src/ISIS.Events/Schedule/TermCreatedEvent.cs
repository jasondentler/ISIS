using System;

namespace ISIS.Schedule
{
    public class TermCreatedEvent : IEvent 
    {

        public Guid TermId { get; private set; }
        public string Name { get; private set; }
        public string Abbreviation { get; private set; }
        public DateTime Start { get; private set; }
        public DateTime End { get; private set; }

        public TermCreatedEvent(
            Guid termId,
            string name,
            string abbreviation,
            DateTime start,
            DateTime end)
        {
            TermId = termId;
            Name = name;
            Abbreviation = abbreviation;
            Start = start;
            End = end;
        }
    }
}