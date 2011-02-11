using Ncqrs.Domain;

namespace ISIS
{
 
    public class Department : AggregateRootMappedByConvention 
    {

        public Department(string name)
        {
            var e = new DepartmentCreatedEvent()
                        {
                            Name = name
                        };
            ApplyEvent(e);
        }

        protected void OnDepartmentCreated(DepartmentCreatedEvent e)
        {
        }

    }

}
