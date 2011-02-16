using System.Web.Mvc;
using Ncqrs;
using Ncqrs.Commanding.ServiceModel;
using MvcContrib;

namespace ISIS.Web.Areas.Schedule.Controllers
{
    public class CourseController : Controller
    {
        //
        // GET: /Schedule/Course/

        public ViewResult Index()
        {
            var courseList = new CourseList();
            return View(courseList.All(orderBy: "Rubric, Number, Title", limit: 200));
        }

        [HttpGet]
        public ViewResult Add()
        {
            return View(new CreateCourseCommand());
        }

        [HttpPost]
        public RedirectToRouteResult Add(CreateCourseCommand command)
        {
            NcqrsEnvironment.Get<ICommandService>().Execute(command);
            return this.RedirectToAction(c => c.Index());
        }

    }
}
