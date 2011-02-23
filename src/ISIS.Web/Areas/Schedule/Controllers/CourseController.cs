using System;
using System.Web.Mvc;
using Ncqrs;
using Ncqrs.Commanding.ServiceModel;
using MvcContrib;

namespace ISIS.Web.Areas.Schedule.Controllers
{
    public class CourseController : Controller
    {

        private readonly IReadRepository _repository;
        private readonly ICommandService _commandService;

        public CourseController(IReadRepository repository, ICommandService commandService)
        {
            _repository = repository;
            _commandService = commandService;
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
        public ViewResult Details(Guid id)
        {
            return View(_repository.Single<CourseDetails>(id));
        }

        [HttpGet]
        public ViewResult Add()
        {
            return View();
        }

        [HttpPost]
        public RedirectToRouteResult Add(CreateCourseCommand command)
        {
            _commandService.Execute(command);
            return this.RedirectToAction(c => c.Index(1));
        }

        [HttpGet]
        public ViewResult ChangeTitle(Guid id)
        {
            var course = _repository.Single<CourseDetails>(id);
            return View(new ChangeCourseTitleCommand()
                            {
                                CourseId = id,
                                NewTitle = course.Title
                            });
        }

        [HttpPost]
        public RedirectToRouteResult ChangeTitle(ChangeCourseTitleCommand command)
        {
            _commandService.Execute(command);
            return this.RedirectToAction(c => c.Details(command.CourseId));
        }

    }
}
