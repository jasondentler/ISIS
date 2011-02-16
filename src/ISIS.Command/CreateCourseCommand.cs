using Ncqrs.Commanding;

namespace ISIS
{

    public class CreateCourseCommand : CommandBase 
    {

        public string Rubric { get; set; }
        public string CourseNumber { get; set; }
        public string Title { get; set; }

    }

}
