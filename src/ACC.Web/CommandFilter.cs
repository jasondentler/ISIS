using System;
using System.Web.Mvc;
using ACC.Web.ModelState;
using FluentValidation;
using FluentValidation.Results;
using ISIS;

namespace ACC.Web
{
    public class CommandFilter : BaseActionFilter
    {

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            HandleValidationException(filterContext);
            HandleSetValidationException(filterContext);
            HandleInvalidStateException(filterContext);
            ExportModelState(filterContext);
        }

        private void HandleSetValidationException(ActionExecutedContext filterContext)
        {
            var setException = filterContext.Exception as SetValidationException;
            if (setException == null)
                return;
            AddExceptionToModelErrors(filterContext, setException);

            RedirectToGet(filterContext);
            filterContext.ExceptionHandled = true;
        }

        private void HandleValidationException(ActionExecutedContext filterContext)
        {
            var validationException = filterContext.Exception as ValidationException;
            if (validationException == null)
                return;
            new Validation.ValidationExceptionHandler()
                .AddToModel(filterContext, validationException);
            RedirectToGet(filterContext);
            filterContext.ExceptionHandled = true;
        }

        private void HandleInvalidStateException(ActionExecutedContext filterContext)
        {
            var stateException = filterContext.Exception as InvalidStateException;
            if (stateException == null)
                return;
            AddExceptionToModelErrors(filterContext, stateException);
            RedirectToGet(filterContext);
            filterContext.ExceptionHandled = true;
        }

        private void RedirectToGet(ActionExecutedContext filterContext)
        {
            var redirect = new RedirectToRouteResult(filterContext.RequestContext.RouteData.Values);
            filterContext.Result = redirect;
        }

        private void ExportModelState(ActionExecutedContext filterContext)
        {
            var filter = new ExportModelStateFilter();
            filter.OnActionExecuted(filterContext);
        }

        private void AddExceptionToModelErrors(ActionExecutedContext filterContext, Exception exception)
        {
            var validationFailure = new ValidationFailure("", exception.Message);
            var validationException = new ValidationException(new[] { validationFailure });
            new Validation.ValidationExceptionHandler()
                .AddToModel(filterContext, validationException);
        }

    }
}
