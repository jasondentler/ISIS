using System;
using Ncqrs.Commanding;

namespace ISIS.Schedule
{
    public class ChangeCourseCreditType : CommandBase 
    {

        public Guid CourseId { get; set; }
        public CreditTypes Type { get; set; }

    }
}
