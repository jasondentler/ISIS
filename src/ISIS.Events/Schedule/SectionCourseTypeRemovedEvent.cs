using System;
using System.Collections.Generic;

namespace ISIS.Schedule
{
    public class SectionCourseTypeRemovedEvent : IEvent 
    {

        public Guid SectionId { get; private set; }
        public CourseTypes CourseTypeRemoved { get; private set; }
        public IEnumerable<CourseTypes> CurrentCourseTypes { get; private set; }

        public SectionCourseTypeRemovedEvent(
            Guid sectionId,
            CourseTypes courseTypeRemoved,
            IEnumerable<CourseTypes> currentCourseTypes)
        {
            SectionId = sectionId;
            CourseTypeRemoved = courseTypeRemoved;
            CurrentCourseTypes = currentCourseTypes;
        }
    }
}
