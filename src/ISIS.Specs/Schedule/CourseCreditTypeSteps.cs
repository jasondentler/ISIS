using System.Linq;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace ISIS.Schedule
{
    [Binding]
    public class CourseCreditTypeSteps
    {

        public static CreditTypes ParseCreditType(string creditTypeString)
        {
            var map = EnumData.GetEnumValues(typeof (CreditTypes));
            return (CreditTypes) map
                                     .Where(i => i.Value == creditTypeString)
                                     .Single()
                                     .Key;
        }

        [When(@"I change the credit type to (.*)")]
        public void WhenIChangeTheCreditTypeTo(
            string creditTypeString)
        {
            var creditType = ParseCreditType(creditTypeString);
            var cmd = new ChangeCourseCreditType()
                          {
                              CourseId = DomainHelper.GetEventSourceId(),
                              Type = creditType
                          };
            DomainHelper.WhenExecuting(cmd);
        }


        [Then(@"the credit type is (.*)")]
        public void ThenTheCreditTypeIs(string creditTypeString)
        {
            var creditType = ParseCreditType(creditTypeString);
            var e = DomainHelper.GetEvent<CourseCreditTypeChangedEvent>();
            Assert.That(e.CreditType, Is.EqualTo(creditType));
        }
    
    }
}
