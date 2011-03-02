using FluentValidation;
using ISIS.Schedule;
using Ncqrs;
using Ncqrs.Commanding;
using Ncqrs.Commanding.ServiceModel;
using NUnit.Framework;

namespace ISIS.Command.Executors.Tests
{
    [TestFixture]
    public class CommandServiceFixture : BaseFixture 
    {

        public class UnvalidatedCommand : CommandBase 
        {}

        [Test, ExpectedException(typeof(ValidationException))]
        public void Command_service_validates_commands()
        {
            var cmdService = NcqrsEnvironment.Get<ICommandService>();
            cmdService.Execute(new CreateCourseCommand());
        }

        [Test, ExpectedException(typeof(MissingCommandValidatorException))]
        public void Command_service_requires_a_validator()
        {
            var cmdService = NcqrsEnvironment.Get<ICommandService>();
            cmdService.Execute(new UnvalidatedCommand());
        }



    }
}
