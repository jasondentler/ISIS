using System;
using Ncqrs.Eventing.ServiceModel.Bus;

namespace ISIS.Denormalizers.Tests
{
    public class PublishableEvent : IPublishableEvent 
    {

        public PublishableEvent(
            Guid eventIdentifier,
            DateTime eventTimestamp,
            Version eventVersion,
            Guid eventSourceId, 
            long eventSequence,
            Guid commitId,
            object payload)
        {
            EventIdentifier = eventIdentifier;
            EventTimestamp = eventTimestamp;
            EventVersion = eventVersion;
            EventSourceId = eventSourceId;
            EventSequence = eventSequence;
            CommitId = commitId;
            Payload = payload;
        }

        public Guid EventIdentifier { get; private set; }
        public DateTime EventTimestamp { get; set; }
        public DateTime EventTimeStamp { get; private set; }
        public Version EventVersion { get; private set; }
        public Guid EventSourceId { get; private set; }
        public long EventSequence { get; private set; }
        public Guid CommitId { get; private set; }
        public object Payload { get; private set; }
    }
}
