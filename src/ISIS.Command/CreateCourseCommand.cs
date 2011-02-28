using System;
using System.ComponentModel.DataAnnotations;
using Ncqrs.Commanding;

namespace ISIS
{

    public class CreateCourseCommand : CommandBase 
    {
        
        public Guid CourseId { get; set; }

        public string Rubric { get; set; }

        [Display(Name = "Course Number")]
        public string CourseNumber { get; set; }

        public string Title { get; set; }

    }

}
