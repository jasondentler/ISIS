using System;
using Ncqrs.Commanding;

namespace ISIS.Schedule
{
    public class ChangeCourseCreditTypeCommand : CommandBase 
    {

        public Guid CourseId { get; set; }
        public CreditTypes Type { get; set; }

    }
}
