using NUnit.Framework;
using TechTalk.SpecFlow;

namespace ISIS.Schedule
{
    [Binding]
    public class CourseCIPAndApprovalSteps
    {

        [Given(@"I have set the approval number to (\d{10})")]
        public void GivenIHaveSetTheApprovalNumberTo(string approvalNumber)
        {
            DomainHelper.GivenEvent<Course>(new CourseApprovalNumberChangedEvent(
                                        DomainHelper.GetEventSourceId<Course>(),
                                        approvalNumber));
            DomainHelper.GivenEvent<Course>(new CourseCIPChangedEvent(
                                        DomainHelper.GetEventSourceId<Course>(),
                                        approvalNumber.Substring(0, 6)));
        }


        [Given(@"I have set the CIP to (\d{6})")]
        public void GivenIHaveSetTheCIPTo(string cip)
        {
            DomainHelper.GivenEvent<Course>(new CourseCIPChangedEvent(
                                        DomainHelper.GetEventSourceId<Course>(),
                                        cip));
        }


        [When(@"I set the approval number to (\d{10})")]
        public void WhenISetTheApprovalNumberTo(string approvalNumber)
        {
            var cmd = new ChangeApprovalNumberCommand()
                          {
                              CourseId = DomainHelper.GetEventSourceId<Course>(),
                              ApprovalNumber = approvalNumber
                          };
            DomainHelper.WhenExecuting(cmd);
        }

        [When(@"I set the CIP to (\d{6})")]
        public void WhenISetTheCIPTo(string cip)
        {
            var cmd = new ChangeCIPCommand()
            {
                CourseId = DomainHelper.GetEventSourceId<Course>(),
                CIP = cip
            };
            DomainHelper.WhenExecuting(cmd);
        }

        [Then(@"the approval number is (\d{10})")]
        public void ThenTheApprovalNumberIs(string approvalNumber)
        {
            var e = DomainHelper.GetEvent<CourseApprovalNumberChangedEvent>();
            Assert.That(e.ApprovalNumber, Is.EqualTo(approvalNumber));
        }

        [Then(@"the CIP is (\d{6})")]
        public void ThenTheCIPIs(string cip)
        {
            var e = DomainHelper.GetEvent<CourseCIPChangedEvent>();
            Assert.That(e.CIP, Is.EqualTo(cip));
        }

        [Then(@"the approval number is blank")]
        public void ThenTheApprovalNumberIsBlank()
        {
            var e = DomainHelper.GetEvent<CourseApprovalNumberChangedEvent>();
            Assert.That(e.ApprovalNumber, Is.Null);
        }


    }
}
