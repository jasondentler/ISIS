using System;

namespace ISIS.Schedule
{
    public class CourseActivatedEvent : IEvent 
    {

        public Guid CourseId { get; private set; }

        public DateTime EffectiveDate { get; private set; }

        public CourseActivatedEvent(Guid courseId, DateTime effectiveDate)
        {
            CourseId = courseId;
            EffectiveDate = effectiveDate;
        }
    }
}
