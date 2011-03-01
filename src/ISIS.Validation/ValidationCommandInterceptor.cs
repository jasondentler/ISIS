using System;
using FluentValidation;
using Ncqrs.Commanding;
using Ncqrs.Commanding.ServiceModel;

namespace ISIS.Validation
{
    public class ValidationCommandInterceptor : ICommandServiceInterceptor 
    {

        private readonly IValidatorFactory _validatorFactory;

        public ValidationCommandInterceptor()
        {
            _validatorFactory = new ValidatorFactory();
        }

        public ValidationCommandInterceptor(IValidatorFactory validatorFactory)
        {
            _validatorFactory = validatorFactory;
        }

        public void OnBeforeBeforeExecutorResolving(CommandContext context)
        {
            // Do nothing
        }

        public void OnBeforeExecution(CommandContext context)
        {
            // Validate command instance
            Validate(context.TheCommandType, context.TheCommand);
        }

        public void OnAfterExecution(CommandContext context)
        {
            // Do nothing
        }

        private void Validate(Type commandType, ICommand command)
        {
            var validator = _validatorFactory.GetValidator(commandType);
            if (validator == null)
                throw new MissingCommandValidatorException(commandType);
            var result = validator.Validate(command);
            if (!result.IsValid)
                throw new ValidationException(result.Errors);
        }


    }
}
