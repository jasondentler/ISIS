using System;
using Ncqrs;
using Ncqrs.Commanding.CommandExecution;
using Ncqrs.Commanding.CommandExecution.Mapping.Fluent;
using Ncqrs.Commanding.ServiceModel;
using Ncqrs.Domain.Storage;

namespace ISIS
{
    public class DepartmentMapping : ICommandMapping
    {
        public void MapCommands(CommandService commandService)
        {
            Map.Command<CreateDepartmentCommand>()
                .ToAggregateRoot<Department>()
                .CreateNew(cmd => new Department(cmd.Name))
                .StoreInDomainRepository()
                .RegisterWith(commandService);

            Map.Command<ChangeDefaultSubjectCommand>()
                .ToAggregateRoot<Department>()
                .WithId(cmd => cmd.DepartmentId)
                .ToCallOn((cmd, department) => department.ChangeDepartmentDefaultSubject(cmd.DefaultSubject))
                .RegisterWith(commandService);
        }
    }
}
