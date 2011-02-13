using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ncqrs;
using Ncqrs.Commanding;
using Ncqrs.Commanding.CommandExecution;
using Ncqrs.Commanding.CommandExecution.Mapping.Fluent;
using Ncqrs.Domain;
using Ncqrs.Domain.Storage;

namespace ISIS
{
    public static class MappingExtensions
    {

        public static ICommandExecutor<TCommand>
            StoreInDomainRepository<TCommand, TAggregateRoot>
            (this MappedCommandToAggregateRootConstructor<TCommand, TAggregateRoot> mapping)
            where TCommand : ICommand
            where TAggregateRoot : AggregateRoot
        {
            return mapping.StoreIn((cmd, aggregate) => NcqrsEnvironment.Get<IDomainRepository>().Save(aggregate));
        }

    }
}
