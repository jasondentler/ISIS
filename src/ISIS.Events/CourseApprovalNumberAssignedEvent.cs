using System;

namespace ISIS
{
    public class CourseApprovalNumberAssignedEvent
    {
        public Guid CourseId { get; set; }
        public string ApprovalNumber { get; set; }
    }
}