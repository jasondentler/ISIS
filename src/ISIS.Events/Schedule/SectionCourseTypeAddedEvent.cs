using System;
using System.Collections.Generic;

namespace ISIS.Schedule
{
    public class SectionCourseTypeAddedEvent : IEvent 
    {

        public Guid SectionId { get; private set; }
        public CourseTypes CourseTypeAdded { get; private set; }
        public IEnumerable<CourseTypes> CurrentCourseTypes { get; private set; }

        public SectionCourseTypeAddedEvent(
            Guid sectionId,
            CourseTypes courseTypeAdded,
            IEnumerable<CourseTypes> currentCourseTypes)
        {
            SectionId = sectionId;
            CourseTypeAdded = courseTypeAdded;
            CurrentCourseTypes = currentCourseTypes;
        }
    }
}
