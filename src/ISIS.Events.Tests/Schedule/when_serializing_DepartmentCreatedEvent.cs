using System;

namespace ISIS.Schedule
{

    public class when_serializing_DepartmentCreatedEvent
        : JsonEventSerializationFixture<DepartmentCreatedEvent>
    {

        protected override DepartmentCreatedEvent GivenEvent()
        {
            return new DepartmentCreatedEvent()
                       {
                           DepartmentId = Guid.NewGuid(),
                           Name = "Biology"
                       };
        }

    }
}
