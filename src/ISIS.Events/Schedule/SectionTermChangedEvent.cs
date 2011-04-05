using System;

namespace ISIS.Schedule
{
    public class SectionTermChangedEvent : IEvent 
    {

        public Guid SectionId { get; private set; }
        public Guid TermId { get; private set; }
        public string TermAbbreviation { get; private set; }
        public string TermName { get; private set; }

        public SectionTermChangedEvent(
            Guid sectionId,
            Guid termId,
            string termAbbreviation,
            string termName)
        {
            SectionId = sectionId;
            TermId = termId;
            TermAbbreviation = termAbbreviation;
            TermName = termName;
        }

    }
}