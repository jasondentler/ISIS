using System.Collections.Generic;
using Ncqrs.Spec;
using NUnit.Framework;

namespace ISIS.Schedule.DepartmentTests
{
    [Specification]
    public class when_a_new_department_is_created : 
        OneEventTestFixture<CreateDepartmentCommand, DepartmentCreatedEvent>
    {

        private const string DepartmentName = "Biology";

        protected override IEnumerable<object> GivenEvents()
        {
            return new object[0];
        }

        protected override CreateDepartmentCommand WhenExecuting()
        {
            return new CreateDepartmentCommand()
            {
                Name = DepartmentName
            };
        }

        [Then]
        public void then_it_should_create_a_new_DepartmentCreatedEvent()
        {
            Assert.That(TheEvent.Name, Is.EqualTo(DepartmentName));
        }


    }
}
