using System;

namespace ISIS
{
    public class CourseCIPAssignedEvent 
    {

        public CourseCIPAssignedEvent(Guid courseId, string cip)
        {
            CourseId = courseId;
            CIP = cip;
        }

        public Guid CourseId { get; set; }
        public string CIP { get; set; }
    }
}