using System;

namespace ISIS
{
    public class CourseMadeObsoleteEvent 
    {

        public Guid CourseId { get; private set; }

        private CourseMadeObsoleteEvent()
        {
        }

        public CourseMadeObsoleteEvent(Guid courseId)
        {
            CourseId = courseId;
        }
    }
}
