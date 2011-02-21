using System;
using System.Collections.Generic;
using System.Linq;
using ISIS.NHibernateReadModel;
using Ncqrs.Eventing.ServiceModel.Bus;
using Ncqrs.Eventing.Sourcing;
using NUnit.Framework;

namespace ISIS.Denormalizers.Tests
{
    public abstract class DenormalizerFixture<TDenormalizer, TEvent>
        : BaseFixture
        where TDenormalizer : IDenormalizer, IEventHandler<TEvent>
    {

        protected abstract TDenormalizer CreateDenormalizer(IRepositoryFactory factory);
        protected virtual IEnumerable<object> Given()
        {
            return new object[0];    
        }


        protected abstract TEvent WhenHandling();

        protected Guid EventSourceId { get; private set; }
        protected Exception CaughException { get; private set; }
        protected TEvent TheEvent { get; private set; }
        protected IReadRepository Repository { get; private set; }
        
        protected override void  OnFixtureSetup()
        {
            base.OnFixtureSetup();
            new DatabaseHelper().Setup();

            EventSourceId = Guid.NewGuid();
            var factory = new RepositoryFactory();
            Repository = factory.CreateRepository();
            var denormalizer = CreateDenormalizer(factory);
            var history = Given();
            TheEvent = WhenHandling();
            ApplyHistory(denormalizer, history);
            try
            {
                var evnt = new PublishableEvent(Guid.NewGuid(),
                                     DateTime.Now, new Version(0, 0, 0, 0),
                                     EventSourceId, history.Count(),
                                     Guid.NewGuid(), TheEvent);
                denormalizer.Handle(new PublishedEvent<TEvent>(evnt));
            }
            catch (Exception exception)
            {
                CaughException = exception;
            }
        }

        protected override void OnFixtureTearDown()
        {
            new DatabaseHelper().TearDown();
            base.OnFixtureTearDown();
        }
        
        private void ApplyHistory(TDenormalizer denormalizer, IEnumerable<object> history)
        {
            foreach (var evnt in history)
                ApplyHistory(denormalizer, evnt);
        }

        private void ApplyHistory(TDenormalizer denormalizer, object evnt)
        {
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
