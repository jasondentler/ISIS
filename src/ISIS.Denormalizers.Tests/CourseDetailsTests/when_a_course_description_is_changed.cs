using System.Collections.Generic;
using Ninject;
using NUnit.Framework;

namespace ISIS.Denormalizers.Tests.CourseDetailsTests
{
    [TestFixture]
    public class when_a_course_description_is_changed
        : DenormalizerFixture<CourseDetailsDenormalizer, CourseDescriptionChangedEvent>
    {

        private const string Rubric = "MATH";
        private const string CourseNumber = "1314";
        private const string Description = "This course includes a review of the fundamental concepts of intermediate algebra, followed by a more intensive study of algebraic equations and inequalities, functions and graphs, graphs and zeros of polynomial functions, rational functions, exponential and logarithmic functions, systems of equations, matrices and the binomial theorem.  Graphing calculators (TI-83, TI-84 or comparable models) are required.  Students enrolling in this course must meet the college algebra standard on the placement test or have passed MATH 0312 with a grade of A, B, or C.  (3 lecture hours per week). Prerequisite: READ 0310 with a C or better or the TSI standard in Reading";


        protected override CourseDetailsDenormalizer CreateDenormalizer()
        {
            return Kernel.Get<CourseDetailsDenormalizer>();
        }

        protected override IEnumerable<object> Given()
        {
            yield return new CourseCreatedEvent(EventSourceId, Rubric, CourseNumber);
        }

        protected override CourseDescriptionChangedEvent WhenHandling()
        {
            return new CourseDescriptionChangedEvent(EventSourceId, Description);
        }

        [Test]
        public void it_updated_a_row()
        {
            var row = Repository.Single<CourseDetails>(EventSourceId);
            var e = TheEvent;

            Assert.That(row.Description, Is.EqualTo(Description));
        }

    }
}
