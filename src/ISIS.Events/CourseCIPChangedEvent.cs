using System;

namespace ISIS
{
    public class CourseCIPChangedEvent 
    {

        public CourseCIPChangedEvent(Guid courseId, string cip)
        {
            CourseId = courseId;
            CIP = cip;
        }

        public Guid CourseId { get; set; }
        public string CIP { get; set; }
    }
}