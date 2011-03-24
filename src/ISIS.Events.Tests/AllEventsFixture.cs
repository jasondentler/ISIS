using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using ISIS.Schedule;
using Ncqrs.Spec;
using NUnit.Framework;

namespace ISIS
{
    [Specification]
    public class AllEventsFixture
    {

        [Then]
        public void all_events_are_checked()
        {
            var eventAssembly = typeof (CourseCreatedEvent).Assembly;
            var testAssembly = Assembly.GetExecutingAssembly();

            var eventTypes = eventAssembly.GetTypes()
                .Where(t => t.IsClass
                            && !t.IsAbstract
                            && typeof (IEvent).IsAssignableFrom(t));

            var eventFixtureTypes = testAssembly.GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract);

            var missingTests = new HashSet<Type>();

            foreach (var eventType in eventTypes)
            {
                var fixtureBaseType = typeof (EventSerializationFixture<>)
                    .MakeGenericType(eventType);

                var fixtureTypes = eventFixtureTypes
                    .Where(t => fixtureBaseType.IsAssignableFrom(t))
                    .ToArray();

                if (!fixtureTypes.Any())
                    missingTests.Add(eventType);
            }

            if (missingTests.Any())
                Assert.Fail("The following events are not being tested: {0}",
                            string.Join(Environment.NewLine, missingTests));

        }

    }
}
