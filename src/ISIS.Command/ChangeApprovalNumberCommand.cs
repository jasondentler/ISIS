﻿using System;
using System.ComponentModel.DataAnnotations;
using Ncqrs.Commanding;

namespace ISIS
{
    public class ChangeApprovalNumberCommand : CommandBase 
    {

        public Guid CourseId { get; set; }

        [Display(Name = "Approval Number")]
        public string ApprovalNumber { get; set; }
    }
}