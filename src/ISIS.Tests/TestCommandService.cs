using System;
using System.Collections.Concurrent;
using ISIS.Validation;
using Ncqrs.Commanding;
using Ncqrs.Commanding.CommandExecution;
using Ncqrs.Commanding.ServiceModel;

namespace ISIS
{

    public class TestCommandService : CommandService 
    {

        public TestCommandService()
        {
            AddInterceptor(new ValidationCommandInterceptor());
        }

        private readonly ConcurrentDictionary<Type, object> _executors = new ConcurrentDictionary<Type, object>();

        public override void RegisterExecutor<TCommand>(ICommandExecutor<TCommand> executor)
        {
            _executors.AddOrUpdate(typeof (TCommand),
                                   executor, (type, previousValue) => executor);
            base.RegisterExecutor(executor);
        }

        public override void UnregisterExecutor<TCommand>()
        {
            object executor;
            _executors.TryRemove(typeof (TCommand), out executor);
            base.UnregisterExecutor<TCommand>();
        }

        public ICommandExecutor<TCommand> GetCommandExecutor<TCommand>()
            where TCommand : ICommand
        {
            object executor = null;
            if (!_executors.TryGetValue(typeof(TCommand), out executor))
                throw new ExecutorForCommandNotFoundException(typeof (TCommand));
            return (ICommandExecutor<TCommand>) executor;
        }


    }

}
