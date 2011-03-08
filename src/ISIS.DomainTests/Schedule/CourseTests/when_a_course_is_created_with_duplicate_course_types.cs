using System;
using System.Collections.Generic;
using System.Linq;
using Ncqrs.Spec;
using NUnit.Framework;

namespace ISIS.Schedule.CourseTests
{
    [Specification]
    public class when_a_course_is_created_with_duplicate_course_types : 
        DomainFixture<CreateCreditCourseCommand>
    {

        private Guid CourseId = Guid.NewGuid();
        private const string Rubric = "BIOL";
        private const string CourseNumber = "1301";
        private const string Title = "Introductory Biology";

        protected override IEnumerable<object> GivenEvents()
        {
            return new object[0];
        }

        protected override CreateCreditCourseCommand WhenExecuting()
        {
            return new CreateCreditCourseCommand()
                       {
                           CourseId = CourseId,
                           Rubric = Rubric,
                           CourseNumber = CourseNumber,
                           Title = Title,
                           Types = new[] {CourseTypes.ACAD, CourseTypes.ACAD}
                       };
        }

        [Then]
        public void it_should_not_throw()
        {
            Assert.That(CaughtException, Is.Null);
        }


        [Then]
        public void then_it_should_create_only_one_CourseTypeAddedEvents()
        {
            var events = PublishedEvents.Select(pe => pe.Payload)
                .OfType<CourseTypeAddedToCourseEvent>();
            Assert.That(events.Count(), Is.EqualTo(1));
        }

    }
}
