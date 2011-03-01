using System;

namespace ISIS
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
