using System;

namespace ISIS.Schedule
{
    public class DepartmentCreatedEvent : IEvent 
    {

        public Guid DepartmentId { get; set; }
        public string Name { get; set; }
        
    }
}
