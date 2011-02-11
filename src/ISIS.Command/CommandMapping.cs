using System;
using System.Collections.Concurrent;
using Ncqrs.Commanding;
using Ncqrs.Commanding.CommandExecution;
using Ncqrs.Commanding.CommandExecution.Mapping;
using Ncqrs.Commanding.CommandExecution.Mapping.Fluent;

namespace ISIS
{
    public static class CommandMapping
    {

        private static readonly ConcurrentDictionary<Type, object> CommandExecutors = 
            new ConcurrentDictionary<Type, object>();


        static CommandMapping()
        {
            MapDepartment();
        }

        private static void MapDepartment()
        {

            Map.Command<CreateDepartmentCommand>()
                .ToAggregateRoot<Department>()
                .CreateNew(cmd => new Department(cmd.Name))
                .StoreIn((cmd, department) => { throw new NotImplementedException(); })
                .Register();
            

            Map.Command<ChangeDefaultSubjectCommand>()
                .ToAggregateRoot<Department>()
                .WithId(cmd => cmd.DepartmentId)
                .ToCallOn((cmd, department) => department.ChangeDepartmentDefaultSubject(cmd.DefaultSubject))
                .Register();
        }


        private static void Register<TCommand>(this ICommandExecutor<TCommand> commandExecutor)
            where TCommand : ICommand
        {
            var commandType = typeof (TCommand);
            CommandExecutors.TryAdd(commandType, commandExecutor);
        }

        public static ICommandExecutor<TCommand> Get<TCommand>()
            where TCommand : ICommand
        {
            var commandType = typeof (TCommand);
            object executor;
            if (CommandExecutors.TryGetValue(commandType, out executor))
                return (ICommandExecutor<TCommand>) executor;
            return null;
        }


    }
}
