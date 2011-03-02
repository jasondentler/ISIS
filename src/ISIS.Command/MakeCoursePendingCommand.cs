using System;
using Ncqrs.Commanding;

namespace ISIS
{
    public class MakeCoursePendingCommand : CommandBase 
    {
        public Guid CourseId { get; set; }
    }
}
