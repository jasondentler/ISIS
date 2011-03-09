using System.Web.Mvc;

namespace ISIS.Web.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public RedirectToRouteResult Index()
        {
            return RedirectToRoute(new
                                       {
                                           area = "Schedule",
                                           controller = "Course",
                                           action = "Index"
                                       });
        }

    }
}
