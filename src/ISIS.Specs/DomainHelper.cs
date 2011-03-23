using System;
using System.Collections.Generic;
using System.Linq;
using Ncqrs;
using Ncqrs.Commanding;
using Ncqrs.Commanding.ServiceModel;
using Ncqrs.Eventing;
using Ncqrs.Eventing.Storage;
using Ncqrs.Spec;
using Newtonsoft.Json;
using TechTalk.SpecFlow;

namespace ISIS.Specs
{
    public static class DomainHelper
    {

        private static bool ContainsKey(string key)
        {
            return ScenarioContext.Current.ContainsKey(key);
        }

        private static T Get<T>() where T : class
        {
            return ScenarioContext.Current.Get<T>();
        }

        private static T Get<T>(string key) where T : class
        {
            return ScenarioContext.Current.Get<T>(key);
        }

        private static void Set<T>(T item) where T : class 
        {
            ScenarioContext.Current.Set(item);
        }

        private static void Set<T>(T item, string key) where T : class
        {
            ScenarioContext.Current.Set(item, key);
        }

        private const string ExceptionKey = "caughtException";
        private const string TestedEventsKey = "testedEvents";
        private const string EventSourceIdKey = "eventSourceId";

        private class WrappedValue<T>
        {
            public WrappedValue(T guid)
            {
                Value = guid;
            }

            public T Value { get; set; }
        }

        public static Guid GetEventSourceId()
        {
            if (!ContainsKey(EventSourceIdKey))
            {
                var id = Guid.NewGuid();
                Set(new WrappedValue<Guid>(id), EventSourceIdKey);
                return id;
            }
            return Get<WrappedValue<Guid>>(EventSourceIdKey).Value;
        }

        public static void GivenEvent(object @event)
        {
            GivenEvent(GetEventSourceId(), @event);
        }

        public static void GivenEvent(Guid eventSourceId, object @event)
        {
            Console.Write("\tGiven ");
            WriteOutObject(@event);

            var store = NcqrsEnvironment.Get<IEventStore>();
            var existingEvents = store.ReadFrom(eventSourceId, 0, long.MaxValue);
            long maxEventSequence = 0;
            if (existingEvents.Any())
                maxEventSequence = existingEvents.Max(e => e.EventSequence);

            var stream = Prepare.Events(@event)
                .ForSourceUncomitted(eventSourceId, Guid.NewGuid(), (int)maxEventSequence);

            store.Store(stream);
        }

        private static void WriteOutObject(object  @event)
        {
            var jsonEvent = JsonConvert.SerializeObject(@event);
            Console.WriteLine("{0}: {1}",
                              @event.GetType(),
                              jsonEvent);
        }

        public static IEnumerable<UncommittedEvent> When(Action action)
        {
            try
            {
                IEnumerable<UncommittedEvent> events;
                using (var context = new EventContext())
                {
                    action();
                    events = context.Events;
                }
                ScenarioContext.Current.Set(new HashSet<object>(), TestedEventsKey);
                ScenarioContext.Current.Set(events);
                return events;
            }
            catch (Exception e)
            {
                ScenarioContext.Current.Set(e, ExceptionKey);
                return new UncommittedEvent[0];
            }
        }

        public static IEnumerable<UncommittedEvent> WhenExecuting(ICommand command)
        {
            Console.Write("\tWhen ");
            WriteOutObject(command);

            ScenarioContext.Current.Set(command);
            var stream = When(() =>
                                  {
                                      var cmdService = NcqrsEnvironment.Get<ICommandService>();
                                      cmdService.Execute(command);
                                  });
            foreach (var e in stream.Select(e => e.Payload))
            {
                Console.Write("\tResulting ");
                WriteOutObject(e);
            }
            if (HasException())
            {
                Console.Write("\tResulting ");
                Console.WriteLine(GetException().ToString());
            }
            return stream;
        }

        public static bool HasException()
        {
            return ScenarioContext.Current.ContainsKey(ExceptionKey);
        }

        public static Exception GetException()
        {
            return HasException() ? ScenarioContext.Current.Get<Exception>(ExceptionKey) : null;
        }

        public static T GetException<T>()
            where T : Exception
        {
            return (T) GetException();
        }

        public static T GetCommand<T>()
            where T : ICommand
        {
            return (T) ScenarioContext.Current.Get<ICommand>();
        }


        public static IEnumerable<UncommittedEvent> GetEvents()
        {
            return ScenarioContext.Current.Get<IEnumerable<UncommittedEvent>>();
        }

        private static IEnumerable<object> GetRawEvents()
        {
            return GetEvents().Select(e => e.Payload);
        }

        public static IEnumerable<T> GetEvents<T>()
        {
            var retval = GetRawEvents().OfType<T>();
            AddToTestedEvents(retval);
            return retval;
        }

        public static T GetEvent<T>()
        {
            return GetEvents<T>().SingleOrDefault();
        }

        private static void AddToTestedEvents<T>(IEnumerable<T> @events)
        {
            var testedEvents = ScenarioContext.Current.Get<HashSet<object>>(TestedEventsKey);
            foreach (var @event in events)
                testedEvents.Add(@event);
        }

        public static bool AllEventsWereTested()
        {
            var testedEvents = ScenarioContext.Current.Get<HashSet<object>>(TestedEventsKey);
            var untestedEvents = GetRawEvents().Except(testedEvents);
            return !untestedEvents.Any();
        }

    }
}
