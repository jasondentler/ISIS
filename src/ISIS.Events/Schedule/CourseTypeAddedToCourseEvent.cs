using System;
using System.Collections.Generic;

namespace ISIS.Schedule
{
    [Serializable]
    public class CourseTypeAddedToCourseEvent : IEvent 
    {
        public Guid CourseId { get; private set; }

        public CourseTypes TypeAdded { get; set; }

        public IEnumerable<CourseTypes> CurrentTypes { get; private set; }
        
        public CourseTypeAddedToCourseEvent(Guid courseId, CourseTypes typeAdded, IEnumerable<CourseTypes> currentTypes)
        {
            CourseId = courseId;
            TypeAdded = typeAdded;
            CurrentTypes = currentTypes;
        }
    }
}