using System.Linq;
using NUnit.Framework;

namespace ISIS.DomainTests.DepartmentTests
{
    [TestFixture]
    public class when_a_new_department_is_created : CommandFixture<CreateDepartmentCommand, Department>
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

        [Test]
        public void it_should_do_nothing_more()
        {
            Assert.That(PublishedEvents.Count(), Is.EqualTo(1));
        }



    }
}
