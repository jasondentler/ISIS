using System;
using Ncqrs.Commanding;

namespace ISIS.Schedule
{
    public class MakeCoursePendingCommand : CommandBase 
    {
        public Guid CourseId { get; set; }
    }
}
