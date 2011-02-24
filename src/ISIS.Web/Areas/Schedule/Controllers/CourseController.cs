using System;
using System.Linq;
using System.Web.Mvc;
using ACC.Web;
using Ncqrs.Commanding.ServiceModel;
using MvcContrib;

namespace ISIS.Web.Areas.Schedule.Controllers
{
    public class CourseController : Controller
    {

        public const string Message = "Message";

        private readonly IReadRepository _repository;
        private readonly ICommandService _commandService;

        public CourseController(IReadRepository repository, ICommandService commandService)
        {
            _repository = repository;
            _commandService = commandService;
        }

        [HttpGet, View]
        public ViewResult Index(int pageNumber = 1)
        {
            return View(_repository.All<CourseList>(pageNumber, 20,
                                                    course => course.Rubric,
                                                    course => course.Number));
        }

        [HttpGet, View]
        public ViewResult Details(Guid id)
        {
            return View(_repository.Single<CourseDetails>(id));
        }

        [HttpGet]
        public RedirectToRouteResult LookupDetails(string rubric, string courseNumber)
        {
            var query = new LookupCourseQuery(rubric, courseNumber);
            var results = _repository.Execute(query);
            var resultCount = results.LongCount();

            switch (resultCount)
            {
                case 0:
                    TempData[Message] = "Unable to lookup course by rubric and course number. No courses found.";
                    return this.RedirectToAction(c => c.Index(1));
                case 1:
                    var course = results.Single();
                    return this.RedirectToAction(c => c.Details(course.CourseId));
                default:
                    TempData[Message] =
                        string.Format("Unable to lookup course by rubric and course number. {0} courses found.",
                                      resultCount);
                    return this.RedirectToAction(c => c.Index(1));
            }

        }

        [HttpGet, View]
        public ViewResult Add()
        {
            return View();
        }

        [HttpPost, Command]
        public RedirectToRouteResult Add(CreateCourseCommand command)
        {
            if (!ModelState.IsValid)
                return this.RedirectToAction(c => c.Add());

            _commandService.Execute(command);
            return this.RedirectToAction(c => c.LookupDetails(command.Rubric, command.CourseNumber));
        }

        [HttpGet, View]
        public ViewResult ChangeTitle(Guid id)
        {
            var course = _repository.Single<CourseDetails>(id);
            return View(new ChangeCourseTitleCommand()
            {
                CourseId = id,
                NewTitle = course.Title
            });
        }

        [HttpPost, Command]
        public RedirectToRouteResult ChangeTitle(ChangeCourseTitleCommand command)
        {
            if (!ModelState.IsValid)
                return this.RedirectToAction(c => c.ChangeTitle(command.CourseId));

            _commandService.Execute(command);
            return this.RedirectToAction(c => c.Details(command.CourseId));
        }

        [HttpGet, View]
        public ViewResult AssignCIP(Guid id)
        {
            var course = _repository.Single<CourseDetails>(id);
            return View(new AssignCIPCommand()
            {
                CourseId = id,
                CIP = course.CIP
            });
        }

        [HttpPost, Command]
        public RedirectToRouteResult AssignCIP(AssignCIPCommand command)
        {
            if (!ModelState.IsValid)
                return this.RedirectToAction(c => c.AssignCIP(command.CourseId));

            _commandService.Execute(command);
            return this.RedirectToAction(c => c.Details(command.CourseId));
        }

        [HttpGet, View]
        public ViewResult AssignApprovalNumber(Guid id)
        {
            var course = _repository.Single<CourseDetails>(id);
            return View(new AssignApprovalNumberCommand()
            {
                CourseId = id,
                ApprovalNumber = course.ApprovalNumber
            });
        }

        [HttpPost, Command]
        public RedirectToRouteResult AssignApprovalNumber(AssignApprovalNumberCommand command)
        {
            if (!ModelState.IsValid)
                return this.RedirectToAction(c => c.AssignApprovalNumber(command.CourseId));

            _commandService.Execute(command);
            return this.RedirectToAction(c => c.Details(command.CourseId));
        }

        [HttpGet, View]
        public ViewResult ChangeLongTitle(Guid id)
        {
            var course = _repository.Single<CourseDetails>(id);
            return View(new ChangeCourseLongTitleCommand()
                            {
                                CourseId = id,
                                NewLongTitle = course.LongTitle
                            });
        }

        [HttpPost, Command]
        public RedirectToRouteResult ChangeLongTitle(ChangeCourseLongTitleCommand command)
        {
            if (!ModelState.IsValid)
                return this.RedirectToAction(c => c.ChangeLongTitle(command.CourseId));

            _commandService.Execute(command);
            return this.RedirectToAction(c => c.Details(command.CourseId));
        }



    }
}
