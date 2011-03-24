using System;
using Ncqrs.Commanding;

namespace ISIS.Schedule
{
    public class ChangeCourseCEUsCommand : CommandBase 
    {

        public Guid CourseId { get; set; }
        public decimal CEUs { get; set; }

    }
}
