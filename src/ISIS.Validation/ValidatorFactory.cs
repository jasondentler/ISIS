using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Reflection;
using FluentValidation;
using Ncqrs;

namespace ISIS.Validation
{
    public class ValidatorFactory : IValidatorFactory
    {
        private static readonly ILog Log = Ncqrs.LogManager.GetLogger(typeof(ValidationCommandInterceptor));

        private readonly ConcurrentDictionary<Type, IValidator> _validatorMap;

        public ValidatorFactory()
        {
            _validatorMap = new ConcurrentDictionary<Type, IValidator>();
        }

        public IValidator<T> GetValidator<T>()
        {
            return (IValidator<T>)GetValidator(typeof(T));
        }

        public IValidator GetValidator(Type type)
        {
            return _validatorMap.GetOrAdd(type, CreateValidator);
        }

        private static Type SearchValidatorType(Type commandType)
        {
            var genericService = typeof(IValidator<>);
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
            if (validatorType == null)
            {
                Log.WarnFormat("No validator for {0}", commandType);
                return null;
            }
            return (IValidator)Activator.CreateInstance(validatorType);
        }


    }
}
