using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FluentDML.ReadModel;
using ISIS.NHibernateReadModel;
using Ncqrs.Eventing.ServiceModel.Bus;
using NHibernate;
using Ninject;
using NUnit.Framework;

namespace ISIS.Denormalizers.Tests
{
    public abstract class DenormalizerFixture<TDenormalizer, TEvent>
        : BaseFixture
        where TDenormalizer : IDenormalizer, IEventHandler<TEvent>
    {

        protected abstract TDenormalizer CreateDenormalizer();
        protected virtual IEnumerable<object> Given()
        {
            return new object[0];    
        }

        protected IKernel Kernel;
        protected abstract TEvent WhenHandling();
        private ISession session;
        protected IReadRepository Repository { get; private set; }

        protected Guid EventSourceId { get; private set; }
        protected Exception CaughException { get; private set; }
        protected TEvent TheEvent { get; private set; }
        
        protected override void  OnFixtureSetup()
        {
            Mapper.Reset();
            Kernel = new StandardKernel(new DenormalizerModule());
            base.OnFixtureSetup();
            new DatabaseHelper().Setup();

            EventSourceId = Guid.NewGuid();
            var denormalizer = CreateDenormalizer();
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
            BuildReadRepository();
        }

        protected override void OnFixtureTearDown()
        {
            session.Dispose();
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

            var publishableEvent = new PublishableEvent(Guid.NewGuid(), DateTime.Now,
                                                        new Version(0, 0, 0, 0),
                                                        EventSourceId, 0,
                                                        Guid.NewGuid(), evnt);

            var eventType = evnt.GetType();
            var publishedEventType = typeof (PublishedEvent<>).MakeGenericType(eventType);
            dynamic publishedEvent = Activator.CreateInstance(publishedEventType, new[] {publishableEvent});

            var d = denormalizer as dynamic;
            d.Handle(publishedEvent);
        }

        private void BuildReadRepository()
        {
            session = ReadModelConfiguration.SessionFactory.OpenSession();
            Repository = new ReadRepository(session);
        }

        [Test]
        public void it_does_not_throw()
        {
            Assert.That(CaughException, Is.Null);
        }
        
    }
}
