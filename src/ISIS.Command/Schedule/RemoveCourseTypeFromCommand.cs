using System;
using Ncqrs.Commanding;

namespace ISIS.Schedule
{
    public class RemoveCourseTypeFromCourse : CommandBase
    {

        public Guid CourseId { get; set; }
        public CourseTypes Type { get; set; }

    }
}
