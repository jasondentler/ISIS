using System;
using Ncqrs.Commanding;
using Ncqrs.Commanding.ServiceModel;
using Ninject;

namespace ISIS
{
    public class SetValidationCommandInterceptor : ICommandServiceInterceptor
    {
        private readonly IKernel _kernel;

        public SetValidationCommandInterceptor(IKernel kernel)
        {
            _kernel = kernel;
        }
        
        public void OnBeforeBeforeExecutorResolving(CommandContext context)
        {
        }

        public void OnBeforeExecution(CommandContext context)
        {
            ValidateSet(context.TheCommandType, context.TheCommand);
        }

        public void OnAfterExecution(CommandContext context)
        {
        }

        private void ValidateSet(Type commandType, ICommand command)
        {
            var genericType = typeof (ISetValidator<>);
            var specificType = genericType.MakeGenericType(commandType);
            dynamic dynamicCommand = command;
            dynamic setValidator = _kernel.TryGet(specificType);
            if (setValidator == null)
                return;
            setValidator.Validate(dynamicCommand);
        }

    }
}
