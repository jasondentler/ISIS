using System;
using System.Web.Mvc;
using ACC.Web.ModelState;

namespace ACC.Web
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class ViewAttribute : ActionFilterAttribute
    {

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var filter = new ImportModelStateFilter();
            filter.OnActionExecuted(filterContext);
        }

    }

}
