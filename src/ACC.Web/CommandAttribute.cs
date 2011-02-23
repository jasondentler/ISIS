using System;
using System.Web.Mvc;

namespace ACC.Web
{

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class CommandAttribute : ActionFilterAttribute
    {

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var filter = new CommandFilter();
            filter.OnActionExecuted(filterContext);
        }

    }

}
