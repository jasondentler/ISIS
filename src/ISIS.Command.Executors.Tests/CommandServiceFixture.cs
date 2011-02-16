using FluentValidation;
using Ncqrs;
using Ncqrs.Commanding.ServiceModel;
using NUnit.Framework;

namespace ISIS.Command.Executors.Tests
{
    [TestFixture]
    public class CommandServiceFixture : BaseFixture 
    {

        [Test, ExpectedException(typeof(ValidationException))]
        public void Command_service_validates_commands()
        {
            var cmdService = NcqrsEnvironment.Get<ICommandService>();
            cmdService.Execute(new CreateCourseCommand());
        }

    }
}
