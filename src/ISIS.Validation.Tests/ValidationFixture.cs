using System;
using System.Linq;
using System.Linq.Expressions;
using FluentValidation;
using FluentValidation.Results;
using NUnit.Framework;

namespace ISIS.Validation.Tests
{

    public abstract class ValidationFixture<T>  : BaseFixture
    {

        protected abstract AbstractValidator<T> CreateValidator();

        private static ValidationFailure GetFailure(ValidationResult result, string propertyName)
        {
            return result.Errors
                .Where(vf => vf.PropertyName == propertyName)
                .First();
        }

        protected ValidationResult GetResult(T instance)
        {
            var validator = CreateValidator();
            return validator.Validate(instance);
        }

        protected ValidationFailure GetFailure<TProperty>(T instance,
            Expression<Func<T, TProperty>> expression)
        {
            var expressionBody = expression.Body as MemberExpression;
            return GetFailure(instance, expressionBody.Member.Name);
        }

        protected ValidationFailure GetFailure(T instance, string propertyName)
        {
            var result = GetResult(instance);
            return GetFailure(result, propertyName);
        }

        protected void AssertIsValid(T instance)
        {
            var result = GetResult(instance);
            var errors = string.Join(
                "\r\n",
                result.Errors
                    .Select(
                        vf => string.Format("{0}: {1}", vf.PropertyName, vf.ErrorMessage))
                    .ToArray());
            Assert.That(result.IsValid, Is.True, errors);
        }
       
    }

}
