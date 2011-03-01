using System;
using Ncqrs.Commanding;

namespace ISIS
{
    public class DeactivateCourseCommand : CommandBase
    {

        public Guid CourseId { get; set; }

    }
}
