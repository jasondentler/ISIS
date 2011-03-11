using System;

namespace ISIS.Schedule
{
    public class CourseDeactivatedEvent : IEvent 
    {

        public Guid CourseId { get; private set; }

        public CourseDeactivatedEvent(Guid courseId)
        {
            CourseId = courseId;
        }
    }
}
