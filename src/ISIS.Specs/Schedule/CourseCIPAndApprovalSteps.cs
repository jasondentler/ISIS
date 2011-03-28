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
            DomainHelper.GivenEvent(new CourseApprovalNumberChangedEvent(
                                        DomainHelper.GetEventSourceId(),
                                        approvalNumber));
            DomainHelper.GivenEvent(new CourseCIPChangedEvent(
                                        DomainHelper.GetEventSourceId(),
                                        approvalNumber.Substring(0, 6)));
        }


        [Given(@"I have set the CIP to (\d{6})")]
        public void GivenIHaveSetTheCIPTo(string cip)
        {
            DomainHelper.GivenEvent(new CourseCIPChangedEvent(
                                        DomainHelper.GetEventSourceId(),
                                        cip));
        }


        [When(@"I set the approval number to (\d{10})")]
        public void WhenISetTheApprovalNumberTo(string approvalNumber)
        {
            var cmd = new ChangeApprovalNumberCommand()
                          {
                              CourseId = DomainHelper.GetEventSourceId(),
                              ApprovalNumber = approvalNumber
                          };
            DomainHelper.WhenExecuting(cmd);
        }

        [When(@"I set the CIP to (\d{6})")]
        public void WhenISetTheCIPTo(string cip)
        {
            var cmd = new ChangeCIPCommand()
            {
                CourseId = DomainHelper.GetEventSourceId(),
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
