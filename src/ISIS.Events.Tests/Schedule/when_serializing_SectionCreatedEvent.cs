using System;

namespace ISIS.Schedule
{

    public class when_serializing_SectionCreatedEvent
        : JsonEventSerializationFixture<SectionCreatedEvent>
    {

        protected override SectionCreatedEvent GivenEvent()
        {
            return new SectionCreatedEvent()
                       {
                           CourseId = Guid.NewGuid(),
                           SectionId = Guid.NewGuid(),
                           SectionNumber = "01",
                           TermId = Guid.NewGuid()
                       };
        }

    }
}
