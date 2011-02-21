using System;
using Ncqrs.Commanding;

namespace ISIS
{
    public class ChangeCourseTitleCommand : CommandBase 
    {
        public Guid CourseId { get; set; }
        public string NewTitle { get; set; }
    }
}