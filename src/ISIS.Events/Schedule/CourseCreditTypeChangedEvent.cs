using System;

namespace ISIS.Schedule
{

    public class CourseCreditTypeChangedEvent : IEvent
    {

        public Guid CourseId { get; private set; }
        public CreditTypes CreditType { get; private set; }

        public CourseCreditTypeChangedEvent(Guid courseId, CreditTypes creditType)
        {
            CourseId = courseId;
            CreditType = creditType;
        }
    }

}
