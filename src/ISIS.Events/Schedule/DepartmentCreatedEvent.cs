using System;

namespace ISIS.Schedule
{
    public class DepartmentCreatedEvent 
    {

        public Guid DepartmentId { get; set; }
        public string Name { get; set; }
        
    }
}
