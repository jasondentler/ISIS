using System.Linq;

namespace ISIS.Schedule
{
    public class CourseSetValidator
        : ISetValidator<CreateCourseCommand>
    {

        private const string Message = "Your attempt to create the course {0} {1} failed because the course already exists.";
        
        private readonly IReadRepository _repository;

        public CourseSetValidator(IReadRepository repository)
        {
            _repository = repository;
        }

        public void Validate(CreateCourseCommand command)
        {
            var query = new LookupCourseSet(command.Rubric, command.CourseNumber);
            var results = _repository.Execute(query);
            if (results.Any())
                throw new SetValidationException(Message, command.Rubric, command.CourseNumber);
        }

    }
}
