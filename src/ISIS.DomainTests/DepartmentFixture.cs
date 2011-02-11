using System.Linq;
using Ncqrs.Spec;
using NUnit.Framework;

namespace ISIS.DomainTests
{
    public abstract class DepartmentFixture : AggregateRootTestFixture<Department>
    {
        protected string Name = "Biology";
        protected int ExpectedEventCount;
        protected DepartmentCreatedEvent DepartmentCreatedEvent;

        [SetUp]
        public void SetUp()
        {
            DepartmentCreatedEvent = new DepartmentCreatedEvent()
                                         {
                                             Name = Name
                                         };
        }

        protected override void When()
        {
            throw new System.NotImplementedException();
        }

        [Test]
        public void it_should_have_only_the_expected_events()
        {
            Assert.That(PublishedEvents.Count(), Is.EqualTo(ExpectedEventCount));
        }

    }
}
