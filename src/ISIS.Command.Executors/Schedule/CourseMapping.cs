using Ncqrs;
using Ncqrs.Commanding.CommandExecution;
using Ncqrs.Commanding.CommandExecution.Mapping.Fluent;
using Ncqrs.Commanding.ServiceModel;
using Ncqrs.Domain;

namespace ISIS.Schedule
{
    public class CourseMapping : ICommandMapping
    {
        public void MapCommands(CommandService commandService)
        {

            Map.Command<CreateCreditCourseCommand>()
                .ToAggregateRoot<Course>()
                .CreateNew(cmd =>
                               {
                                   var clock = NcqrsEnvironment.Get<IClock>();
                                   return new Course(
                                       cmd.CourseId,
                                       cmd.Rubric,
                                       cmd.CourseNumber,
                                       cmd.Title,
                                       cmd.Types,
                                       clock);
                               })
                .RegisterWith(commandService);

            Map.Command<CreateContinuingEducationCourseCommand>()
                .ToAggregateRoot<Course>()
                .CreateNew(cmd =>
                               {
                                   var clock = NcqrsEnvironment.Get<IClock>();
                                   return new Course(
                                       cmd.CourseId,
                                       cmd.Rubric,
                                       cmd.CourseNumber,
                                       cmd.Title,
                                       cmd.Type,
                                       cmd.EffectiveDate,
                                       cmd.ApprovedBy,
                                       clock);
                               })
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

            Map.Command<RemoveCourseTypeFromCourseCommand>()
                .ToAggregateRoot<Course>()
                .WithId(cmd => cmd.CourseId)
                .ToCallOn((cmd, course) => course.RemoveCourseType(cmd.Type))
                .RegisterWith(commandService);
            
            Map.Command<ChangeCourseCreditTypeCommand>()
                .ToAggregateRoot<Course>()
                .WithId(cmd => cmd.CourseId)
                .ToCallOn((cmd, course) => course.ChangeCreditType(cmd.Type))
                .RegisterWith(commandService);

            Map.Command<ChangeCourseCEUsCommand>()
                .ToAggregateRoot<Course>()
                .WithId(cmd => cmd.CourseId)
                .ToCallOn((cmd, course) => course.ChangeCEUs(cmd.CEUs))
                .RegisterWith(commandService);

            Map.Command<ChangeCourseTopicCodeCommand>()
                .ToAggregateRoot<Course>()
                .WithId(cmd => cmd.CourseId)
                .ToCallOn((cmd, course) =>
                              {
                                  var uow = UnitOfWorkContext.Current;
                                  var topicCode = (TopicCode) uow.GetById(typeof (TopicCode), cmd.TopicCodeId, null);
                                  course.ChangeTopicCode(topicCode);
                              })
                .RegisterWith(commandService);

        }

    }
}
