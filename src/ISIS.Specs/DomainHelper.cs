using System;
using System.Collections.Generic;
using System.Linq;
using Ncqrs;
using Ncqrs.Commanding;
using Ncqrs.Commanding.ServiceModel;
using Ncqrs.Eventing;
using Ncqrs.Eventing.Storage;
using Ncqrs.Spec;
using TechTalk.SpecFlow;

namespace ISIS.Specs
{
    public static class DomainHelper
    {

        private const string TestedEvents = "testedEvents";

        public static void GivenEvent(Guid eventSourceId, object @event)
        {
            var store = NcqrsEnvironment.Get<IEventStore>();
            var existingEvents = store.ReadFrom(eventSourceId, 0, long.MaxValue);
            long maxEventSequence = 0;
            if (existingEvents.Any())
                maxEventSequence = existingEvents.Max(e => e.EventSequence);

            var stream = Prepare.Events(@event)
                .ForSourceUncomitted(eventSourceId, Guid.NewGuid(), (int)maxEventSequence + 1);

            store.Store(stream);
        }

        public static IEnumerable<UncommittedEvent> When(Action action)
        {
            IEnumerable<UncommittedEvent> events;
            using (var context = new EventContext())
            {
                action();
                events = context.Events;
            }
            ScenarioContext.Current.Set(new HashSet<object>(), TestedEvents);
            ScenarioContext.Current.Set(events);
            return events;
        }

        public static IEnumerable<UncommittedEvent> WhenExecuting(ICommand command)
        {
            ScenarioContext.Current.Set(command);
            return When(() =>
                            {
                                var cmdService = NcqrsEnvironment.Get<ICommandService>();
                                cmdService.Execute(command);
                            });
        }

        public static T GetCommand<T>()
            where T : ICommand
        {
            return (T) ScenarioContext.Current.Get<ICommand>();
        }


        private static IEnumerable<UncommittedEvent> GetEvents()
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
            var testedEvents = ScenarioContext.Current.Get<HashSet<object>>(TestedEvents);
            foreach (var @event in events)
                testedEvents.Add(@event);
        }

        public static bool AllEventsWereTested()
        {
            var testedEvents = ScenarioContext.Current.Get<HashSet<object>>(TestedEvents);
            var untestedEvents = GetRawEvents().Except(testedEvents);
            return !untestedEvents.Any();
        }

    }
}
