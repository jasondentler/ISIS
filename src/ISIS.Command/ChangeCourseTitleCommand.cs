﻿using System;
using System.ComponentModel.DataAnnotations;
using Ncqrs.Commanding;

namespace ISIS
{
    public class ChangeCourseTitleCommand : CommandBase 
    {
        public Guid CourseId { get; set; }

        [Display(Name = "New Title")]
        public string NewTitle { get; set; }
    }
}