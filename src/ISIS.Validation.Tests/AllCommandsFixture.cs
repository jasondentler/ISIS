using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using ISIS.Schedule;
using Ncqrs.Commanding;
using Ncqrs.Spec;
using NUnit.Framework;

namespace ISIS
{
    [Specification]
    public class AllCommandsFixture
    {

        [Then]
        public void all_commands_are_checked()
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
                var fixtureBaseType = typeof (ValidationFixture<>)
                    .MakeGenericType(commandType);

                var fixtureTypes = commandFixtureTypes
                    .Where(t => fixtureBaseType.IsAssignableFrom(t))
                    .ToArray();

                if (!fixtureTypes.Any())
                    missingTests.Add(commandType);
            }

            if (missingTests.Any())
                Assert.Fail("The following commands are not being tested for validation: {0}",
                            string.Join(Environment.NewLine, missingTests));

        }

    }
}
