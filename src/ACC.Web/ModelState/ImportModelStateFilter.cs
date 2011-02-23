using System.Web.Mvc;

namespace ACC.Web.ModelState
{
    public class ImportModelStateFilter : BaseActionFilter
    {
        private static readonly string MODEL_STATE_KEY
            = ExportModelStateFilter.MODEL_STATE_KEY;

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var modelState =
                filterContext.Controller.TempData[MODEL_STATE_KEY]
                as ModelStateDictionary;

            if (modelState != null)
            {
                if (filterContext.Result is ViewResult)
                {
                    filterContext.Controller.ViewData.ModelState.Merge(modelState);
                }
                else
                {
                    filterContext.Controller.TempData.Remove(MODEL_STATE_KEY);
                }
            }
        }
    }
}