using Ncqrs.Domain;

namespace ISIS
{
    public class Term : AggregateRootMappedByConvention
    {

        private Term()
        {
        }

        public Term(string name)
        {
            var e = new TermCreatedEvent()
                        {
                            Name = name
                        };
            ApplyEvent(e);
        }

        protected void OnTermCreated(TermCreatedEvent @event)
        {
        }

    }
}