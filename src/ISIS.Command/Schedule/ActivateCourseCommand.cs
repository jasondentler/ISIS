using System;
using Ncqrs.Commanding;

namespace ISIS.Schedule
{

    public class ActivateCourseCommand : CommandBase
    {

        public Guid CourseId { get; set; }

    }

}
