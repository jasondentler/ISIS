using Ncqrs.Eventing.Sourcing;

namespace ISIS
{
    public class CourseApprovalNumberAssignedEvent : SourcedEvent
    {
        public string ApprovalNumber { get; set; }
    }
}