using System.Linq;
using NUnit.Framework;

namespace ISIS.DomainTests.DepartmentTests
{
    [TestFixture]
    public class when_a_new_department_is_created : 
        SimpleCommandFixture<CreateDepartmentCommand, Department, DepartmentCreatedEvent>
    {

        private const string DepartmentName = "Biology";
        
        protected override CreateDepartmentCommand WhenExecutingCommand()
        {
            return new CreateDepartmentCommand()
                       {
                           Name = DepartmentName
                       };
        }

        [Test]
        public void then_it_should_create_a_new_DepartmentCreatedEvent()
        {
            var firstEvent = (DepartmentCreatedEvent) PublishedEvents.First();
            Assert.That(firstEvent.Name, Is.EqualTo(DepartmentName));
        }


    }
}
