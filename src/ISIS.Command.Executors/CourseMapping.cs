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
                .CreateNew(cmd => new Course(cmd.Rubric, cmd.CourseNumber, cmd.Title))
                .StoreInDomainRepository()
                .RegisterWith(commandService);

            Map.Command<AssignCIPCommand>()
                .ToAggregateRoot<Course>()
                .WithId(cmd => cmd.CourseId)
                .ToCallOn((cmd, course) => course.AssignCIPNumber(cmd.CIP))
                .RegisterWith(commandService);

            Map.Command<AssignApprovalNumberCommand>()
                .ToAggregateRoot<Course>()
                .WithId(cmd => cmd.CourseId)
                .ToCallOn((cmd, course) => course.AssignApprovalNumber(cmd.ApprovalNumber))
                .RegisterWith(commandService);
        }
    }
}
