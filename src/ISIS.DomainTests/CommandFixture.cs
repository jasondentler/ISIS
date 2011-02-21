using System;
using System.Collections.Generic;
using System.Linq;
using Ncqrs;
using Ncqrs.Commanding;
using Ncqrs.Commanding.CommandExecution;
using Ncqrs.Commanding.ServiceModel;
using Ncqrs.Domain;
using Ncqrs.Domain.Storage;
using Ncqrs.Eventing;
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
        protected Guid CommandId { get; private set; }

        protected override void SetupDependencies()
        {
            EventSourceId = Guid.NewGuid();
            CommandId = Guid.NewGuid();

            var history = GivenUncommittedEvents();
            if (!history.Any()) return;

            var eventStore = (TestEventStoreWrapper) NcqrsEnvironment.Get<IEventStore>();
            eventStore.Setup(EventSourceId, history);

            var factory = NcqrsEnvironment.Get<IUnitOfWorkFactory>() ?? new UnitOfWorkFactory();
            using (var uow = factory.CreateUnitOfWork(CommandId))
            {
                AggregateRoot = (TAggregateRoot)UnitOfWorkContext.Current.GetById(typeof(TAggregateRoot), EventSourceId, null);
                uow.Accept();
            }
        }

        private static void EnsureConfiguredEnvironment()
        {
            if(!NcqrsEnvironment.IsConfigured)
                NcqrsEnvironment.Configure(new TestEnvironmentConfiguration());
        }

        protected virtual IEnumerable<object> Given()
        {
            return new object[0];
        }

        protected virtual IEnumerable<UncommittedEvent> GivenUncommittedEvents()
        {
            long sequence = 0;
            return Given().Select(evnt => new UncommittedEvent(
                                              Guid.NewGuid(), EventSourceId,
                                              sequence++, 0, DateTime.Now,
                                              evnt, new Version(0, 0, 0, 0)));
        }

        protected override ICommandExecutor<ICommand> BuildCommandExecutor()
        {
            var cmdService = (TestCommandService)NcqrsEnvironment.Get<ICommandService>();
            var realExecutor = cmdService.GetCommandExecutor<TCommand>();
            return new WrappedCommandExecutor<TCommand>(realExecutor);
        }
        
    }

}
