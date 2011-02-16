using System;
using System.Collections.Generic;
using Ncqrs.Eventing.Sourcing;

namespace ISIS
{
    public class FakeEventSource : IEventSource
    {

        private readonly Queue<ISourcedEvent> _events;

        public FakeEventSource(Guid eventSourceId, IEnumerable<ISourcedEvent> history)
        {
            EventSourceId = eventSourceId;
            _events = new Queue<ISourcedEvent>();
            foreach ( var e in history)
            {
                e.ClaimEvent(eventSourceId, ++Version);
                _events.Enqueue(e);
            }
        }

        public void InitializeFromHistory(IEnumerable<ISourcedEvent> history)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ISourcedEvent> GetUncommittedEvents()
        {
            return _events.ToArray();
        }

        public void AcceptChanges()
        {
            InitialVersion = Version;
            _events.Clear();
        }

        public Guid EventSourceId { get; private set; }
        public long Version { get; private set; }
        public long InitialVersion { get; private set; }

    }
}
