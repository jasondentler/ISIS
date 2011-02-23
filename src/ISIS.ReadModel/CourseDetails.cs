using System;

namespace ISIS
{
    public class CourseDetails : IEntity
    {

        [Id]
        public Guid CourseId { get; set; }
        public string Rubric { get; set; }
        public string Number { get; set; }
        public string Title { get; set; }
        public string CIP { get; set; }
        public string ApprovalNumber { get; set; }
        
    }
}
