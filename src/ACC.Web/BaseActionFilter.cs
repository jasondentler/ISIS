using System.Web.Mvc;

namespace ACC.Web
{
    public abstract class BaseActionFilter : IActionFilter, IResultFilter
    {
        #region IActionFilter Members

        public virtual void OnActionExecuted(ActionExecutedContext filterContext)
        {
        }

        public virtual void OnActionExecuting(ActionExecutingContext filterContext)
        {
        }

        #endregion

        #region IResultFilter Members

        public virtual void OnResultExecuted(ResultExecutedContext filterContext)
        {
        }

        public virtual void OnResultExecuting(ResultExecutingContext filterContext)
        {
        }

        #endregion
    }
}