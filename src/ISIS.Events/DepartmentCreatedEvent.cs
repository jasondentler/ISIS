using System;

namespace ISIS
{
    public class DepartmentCreatedEvent 
    {

        public Guid DepartmentId { get; set; }
        public string Name { get; set; }
        
    }
}
