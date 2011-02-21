using Ncqrs.Commanding;
using Ncqrs.Commanding.CommandExecution;
using Ncqrs.Commanding.CommandExecution.Mapping.Fluent;
using Ncqrs.Domain;

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
            return mapping.StoreIn(
                (cmd, aggregate) => UnitOfWorkContext.Current.Accept());
        }

    }
}
