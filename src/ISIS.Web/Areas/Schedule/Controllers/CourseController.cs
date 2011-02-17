using System.Web.Mvc;
using Ncqrs;
using Ncqrs.Commanding.ServiceModel;
using MvcContrib;

namespace ISIS.Web.Areas.Schedule.Controllers
{
    public class CourseController : Controller
    {

        private readonly IReadRepository _repository;

        public CourseController(IReadRepository repository)
        {
            _repository = repository;
        }

        //
        // GET: /Schedule/Course/

        public ViewResult Index(int pageNumber = 1)
        {
            return View(_repository.All<CourseList>(pageNumber, 20,
                                                    course => course.Rubric,
                                                    course => course.Number));
        }

        [HttpGet]
        public ViewResult Add()
        {
            return View();
        }

        [HttpPost]
        public RedirectToRouteResult Add(CreateCourseCommand command)
        {
            NcqrsEnvironment.Get<ICommandService>().Execute(command);
            return this.RedirectToAction(c => c.Index(1));
        }

    }
}
