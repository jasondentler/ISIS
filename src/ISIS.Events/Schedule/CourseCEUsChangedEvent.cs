using System;

namespace ISIS.Schedule
{
    public class CourseCEUsChangedEvent : IEvent 
    {

        public CourseCEUsChangedEvent(Guid courseId, decimal ceus)
        {
            CourseId = courseId;
            Ceus = ceus;
        }

        public Guid CourseId { get; private set; }
        public decimal Ceus { get; private set; }
    }
}