using System;

namespace ISIS.Schedule
{
    public class CourseMadeObsoleteEvent : IEvent 
    {

        public Guid CourseId { get; private set; }

        public CourseMadeObsoleteEvent(Guid courseId)
        {
            CourseId = courseId;
        }
    }
}
