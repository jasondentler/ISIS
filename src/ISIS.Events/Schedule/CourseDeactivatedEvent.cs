using System;

namespace ISIS.Schedule
{
    public class CourseDeactivatedEvent 
    {

        public Guid CourseId { get; private set; }

        private CourseDeactivatedEvent()
        {
        }

        public CourseDeactivatedEvent(Guid courseId)
        {
            CourseId = courseId;
        }
    }
}
