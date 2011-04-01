using System;
using System.ComponentModel.DataAnnotations;
using Ncqrs.Commanding;

namespace ISIS.Schedule
{
    public class CreateContinuingEducationCourseCommand : CommandBase
    {

        public Guid CourseId { get; set; }

        public string Rubric { get; set; }

        [Display(Name = "Course Number")]
        public string CourseNumber { get; set; }

        public string Title { get; set; }

        public CreditTypes Type { get; set; }

        public DateTime? EffectiveDate { get; set; }
        
    }
}
