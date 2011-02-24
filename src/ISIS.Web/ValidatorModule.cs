using System.Web.Mvc;
using FluentValidation;
using FluentValidation.Mvc;
using ISIS.Validation;
using Ninject;
using Ninject.Modules;


namespace ISIS.Web
{
    public class ValidatorModule
        : NinjectModule 
    {
        public override void Load()
        {
            Kernel.Bind<IValidatorFactory>().To<ValidatorFactory>().InSingletonScope();


            DataAnnotationsModelValidatorProvider
                .AddImplicitRequiredAttributeForValueTypes = false;
            ModelValidatorProviders.Providers.Clear();
            ModelValidatorProviders.Providers.Add(
                new FluentValidationModelValidatorProvider(Kernel.Get<IValidatorFactory>()));

            ModelMetadataProviders.Current = new FluentValidationModelMetadataProvider(
                Kernel.Get<IValidatorFactory>());

        }
    }
}