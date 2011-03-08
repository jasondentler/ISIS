using Ncqrs.Commanding.CommandExecution;
using Ncqrs.Commanding.CommandExecution.Mapping.Fluent;
using Ncqrs.Commanding.ServiceModel;

namespace ISIS.Schedule
{
    public class CourseMapping : ICommandMapping
    {
        public void MapCommands(CommandService commandService)
        {
            Map.Command<CreateCreditCourseCommand>()
                .ToAggregateRoot<Course>()
                .CreateNew(cmd => new Course(cmd.CourseId, cmd.Rubric, cmd.CourseNumber, cmd.Title, cmd.Types))
                .RegisterWith(commandService);

            Map.Command<ChangeCIPCommand>()
                .ToAggregateRoot<Course>()
                .WithId(cmd => cmd.CourseId)
                .ToCallOn((cmd, course) => course.AssignCIP(cmd.CIP))
                .RegisterWith(commandService);

            Map.Command<ChangeApprovalNumberCommand>()
                .ToAggregateRoot<Course>()
                .WithId(cmd => cmd.CourseId)
                .ToCallOn((cmd, course) => course.AssignApprovalNumber(cmd.ApprovalNumber))
                .RegisterWith(commandService);

            Map.Command<ChangeCourseTitleCommand>()
                .ToAggregateRoot<Course>()
                .WithId(cmd => cmd.CourseId)
                .ToCallOn((cmd, course) => course.ChangeCourseTitle(cmd.NewTitle))
                .RegisterWith(commandService);

            Map.Command<ChangeCourseLongTitleCommand>()
                .ToAggregateRoot<Course>()
                .WithId(cmd => cmd.CourseId)
                .ToCallOn((cmd, course) => course.ChangeCourseLongTitle(cmd.NewLongTitle))
                .RegisterWith(commandService);

            Map.Command<ChangeCourseDescriptionCommand>()
                .ToAggregateRoot<Course>()
                .WithId(cmd => cmd.CourseId)
                .ToCallOn((cmd, course) => course.ChangeDescription(cmd.NewDescription))
                .RegisterWith(commandService);

            Map.Command<ActivateCourseCommand>()
                .ToAggregateRoot<Course>()
                .WithId(cmd => cmd.CourseId)
                .ToCallOn((cmd, course) => course.Activate())
                .RegisterWith(commandService);

            Map.Command<DeactivateCourseCommand>()
                .ToAggregateRoot<Course>()
                .WithId(cmd => cmd.CourseId)
                .ToCallOn((cmd, course) => course.Deactivate())
                .RegisterWith(commandService);

            Map.Command<MakeCoursePendingCommand>()
                .ToAggregateRoot<Course>()
                .WithId(cmd => cmd.CourseId)
                .ToCallOn((cmd, course) => course.MakePending())
                .RegisterWith(commandService);

            Map.Command<MakeCourseObsoleteCommand>()
                .ToAggregateRoot<Course>()
                .WithId(cmd => cmd.CourseId)
                .ToCallOn((cmd, course) => course.MakeObsolete())
                .RegisterWith(commandService);

            Map.Command<AddCourseTypeToCourseCommand>()
                .ToAggregateRoot<Course>()
                .WithId(cmd => cmd.CourseId)
                .ToCallOn((cmd, course) => course.AddCourseType(cmd.Type))
                .RegisterWith(commandService);

        }

    }
}
