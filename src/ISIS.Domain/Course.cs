using Ncqrs.Domain;

namespace ISIS
{

    public class Course : AggregateRootMappedByConvention
    {

        private Course()
        {
        }

        public Course(string subject, string courseNumber, string title)
        {
            var e = new CourseCreatedEvent()
                        {
                            Subject = subject,
                            Number = courseNumber,
                            Title = title
                        };
            ApplyEvent(e);
        }


        protected void OnCourseCreated(CourseCreatedEvent @event)
        {
        }

    }

}
