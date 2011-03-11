using System;

namespace ISIS.Schedule
{
    public class CourseMadePendingEvent : IEvent 
    {

        public Guid CourseId { get; private set; }
        
        public CourseMadePendingEvent(Guid courseId)
        {
            CourseId = courseId;
        }
    }
}
