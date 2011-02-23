using System.Web.Mvc;

namespace ACC.Web.ModelState
{
    public class ExportModelStateFilter : BaseActionFilter
    {
        internal const string MODEL_STATE_KEY = "ModelStateKey)@#(*";

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (!filterContext.Controller.ViewData.ModelState.IsValid)
                if ((filterContext.Result is RedirectResult) ||
                    (filterContext.Result is RedirectToRouteResult))
                {
                    filterContext.Controller.TempData[MODEL_STATE_KEY] =
                        filterContext.Controller.ViewData.ModelState;
                }
        }
    }
}