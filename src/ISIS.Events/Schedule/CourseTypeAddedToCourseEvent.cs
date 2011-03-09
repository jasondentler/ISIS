using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ISIS.Schedule
{
    [Serializable]
    public class CourseTypeAddedToCourseEvent
    {
        public Guid CourseId { get; private set; }

        public CourseTypes TypeAdded { get; set; }

        public IEnumerable<CourseTypes> CurrentTypes { get; private set; }

        protected CourseTypeAddedToCourseEvent()
        {
        }

        public CourseTypeAddedToCourseEvent(Guid courseId, CourseTypes typeAdded, IEnumerable<CourseTypes> currentTypes)
        {
            CourseId = courseId;
            TypeAdded = typeAdded;
            CurrentTypes = currentTypes;
        }
    }
}