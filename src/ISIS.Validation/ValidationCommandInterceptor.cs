using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Reflection;
using FluentValidation;
using Ncqrs.Commanding;
using Ncqrs.Commanding.ServiceModel;

namespace ISIS.Validation
{
    public class ValidationCommandInterceptor : ICommandServiceInterceptor 
    {

        private readonly ConcurrentDictionary<Type, IValidator> _validatorMap;

        public ValidationCommandInterceptor()
        {
            _validatorMap = new ConcurrentDictionary<Type, IValidator>();
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
            var validator = GetValidator(commandType);
            var result = validator.Validate(command);
            if (!result.IsValid)
                throw new ValidationException(result.Errors);
        }

        private IValidator GetValidator(Type commandType)
        {
            return _validatorMap.GetOrAdd(commandType, CreateValidator);
        }

        private static Type SearchValidatorType(Type commandType)
        {
            var genericService = typeof (IValidator<>);
            var concreteService = genericService.MakeGenericType(commandType);
            
            var asm = Assembly.GetExecutingAssembly();
            return asm.GetTypes()
                .Where(t => concreteService.IsAssignableFrom(t))
                .Where(t => t.IsClass && !t.IsAbstract)
                .FirstOrDefault();
        }

        private static IValidator CreateValidator(Type commandType)
        {
            var validatorType = SearchValidatorType(commandType);
            return validatorType == null
                       ? null
                       : (IValidator) Activator.CreateInstance(validatorType);
        }

    }
}
