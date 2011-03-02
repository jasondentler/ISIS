using System;
using Ncqrs.Commanding;

namespace ISIS.Schedule
{
    public class DeactivateCourseCommand : CommandBase
    {

        public Guid CourseId { get; set; }

    }
}
