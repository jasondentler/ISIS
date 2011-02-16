using System.Web.Mvc;
using ISIS.Web.Areas.Schedule.Controllers;
using MvcContrib;

namespace ISIS.Web.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public RedirectToRouteResult Index()
        {
            return this.RedirectToAction<CourseController>(
                c => c.Index());
        }

    }
}
