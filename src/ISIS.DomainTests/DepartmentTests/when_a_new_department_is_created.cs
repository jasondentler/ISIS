using System;
using System.Collections.Generic;
using Ncqrs.Spec;
using NUnit.Framework;

namespace ISIS.DomainTests.DepartmentTests
{
    [TestFixture]
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

        [Test]
        public void then_it_should_create_a_new_DepartmentCreatedEvent()
        {
            Assert.That(TheEvent.Name, Is.EqualTo(DepartmentName));
        }


    }
}
