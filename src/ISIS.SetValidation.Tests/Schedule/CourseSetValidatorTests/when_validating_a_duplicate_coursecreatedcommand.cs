using System;
using System.Collections.Generic;
using Ncqrs.Spec;

namespace ISIS.Schedule.CourseSetValidatorTests
{
    [Specification]
    public class when_validating_a_duplicate_coursecreatedcommand 
        : SetValidationFailureFixture<CreateCreditCourseCommand, CourseSet>
    {

        public Guid CourseId = Guid.NewGuid();
        public string Rubric = "BIOL";
        public string Number = "1301";
        
        protected override CreateCreditCourseCommand WhenExecuting()
        {
            return new CreateCreditCourseCommand()
                       {
                           CourseId = CourseId,
                           Rubric = Rubric,
                           CourseNumber = Number,
                           Title = "Cuttin' up frogs"
                       };
        }

        protected override IEnumerable<CourseSet> GivenQueryResults()
        {
            yield return new CourseSet()
                             {
                                 CourseId = CourseId,
                                 Rubric = Rubric,
                                 Number = Number
                             };
        }

    }
}
