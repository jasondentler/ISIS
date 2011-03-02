using System;
using Ncqrs.Commanding;

namespace ISIS
{

    public class MakeCourseObsoleteCommand : CommandBase
    {

        public Guid CourseId { get; set; }

    }

}
