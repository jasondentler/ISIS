using System;
using System.Collections.Generic;
using System.Linq;
using Ncqrs.Commanding;
using Ncqrs.Commanding.CommandExecution;
using Ncqrs.Domain;
using Ncqrs.Domain.Storage;
using Ncqrs.Eventing.Sourcing;
using Ncqrs.Spec;

namespace ISIS.DomainTests
{

    public abstract class CommandFixture<TCommand, TAggregateRoot> : CommandTestFixture<TCommand>
        where TCommand : ICommand
        where TAggregateRoot : AggregateRoot 
    {

        protected IAggregateRootCreationStrategy CreationStrategy { get; set; }
        protected TAggregateRoot AggregateRoot { get; set; }
        protected Guid eventSourceId { get; set; }

        protected override void SetupDependencies()
        {
            long sequence = 0;

            CreationStrategy = new SimpleAggregateRootCreationStrategy();
            eventSourceId = Guid.NewGuid();
            var history = Given();
            AggregateRoot = CreationStrategy.CreateAggregateRoot<TAggregateRoot>();

            if (!history.Any()) return;

            foreach (var @event in history)
                @event.ClaimEvent(eventSourceId, ++sequence);

            AggregateRoot.InitializeFromHistory(history);
        }

        protected virtual IEnumerable<ISourcedEvent> Given()
        {
            return new SourcedEvent[0];
        }


        protected override ICommandExecutor<TCommand> BuildCommandExecutor()
        {
            return CommandMapping.Get<TCommand>();
        }

    }

}
