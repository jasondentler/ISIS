using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using ISIS.Schedule;
using Ncqrs.Commanding;
using Ncqrs.Eventing;
using Ncqrs.Spec;
using NUnit.Framework;

namespace ISIS
{
    [Specification]
    public class AllCommandsFixture
    {

        [Then]
        public void all_events_are_checked()
        {
            var commandAssembly = typeof(CreateCreditCourseCommand).Assembly;
            var testAssembly = Assembly.GetExecutingAssembly();

            var commandTypes = commandAssembly.GetTypes()
                .Where(t => t.IsClass
                            && !t.IsAbstract
                            && typeof (ICommand).IsAssignableFrom(t));

            var commandFixtureTypes = testAssembly.GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract);

            var missingTests = new HashSet<Type>();

            foreach (var commandType in commandTypes)
            {
                var commandType2 = commandType;
                var domainFixtureType = typeof (DomainFixture<>)
                    .MakeGenericType(commandType);

                var fixtureTypes = commandFixtureTypes
                    .Where(t => domainFixtureType.IsAssignableFrom(t))
                    .ToArray();


                var simpleDomainFixtureTypes = commandFixtureTypes
                    .Select(t => new {type = t, baseType = t.BaseType})
                    .Where(item => item.baseType.IsGenericType)
                    .Select(item => new
                                        {
                                            item.type,
                                            item.baseType,
                                            generic = item.baseType.GetGenericTypeDefinition(),
                                            genericArgs = item.baseType.GetGenericArguments()
                                        })
                    .Where(item => item.generic == typeof (SimpleDomainFixture<,>)
                                   && item.genericArgs.First() == commandType2);

                if (!fixtureTypes.Any() && !simpleDomainFixtureTypes.Any())
                    missingTests.Add(commandType);
            }

            if (missingTests.Any())
                Assert.Fail("The following commands do not have a domain test: {0}",
                            string.Join(Environment.NewLine, missingTests));

        }

    }
}
