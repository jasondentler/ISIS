using System;
using System.Collections.Generic;
using Ncqrs.Spec;

namespace ISIS.Schedule.CourseSetValidatorTests
{
    [Specification]
    public class when_validating_a_unique_coursecreatedcommand 
        : SetValidationSuccessFixture<CreateCreditCourseCommand, CourseSet>
    {

        protected override IEnumerable<CourseSet> GivenQueryResults()
        {
            return new CourseSet[0];
        }

        protected override CreateCreditCourseCommand WhenExecuting()
        {
            return new CreateCreditCourseCommand()
                       {
                           CourseId = Guid.NewGuid(),
                           Rubric = "BIOL",
                           CourseNumber = "1301",
                           Title = "Cuttin' up frogs",
                           Types = new[] {CourseTypes.ACAD}
                       };
        }
        
    }
}
