using Ncqrs.Commanding.CommandExecution;
using Ncqrs.Commanding.CommandExecution.Mapping.Fluent;
using Ncqrs.Commanding.ServiceModel;

namespace ISIS
{
    public class CourseMapping : ICommandMapping
    {
        public void MapCommands(CommandService commandService)
        {
            Map.Command<CreateCourseCommand>()
                .ToAggregateRoot<Course>()
                .CreateNew(cmd => new Course(cmd.Subject, cmd.CourseNumber, cmd.Title))
                .StoreInDomainRepository()
                .RegisterWith(commandService);

        }
    }
}
