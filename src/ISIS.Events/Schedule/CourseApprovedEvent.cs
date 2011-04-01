using System;

namespace ISIS.Schedule
{
    public class CourseApprovedEvent : IEvent 
    {
        public Guid CourseId { get; private set; }
        public string ApprovedBy { get; private set; }

        public CourseApprovedEvent(
            Guid courseId,
            string approvedBy)
        {
            CourseId = courseId;
            ApprovedBy = approvedBy;
        }
    }
}
