using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Ncqrs.Commanding;

namespace ISIS.Schedule
{

    public class CreateCreditCourseCommand : CommandBase 
    {
        
        public Guid CourseId { get; set; }

        public string Rubric { get; set; }

        [Display(Name = "Course Number")]
        public string CourseNumber { get; set; }

        public string Title { get; set; }

        public IEnumerable<CourseTypes> Types { get; set; }

    }

}
