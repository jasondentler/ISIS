using System;
using Ncqrs.Commanding;

namespace ISIS
{

    public class ActivateCourseCommand : CommandBase
    {

        public Guid CourseId { get; set; }

    }

}
