using System;
using Ncqrs.Domain;

namespace ISIS
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

        protected void OnDepartmentCreated(DepartmentCreatedEvent e)
        {
        }

        public void ChangeDepartmentDefaultSubject(string subject)
        {
        throw new NotImplementedException();
        }

    }

}
