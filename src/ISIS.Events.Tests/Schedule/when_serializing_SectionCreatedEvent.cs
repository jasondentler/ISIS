namespace ISIS.Schedule
{

    public class when_serializing_SectionCreatedEvent
        : JsonEventSerializationFixture<SectionCreatedEvent>
    {

        protected override SectionCreatedEvent GivenEvent()
        {
            return new SectionCreatedEvent();
        }

    }
}
