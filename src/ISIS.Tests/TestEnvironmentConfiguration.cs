using System;
using System.Collections.Generic;
using Ncqrs;
using Ncqrs.Commanding.ServiceModel;
using Ncqrs.Config;
using Ncqrs.Domain.Storage;
using Ncqrs.Eventing.ServiceModel.Bus;
using Ncqrs.Eventing.Storage;

namespace ISIS
{
    public class TestEnvironmentConfiguration : IEnvironmentConfiguration
    {

        private readonly Dictionary<Type, object> _instances;
        private readonly Dictionary<Type, Func<object>> _factories;

        public TestEnvironmentConfiguration()
        {

            _factories = new Dictionary<Type, Func<object>>();
            _instances = new Dictionary<Type, object>();

            _factories[typeof(IEventStore)] = () => new SimpleTestEventStoreWrapper<InMemoryEventStore>();
            _factories[typeof (IEventBus)] = () => new NullEventBus();
            _factories[typeof(IAggregateRootCreationStrategy)] = () => new SimpleAggregateRootCreationStrategy();
            _factories[typeof (IDomainRepository)] =
                () => new DomainRepository(
                          NcqrsEnvironment.Get<IAggregateRootCreationStrategy>(),
                          NcqrsEnvironment.Get<IAggregateSnapshotter>());
            _factories[typeof (ICommandService)] =
                () =>
                    {
                        var testCommandService = new TestCommandService();
                        testCommandService.Configure();
                        return testCommandService;
                    };
        
        }

        public bool TryGet<T>(out T result) where T : class
        {
            return TryGetInstance(out result)
                   || TryBuild(out result);
        }

        private bool TryGetInstance<T>(out T result) where T : class
        {
            object instance;
            if (_instances.TryGetValue(typeof(T), out instance))
            {
                result = (T) instance;
                return true;
            }
            result = null;
            return false;
        }

        private bool TryBuild<T>(out T result) where T : class
        {
            Func<object> factory;
            if (_factories.TryGetValue(typeof(T), out factory))
            {
                var instance = factory();
                result = (T)instance;
                _instances[typeof(T)] = instance;
                return true;
            }
            result = null;
            return false;

        }


    }
}
