using Ncqrs.Domain;

namespace ISIS.Schedule
{
 
    public class Department : AggregateRootMappedByConvention 
    {
        private Department()
        {
        }

        public Department(string name)
        {
            var e = new DepartmentCreatedEvent()
                        {
                            Name = name
                        };
            ApplyEvent(e);
        }

        protected void OnCreated(DepartmentCreatedEvent e)
        {
        }


    }

}
