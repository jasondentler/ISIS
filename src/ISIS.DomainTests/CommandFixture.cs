using System;
using System.Collections.Generic;
using System.Linq;
using ISIS.DomainTests.Environment;
using Ncqrs;
using Ncqrs.Commanding;
using Ncqrs.Commanding.CommandExecution;
using Ncqrs.Commanding.ServiceModel;
using Ncqrs.Domain;
using Ncqrs.Domain.Storage;
using Ncqrs.Eventing.Sourcing;
using Ncqrs.Eventing.Storage;
using Ncqrs.Spec;

namespace ISIS.DomainTests
{

    public abstract class CommandFixture<TCommand, TAggregateRoot> : CommandTestFixture<TCommand>
        where TCommand : ICommand
        where TAggregateRoot : AggregateRoot 
    {
        protected CommandFixture()
        {
            EnsureConfiguredEnvironment();
        }

        protected IAggregateRootCreationStrategy CreationStrategy { get; set; }
        protected TAggregateRoot AggregateRoot { get; set; }
        protected Guid EventSourceId { get; private set; }

        protected override void SetupDependencies()
        {
            EventSourceId = Guid.NewGuid();

            var history = Given();
            if (!history.Any()) return;

            var eventStore = (TestEventStore)NcqrsEnvironment.Get<IEventStore>();
            var repo = NcqrsEnvironment.Get<IDomainRepository>();

            eventStore.Setup(EventSourceId, history);
            AggregateRoot = repo.GetById<TAggregateRoot>(EventSourceId);
        }

        private static void EnsureConfiguredEnvironment()
        {
            if(!NcqrsEnvironment.IsConfigured)
                NcqrsEnvironment.Configure(new TestEnvironmentConfiguration());
        }

        protected virtual IEnumerable<ISourcedEvent> Given()
        {
            return new SourcedEvent[0];
        }

        protected override ICommandExecutor<TCommand> BuildCommandExecutor()
        {
            var cmdService = (TestCommandService) NcqrsEnvironment.Get<ICommandService>();
            return cmdService.GetCommandExecutor<TCommand>();
        }

    }

}
