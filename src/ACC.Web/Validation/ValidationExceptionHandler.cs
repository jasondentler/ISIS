using System.Web.Mvc;
using FluentValidation;

namespace ACC.Web.Validation
{
    public class ValidationExceptionHandler
    {

        public void AddToModel(ActionExecutedContext filter, ValidationException exception)
        {
            AddToModel(filter.Controller.ViewData.ModelState, exception);
        }

        public void AddToModel(ModelStateDictionary modelState, ValidationException exception)
        {
            foreach (var error in exception.Errors)
                modelState.AddModelError(error.PropertyName, error.ErrorMessage);
        }

    }
}
