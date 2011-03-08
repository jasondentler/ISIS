using System;

namespace ISIS.Schedule
{
    public class CourseTypeAddedToCourseEvent
    {
        public Guid CourseId { get; private set; }
        public CourseTypes Type { get; private set; }

        private CourseTypeAddedToCourseEvent()
        {
        }

        public CourseTypeAddedToCourseEvent(Guid courseId, CourseTypes type)
        {
            CourseId = courseId;
            Type = type;
        }
    }
}