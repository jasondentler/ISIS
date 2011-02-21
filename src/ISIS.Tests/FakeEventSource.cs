using System;
using System.Collections.Generic;
using Ncqrs.Eventing;
using Ncqrs.Eventing.Sourcing;

namespace ISIS
{
    public class FakeEventSource : IEventSource
    {

        private readonly Queue<object> _events;

        public FakeEventSource(Guid eventSourceId, IEnumerable<object> history)
        {
            EventSourceId = eventSourceId;
            _events = new Queue<object>();
            foreach ( var e in history)
            {
                _events.Enqueue(e);
                Version++;
            }
        }

        public void InitializeFromHistory(CommittedEventStream history)
        {
            throw new NotImplementedException();
        }

        public void AcceptChanges()
        {
            foreach (var e in _events)
            {
                InitialVersion++;
                EventApplied(this, new EventAppliedEventArgs(
                                       new UncommittedEvent(
                                           Guid.NewGuid(), EventSourceId,
                                           InitialVersion, 0, DateTime.Now,
                                           e, new Version(0, 0, 0, 0))));
            }
            InitialVersion = Version;
            _events.Clear();
        }

        public Guid EventSourceId { get; private set; }
        public long Version { get; private set; }
        public long InitialVersion { get; private set; }
        public event EventHandler<EventAppliedEventArgs> EventApplied;
    }
}
