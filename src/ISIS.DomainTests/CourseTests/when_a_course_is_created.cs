using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace ISIS.DomainTests.CourseTests
{
    [TestFixture]
    public class when_a_course_is_created : 
        DomainFixture<CreateCourseCommand>
    {

        private Guid CourseId = Guid.NewGuid();
        private const string Rubric = "BIOL";
        private const string CourseNumber = "1301";
        private const string Title = "Introductory Biology";


        protected override IEnumerable<object> GivenEvents()
        {
            return new object[0];
        }

        protected override CreateCourseCommand WhenExecuting()
        {
            return new CreateCourseCommand()
            {
                CourseId = CourseId,
                Rubric = Rubric,
                CourseNumber = CourseNumber,
                Title = Title
            };
        }

        [Test]
        public void it_should_do_no_more()
        {
            Assert.That(PublishedEvents.Count(), Is.EqualTo(4));
        }

        [Test]
        public void then_it_should_create_a_new_CourseCreatedEvent()
        {
            var TheEvent = PublishedEvents.Select(pe => pe.Payload)
                .OfType<CourseCreatedEvent>().Single();
            Assert.That(TheEvent.CourseId, Is.EqualTo(CourseId));
            Assert.That(TheEvent.Rubric, Is.EqualTo(Rubric));
            Assert.That(TheEvent.Number, Is.EqualTo(CourseNumber));
        }

        [Test]
        public void then_it_should_create_a_new_CourseTitleChangedEvent()
        {
            var TheEvent = PublishedEvents.Select(pe => pe.Payload)
                .OfType<CourseTitleChangedEvent>().Single();
            Assert.That(TheEvent.CourseId, Is.Not.EqualTo(default(Guid)));
            Assert.That(TheEvent.Title, Is.EqualTo(Title));
        }

        [Test]
        public void then_it_should_create_a_new_CourseLongTitleChangedEvent()
        {
            var TheEvent = PublishedEvents.Select(pe => pe.Payload)
                .OfType<CourseLongTitleChangedEvent>().Single();
            Assert.That(TheEvent.CourseId, Is.Not.EqualTo(default(Guid)));
            Assert.That(TheEvent.LongTitle, Is.EqualTo(Title));
        }

        [Test]
        public void then_it_should_create_a_new_CourseActivatedEvent()
        {
            var TheEvent = PublishedEvents.Select(pe => pe.Payload)
                .OfType<CourseActivatedEvent>().Single();
            Assert.That(TheEvent.CourseId, Is.Not.EqualTo(default(Guid)));
        }


    }
}
