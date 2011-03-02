using System;

namespace ISIS.Schedule
{
    public class CourseActivatedEvent 
    {

        public Guid CourseId { get; private set; }

        private CourseActivatedEvent()
        {
        }

        public CourseActivatedEvent(Guid courseId)
        {
            CourseId = courseId;
        }
    }
}
