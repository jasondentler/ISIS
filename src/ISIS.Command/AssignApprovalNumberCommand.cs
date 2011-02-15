using System;
using Ncqrs.Commanding;

namespace ISIS
{
    public class AssignApprovalNumberCommand : CommandBase 
    {
        public Guid CourseId { get; set; }
        public string ApprovalNumber { get; set; }
    }
}