using System;
using Ncqrs.Domain;

namespace ISIS.Schedule
{

    public class TopicCode : AggregateRootMappedByConvention
    {
        private string _abbreviation;
        private string _description;

        private TopicCode()
        {
        }

        public TopicCode(
            string abbreviation, 
            string description)
        {
            ApplyEvent(new TopicCodeCreatedEvent(EventSourceId, abbreviation, description));
        }

        protected void OnTopicCodeCreated(TopicCodeCreatedEvent @event)
        {
            _abbreviation = @event.Abbreviation;
            _description = @event.Description;
        }

        public void ChangeAbbreviation(string abbreviation)
        {
            ApplyEvent(new TopicCodeAbbreviationChangedEvent(
                           EventSourceId,
                           abbreviation,
                           _abbreviation));
        }

        protected void OnAbbreviationChanged(TopicCodeAbbreviationChangedEvent @event)
        {
            _abbreviation = @event.Abbreviation;
        }

        public void ChangeDescription(string description)
        {
            ApplyEvent(new TopicCodeDescriptionChangedEvent(
                EventSourceId,
                description,
                _description));
        }

        protected void OnDescriptionChanged(TopicCodeDescriptionChangedEvent @event)
        {
            _description = @event.Description;
        }

        public TopicCodeMemento BuildMemento()
        {
            return new TopicCodeMemento(EventSourceId, _abbreviation, _description);
        }

    }

}
