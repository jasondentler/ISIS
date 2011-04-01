using Ncqrs.Domain;

namespace ISIS.Schedule
{
    public class Term : AggregateRoot 
    {

        [Inject]
        private Term()
        {
        }

        protected void OnTermCreated(TermCreatedEvent @event)
        {
        }


    }
}
