using System;
using Ncqrs.Commanding;

namespace ISIS.Schedule
{
    public class ChangeCourseTopicCodeCommand : CommandBase 
    {

        public Guid CourseId { get; set; }
        public Guid TopicCodeId { get; set; }

    }
}