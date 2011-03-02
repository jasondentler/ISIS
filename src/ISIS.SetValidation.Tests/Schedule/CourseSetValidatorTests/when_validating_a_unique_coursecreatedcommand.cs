using System;
using System.Collections.Generic;
using Ncqrs.Spec;

namespace ISIS.Schedule.CourseSetValidatorTests
{
    [Specification]
    public class when_validating_a_unique_coursecreatedcommand 
        : SetValidationSuccessFixture<CreateCourseCommand, CourseSet>
    {

        protected override IEnumerable<CourseSet> GivenQueryResults()
        {
            return new CourseSet[0];
        }

        protected override CreateCourseCommand WhenExecuting()
        {
            return new CreateCourseCommand()
                       {
                           CourseId = Guid.NewGuid(),
                           Rubric = "BIOL",
                           CourseNumber = "1301",
                           Title = "Cuttin' up frogs"
                       };
        }
        
    }
}
