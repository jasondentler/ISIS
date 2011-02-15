using NUnit.Framework;

namespace ISIS.DomainTests.TermTests
{
    [TestFixture]
    public class when_a_new_term_is_created : 
        SimpleCommandFixture<CreateTermCommand, Term, TermCreatedEvent>
    {

        private const string TermName = "Spring 2010";

        protected override CreateTermCommand WhenExecutingCommand()
        {
            return new CreateTermCommand()
                       {
                           Name = TermName
                       };
        }

        [Test]
        public void then_it_should_create_a_new_TermCreatedEvent()
        {
            Assert.That(TheEvent.Name, Is.EqualTo(TermName));
        }


    }
}
