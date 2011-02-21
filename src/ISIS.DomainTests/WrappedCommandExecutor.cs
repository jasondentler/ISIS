using Ncqrs.Commanding;
using Ncqrs.Commanding.CommandExecution;

namespace ISIS.DomainTests
{
    public class WrappedCommandExecutor<TCommand> : ICommandExecutor<ICommand>
        where TCommand : ICommand
    {

        private readonly ICommandExecutor<TCommand> _realExecutor;

        public WrappedCommandExecutor(ICommandExecutor<TCommand> realExecutor)
        {
            _realExecutor = realExecutor;
        }

        public void Execute(ICommand command)
        {
            _realExecutor.Execute((TCommand) command);
        }
    }
}
