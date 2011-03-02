using System;

namespace ISIS
{
    public class CourseMadePendingEvent 
    {

        public Guid CourseId { get; private set; }

        private CourseMadePendingEvent()
        {
        }

        public CourseMadePendingEvent(Guid courseId)
        {
            CourseId = courseId;
        }
    }
}
