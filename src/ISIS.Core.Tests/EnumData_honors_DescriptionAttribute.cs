using ISIS.Schedule;
using Ncqrs.Spec;
using NUnit.Framework;

namespace ISIS
{
    [Specification]
    public class EnumData_honors_DescriptionAttribute : BaseTestFixture
    {

        private CourseTypes courseType = CourseTypes.ACAD;
        private string expected = "General Academic";
        private string actual;

        protected override void When()
        {
            actual = EnumData.GetNameForValue(typeof (CourseTypes), courseType);
        }

        [Then]
        public void it_should_not_throw()
        {
            Assert.That(CaughtException, Is.Null);
        }

        [Then]
        public void it_should_return_the_correct_string()
        {
            Assert.That(actual, Is.EqualTo(expected));
        }
        
    }
}
