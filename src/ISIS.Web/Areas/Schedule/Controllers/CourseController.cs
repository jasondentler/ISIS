using System;
using System.Web.Mvc;
using ACC.Web;
using ACC.Web.ModelState;
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

        [HttpGet, ImportModelState]
        public ViewResult Add()
        {
            return View();
        }

        [HttpPost, ExportModelState]
        public RedirectToRouteResult Add(CreateCourseCommand command)
        {
            if (!ModelState.IsValid)
                return this.RedirectToAction(c => c.Add());

            _commandService.Execute(command);
            return this.RedirectToAction(c => c.Index(1));
        }

        [HttpGet, ImportModelState]
        public ViewResult ChangeTitle(Guid id)
        {
            var course = _repository.Single<CourseDetails>(id);
            return View(new ChangeCourseTitleCommand()
            {
                CourseId = id,
                NewTitle = course.Title
            });
        }

        [HttpPost, ExportModelState]
        public RedirectToRouteResult ChangeTitle(ChangeCourseTitleCommand command)
        {
            if (!ModelState.IsValid)
                return this.RedirectToAction(c => c.ChangeTitle(command.CourseId));

            _commandService.Execute(command);
            return this.RedirectToAction(c => c.Details(command.CourseId));
        }

        [HttpGet, ImportModelState]
        public ViewResult AssignCIP(Guid id)
        {
            var course = _repository.Single<CourseDetails>(id);
            return View(new AssignCIPCommand()
            {
                CourseId = id,
                CIP = course.CIP
            });
        }

        [HttpPost, ExportModelState]
        public RedirectToRouteResult AssignCIP(AssignCIPCommand command)
        {
            if (!ModelState.IsValid)
                return this.RedirectToAction(c => c.AssignCIP(command.CourseId));

            _commandService.Execute(command);
            return this.RedirectToAction(c => c.Details(command.CourseId));
        }

        [HttpGet, ImportModelState]
        public ViewResult AssignApprovalNumber(Guid id)
        {
            var course = _repository.Single<CourseDetails>(id);
            return View(new AssignApprovalNumberCommand()
            {
                CourseId = id,
                ApprovalNumber = course.ApprovalNumber
            });
        }

        [HttpPost, Command, ExportModelState]
        public RedirectToRouteResult AssignApprovalNumber(AssignApprovalNumberCommand command)
        {
            if (!ModelState.IsValid)
                return this.RedirectToAction(c => c.AssignApprovalNumber(command.CourseId));

            _commandService.Execute(command);
            return this.RedirectToAction(c => c.Details(command.CourseId));
        }

    }
}
