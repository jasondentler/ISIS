using System;
using System.Linq;
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
            var ex = filterContext.Exception;
            if (ex == null)
                return;
            HandleValidationException(filterContext);
            HandleInvalidStateException(filterContext);
            ExportModelState(filterContext);
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
            var validationError = new ValidationException(new[] {new ValidationFailure("", stateException.Message)});
            new Validation.ValidationExceptionHandler()
                .AddToModel(filterContext, validationError);
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

    }
}
