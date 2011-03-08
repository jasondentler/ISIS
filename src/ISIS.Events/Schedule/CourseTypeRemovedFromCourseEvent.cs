using System;
using System.Collections.Generic;

namespace ISIS.Schedule
{
    public class CourseTypeRemovedFromCourseEvent
    {
        public Guid CourseId { get; private set; }
        public CourseTypes TypeRemoved { get; private set; }
        public IEnumerable<CourseTypes> CurrentTypes { get; private set; }

        private CourseTypeRemovedFromCourseEvent()
        {
        }

        public CourseTypeRemovedFromCourseEvent(Guid courseId, CourseTypes type, IEnumerable<CourseTypes> currentTypes)
        {
            CourseId = courseId;
            TypeRemoved = type;
            CurrentTypes = currentTypes;
        }
    }
}