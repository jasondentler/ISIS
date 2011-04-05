using System;

namespace ISIS.Schedule
{
    public class SectionApprovalNumberChangedEvent : IEvent 
    {

        public Guid SectionId { get; private set; }
        public string ApprovalNumber { get; private set; }

        public SectionApprovalNumberChangedEvent(
            Guid sectionId,
            string approvalNumber)
        {
            SectionId = sectionId;
            ApprovalNumber = approvalNumber;
        }

    }
}