using System;
using Ncqrs.Commanding;

namespace ISIS.Schedule
{
    public class AddCourseTypeToCourseCommand : CommandBase
    {

        public Guid CourseId { get; set; }
        public CourseTypes Type { get; set; }

    }
}
