using System;
using Ncqrs.Commanding;

namespace ISIS.Schedule
{

    public class MakeCourseObsoleteCommand : CommandBase
    {

        public Guid CourseId { get; set; }

    }

}
