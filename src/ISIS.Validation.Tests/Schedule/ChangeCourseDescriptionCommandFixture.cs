using System;
using FluentValidation;
using NUnit.Framework;

namespace ISIS.Schedule
{
    [TestFixture]
    public class ChangeCourseDescriptionCommandFixture : ValidationFixture<ChangeCourseDescriptionCommand>
    {
        protected override AbstractValidator<ChangeCourseDescriptionCommand> CreateValidator()
        {
            return new ChangeCourseDescriptionCommandValidator();
        }

        protected override ChangeCourseDescriptionCommand GetValidInstance()
        {
            return new ChangeCourseDescriptionCommand()
                       {
                           CourseId = Guid.NewGuid(),
                           NewDescription =
                               @"This course includes a review of the fundamental concepts of intermediate algebra, followed by a more intensive study of algebraic equations and inequalities, functions and graphs, graphs and zeros of polynomial functions, rational functions, exponential and logarithmic functions, systems of equations, matrices and the binomial theorem. Graphing calculators (TI-83, TI-84 or comparable models) are required. Students enrolling in this course must meet the college algebra standard on the placement test or have passed MATH 0312 with a grade of A, B, or C. (3 lecture hours per week). Prerequisite: READ 0310 with a C or better or the TSI standard in Reading."
                       };
        }

        [Test]
        public void CourseId_must_be_specified()
        {
            GetFailure(new ChangeCourseDescriptionCommand()
            {
                CourseId = default(Guid)
            }, cmd => cmd.CourseId);
        }


        [Test]
        public void Description_cant_be_null()
        {
            GetFailure(new ChangeCourseDescriptionCommand()
            {
                NewDescription = null
            }, cmd => cmd.NewDescription);
        }

        [Test]
        public void Description_cant_be_empty()
        {
            GetFailure(new ChangeCourseDescriptionCommand()
            {
                NewDescription = ""
            }, cmd => cmd.NewDescription);
        }

    }
}
