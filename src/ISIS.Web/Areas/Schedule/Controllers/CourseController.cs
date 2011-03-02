using System;
using System.Web.Mvc;
using ACC.Web;
using Ncqrs;
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
        public ViewResult Index(int? pageNumber)
        {
            return View(_repository.All<CourseList>(25, pageNumber ?? 1,
                                                    course => course.Rubric,
                                                    course => course.Number));
        }

        [HttpGet, View]
        public ViewResult Details(Guid id)
        {
            return View(_repository.Single<CourseDetails>(id));
        }

        [HttpGet, View]
        public ViewResult Add()
        {
            var identifierGenerator = NcqrsEnvironment.Get<IUniqueIdentifierGenerator>();
            var courseId = identifierGenerator.GenerateNewId();
            var cmd = new CreateCourseCommand()
                          {
                              CourseId = courseId
                          };
            return View(cmd);
        }

        [HttpPost, Command]
        public RedirectToRouteResult Add(CreateCourseCommand command)
        {
            if (!ModelState.IsValid)
                return this.RedirectToAction(c => c.Add());

            _commandService.Execute(command);
            return this.RedirectToAction(c => c.Details(command.CourseId));
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
            return View(new ChangeCIPCommand()
            {
                CourseId = id,
                CIP = course.CIP
            });
        }

        [HttpPost, Command]
        public RedirectToRouteResult AssignCIP(ChangeCIPCommand command)
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
            return View(new ChangeApprovalNumberCommand()
            {
                CourseId = id,
                ApprovalNumber = course.ApprovalNumber
            });
        }

        [HttpPost, Command]
        public RedirectToRouteResult AssignApprovalNumber(ChangeApprovalNumberCommand command)
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

        [HttpGet, View]
        public ViewResult ChangeDescription(Guid id)
        {
            var course = _repository.Single<CourseDetails>(id);
            return View(new ChangeCourseDescriptionCommand()
            {
                CourseId = id,
                NewDescription = course.Description
            });
        }

        [HttpPost, Command]
        public RedirectToRouteResult ChangeDescription(ChangeCourseDescriptionCommand command)
        {
            if (!ModelState.IsValid)
                return this.RedirectToAction(c => c.ChangeDescription(command.CourseId));

            _commandService.Execute(command);
            return this.RedirectToAction(c => c.Details(command.CourseId));
        }

        [HttpGet, View]
        public ViewResult Activate(Guid id)
        {
            return View(new ActivateCourseCommand()
            {
                CourseId = id
            });
        }

        [HttpPost, Command]
        public RedirectToRouteResult Activate(ActivateCourseCommand command)
        {
            if (!ModelState.IsValid)
                return this.RedirectToAction(c => c.Activate(command.CourseId));
            _commandService.Execute(command);
            return this.RedirectToAction(c => c.Details(command.CourseId));
        }

        [HttpGet, View]
        public ViewResult Deactivate(Guid id)
        {
            return View(new DeactivateCourseCommand()
            {
                CourseId = id
            });
        }

        [HttpPost, Command]
        public RedirectToRouteResult Deactivate(DeactivateCourseCommand command)
        {
            if (!ModelState.IsValid)
                return this.RedirectToAction(c => c.Deactivate(command.CourseId));
            _commandService.Execute(command);
            return this.RedirectToAction(c => c.Details(command.CourseId));
        }

        [HttpGet, View]
        public ViewResult MakePending(Guid id)
        {
            return View(new MakeCoursePendingCommand()
            {
                CourseId = id
            });
        }

        [HttpPost, Command]
        public RedirectToRouteResult MakePending(MakeCoursePendingCommand command)
        {
            if (!ModelState.IsValid)
                return this.RedirectToAction(c => c.MakePending(command.CourseId));
            _commandService.Execute(command);
            return this.RedirectToAction(c => c.Details(command.CourseId));
        }

    }
}
