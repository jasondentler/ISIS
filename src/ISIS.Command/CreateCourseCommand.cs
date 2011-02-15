using Ncqrs.Commanding;

namespace ISIS
{

    public class CreateCourseCommand : CommandBase 
    {

        public string Subject { get; set; }
        public string CourseNumber { get; set; }
        public string Title { get; set; }

    }

}
