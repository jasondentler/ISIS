using System;

namespace ISIS.Schedule
{
    public class SectionCreditTypeChangedEvent : IEvent 
    {
        public Guid SectionId { get; private set; }
        public CreditTypes CreditType { get; private set; }

        public SectionCreditTypeChangedEvent(Guid sectionId, CreditTypes creditType)
        {
            SectionId = sectionId;
            CreditType = creditType;
        }
    }
}
