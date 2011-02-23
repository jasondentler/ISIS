using System;

namespace ISIS
{
    public class CourseApprovalNumberAssignedEvent
    {

        private CourseApprovalNumberAssignedEvent()
        {
            
        }

        public CourseApprovalNumberAssignedEvent(Guid courseId, string approvalNumber)
        {
            CourseId = courseId;
            ApprovalNumber = approvalNumber;
        }

        public Guid CourseId { get; set; }
        public string ApprovalNumber { get; set; }
    }
}