using System;
using Ncqrs.Domain;

namespace ISIS.Schedule
{
    public class Term : AggregateRootMappedByConvention 
    {
        private string _abbreviation;
        private string _name;
        private DateTime _start;
        private DateTime _end;

        [Inject]
        private Term()
        {
        }

        public Term(
            Guid termId, 
            string abbreviation, 
            string name, 
            DateTime startDate, 
            DateTime endDate)
        {
            ApplyEvent(new TermCreatedEvent(
                           termId,
                           abbreviation,
                           name,
                           startDate,
                           endDate));
        }

        protected void OnTermCreated(TermCreatedEvent @event)
        {
            _abbreviation = @event.Abbreviation;
            _name = @event.Name;
            _start = @event.Start;
            _end = @event.End;
        }

        public TermMemento BuildMememto()
        {
            return new TermMemento(
                EventSourceId,
                _abbreviation,
                _name,
                _start,
                _end);
        }
    }
}
