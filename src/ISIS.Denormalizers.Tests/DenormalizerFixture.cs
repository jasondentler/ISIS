using System;
using System.Collections.Generic;
using Ncqrs;
using Ncqrs.Eventing.ServiceModel.Bus;
using Ncqrs.Eventing.Sourcing;
using NUnit.Framework;

namespace ISIS.Denormalizers.Tests
{
    public abstract class DenormalizerFixture<TDenormalizer, TEvent>
        : BaseFixture
        where TDenormalizer : Denormalizer, IEventHandler<TEvent>
        where TEvent : ISourcedEvent
    {

        protected abstract TDenormalizer CreateDenormalizer(IRepositoryFactory factory);
        protected abstract IEnumerable<ISourcedEvent> Given();
        protected abstract TEvent WhenHandling();

        protected Guid EventSourceId { get; private set; }
        protected Exception CaughException { get; private set; }
        protected TEvent TheEvent { get; private set; }
        protected IReadRepository Repository { get; private set; }
        
        protected override void OnTestSetup()
        {
            EventSourceId = Guid.NewGuid();
            var factory = NcqrsEnvironment.Get<IRepositoryFactory>();
            var denormalizer = CreateDenormalizer(factory);
            var history = Given();
            TheEvent = WhenHandling();
            ApplyHistory(denormalizer, history);
            try
            {
                denormalizer.Handle(TheEvent);
            }
            catch (Exception exception)
            {
                CaughException = exception;
            }
        }

        private void TryClaim(ISourcedEvent @event)
        {
            if (@event.EventSourceId == default(Guid))
                @event.ClaimEvent(EventSourceId, 0);
        }

        private void ApplyHistory(TDenormalizer denormalizer, IEnumerable<ISourcedEvent> history)
        {
            foreach (var evnt in history)
                ApplyHistory(denormalizer, evnt);
        }

        private void ApplyHistory(TDenormalizer denormalizer, ISourcedEvent evnt)
        {
            TryClaim(evnt);
            var handlerInterface = typeof (IEventHandler<>)
                .MakeGenericType(evnt.GetType());
            if (!handlerInterface.IsAssignableFrom(typeof(TDenormalizer)))
                throw new Exception(string.Format("{0} can't handle {1}", typeof (TDenormalizer), evnt.GetType()));
            var d = denormalizer as dynamic;
            d.Handle(evnt);
        }

        [Test]
        public void it_does_not_throw()
        {
            Assert.That(CaughException, Is.Null);
        }
        
    }
}
