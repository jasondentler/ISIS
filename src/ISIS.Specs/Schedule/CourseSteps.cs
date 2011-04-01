using System;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace ISIS.Schedule
{
    [Binding]
    public class CourseSteps 
    {

        [Given(@"I have created an (.*) course ([A-Z]{4}) (\d{1}[1-9]\d{2}) ""(.*)""")]
        [Given(@"I have created a (.*) course ([A-Z]{4}) (\d{1}[1-9]\d{2}) ""(.*)""")]
        public void GivenIHaveCreatedACreditCourse(
            string courseTypeString,
            string rubric,
            string number,
            string title)
        {
            var id = Guid.NewGuid();
            DomainHelper.SetId<Course>(id, rubric, number);

            DomainHelper.GivenEvent(id, new CreditCourseCreatedEvent(
                                        id,
                                        rubric,
                                        number));
            DomainHelper.GivenEvent(id, new CourseTitleChangedEvent(
                                        id,
                                        title));
            DomainHelper.GivenEvent(id, new CourseLongTitleChangedEvent(
                                        id,
                                        title));
            var courseTypes = CourseTypeSteps.ParseCourseTypes(courseTypeString);
            foreach (var courseType in courseTypes)
                DomainHelper.GivenEvent(id, new CourseTypeAddedToCourseEvent(
                                            id,
                                            courseType,
                                            courseTypes));
        }

        [Given(@"I have created a (.*) course ([A-Z]{4}) (\d{1}0\d{2}) ""(.*)""")]
        public void GivenIHaveCreatedContinuingEducationCourse(
            string creditTypeString,
            string rubric,
            string number,
            string title)
        {
            var id = Guid.NewGuid();
            DomainHelper.SetId<Course>(id, rubric, number);

            var creditType = CourseCreditTypeSteps.ParseCreditType(creditTypeString);

            DomainHelper.GivenEvent(id, new ContinuingEducationCourseCreatedEvent(
                                        id,
                                        rubric,
                                        number));
            DomainHelper.GivenEvent(id, new CourseTitleChangedEvent(
                                        id,
                                        title));
            DomainHelper.GivenEvent(id, new CourseLongTitleChangedEvent(
                                        id,
                                        title));
            DomainHelper.GivenEvent(id, new CourseCreditTypeChangedEvent(
                                        id,
                                        creditType));

            var courseType = CourseTypes.CE;
            switch (creditType)
            {
                case CreditTypes.ContractTrainingFunded:
                case CreditTypes.GrantFunded:
                case CreditTypes.WorkforceFunded:
                    courseType = CourseTypes.CWECM;
                    break;
            }
            DomainHelper.GivenEvent(id, new CourseTypeAddedToCourseEvent(
                                        id,
                                        courseType,
                                        new[] {courseType}));
        }
        
        [When(@"I create an (.*) course ([A-Z]{4}) (\d{1}[1-9]\d{2}) ""(.*)""")]
        [When(@"I create a (.*) course ([A-Z]{4}) (\d{1}[1-9]\d{2}) ""(.*)""")]
        public void WhenICreateACreditCourse(
            string courseTypeString,
            string rubric,
            string number,
            string title)
        {
            var id = Guid.NewGuid();
            DomainHelper.SetId<Course>(id, rubric, number);

            var courseTypes = CourseTypeSteps.ParseCourseTypes(courseTypeString);
            var cmd = new CreateCreditCourseCommand()
                          {
                              CourseId = id,
                              Rubric = rubric,
                              CourseNumber = number,
                              Title = title,
                              Types = courseTypes
                          };
            DomainHelper.WhenExecuting(cmd);
        }

        [When(@"I create a course ([A-Z]{4}) (\d{4}) ""(.*)"" without a course type")]
        public void WhenICreateACourseWithoutACourseType(
            string rubric,
            string number,
            string title)
        {
            var id = Guid.NewGuid();
            DomainHelper.SetId<Course>(id, rubric, number);

            var cmd = new CreateCreditCourseCommand()
                          {
                              CourseId = id,
                              Rubric = rubric,
                              CourseNumber = number,
                              Title = title,
                              Types = new CourseTypes[0]
                          };
            DomainHelper.WhenExecuting(cmd);
        }

        [When(@"I create a (.*) course ([A-Z]{4}) (\d{1}0\d{2}) ""(.*)""")]
        public void WhenICreateAContinuingEducationCourse(
            string creditTypeString,
            string rubric,
            string number,
            string title)
        {
            var id = Guid.NewGuid();
            DomainHelper.SetId<Course>(id, rubric, number);

            var creditType = CourseCreditTypeSteps.ParseCreditType(creditTypeString);
            var cmd = new CreateContinuingEducationCourseCommand()
                          {
                              CourseId = id,
                              Rubric = rubric,
                              CourseNumber = number,
                              Title = title,
                              Type = creditType
                          };
            DomainHelper.WhenExecuting(cmd);
        }

        [When(@"I create a backdated (.*) course ([A-Z]{4}) (\d{1}0\d{2}) ""(.*)"" on (\d{1,2}/\d{1,2}/\d{4})")]
        public void WhenICreateABackdatedCourse(
            string creditTypeString,
            string rubric,
            string number,
            string title,
            string effectiveDateString)
        {
            var id = Guid.NewGuid();
            DomainHelper.SetId<Course>(id, rubric, number);

            var creditType = CourseCreditTypeSteps.ParseCreditType(creditTypeString);
            var effectiveDate = DateTime.Parse(effectiveDateString);
            var cmd = new CreateContinuingEducationCourseCommand()
                          {
                              CourseId = id,
                              Rubric = rubric,
                              CourseNumber = number,
                              Title = title,
                              Type = creditType,
                              EffectiveDate = effectiveDate
                          };
            DomainHelper.WhenExecuting(cmd);
        }

        [When(@"I create a (.*) course ([A-Z]{4}) (\d{1}0\d{2}) ""(.*)"" approved by (.*)")]
        public void WhenICreateACourseApprovedBy(
            string creditTypeString,
            string rubric,
            string number,
            string title,
            string approvedBy)
        {
            var id = Guid.NewGuid();
            DomainHelper.SetId<Course>(id, rubric, number);

            var creditType = CourseCreditTypeSteps.ParseCreditType(creditTypeString);
            var cmd = new CreateContinuingEducationCourseCommand()
            {
                CourseId = id,
                Rubric = rubric,
                CourseNumber = number,
                Title = title,
                Type = creditType,
                ApprovedBy = approvedBy
            };
            DomainHelper.WhenExecuting(cmd);
        }


        [Then(@"the credit course is created")]
        public void ThenTheCreditCourseShouldBeCreated()
        {
            var cmd = DomainHelper.GetCommand();
            var e = DomainHelper.GetEvent<CreditCourseCreatedEvent>();
            Assert.That(e.CourseId, Is.EqualTo(cmd.CourseId));
        }

        [Then(@"the CE course is created")]
        public void ThenTheCECourseShouldBeCreated()
        {
            var cmd = DomainHelper.GetCommand();
            var e = DomainHelper.GetEvent<ContinuingEducationCourseCreatedEvent>();
            Assert.That(e.CourseId, Is.EqualTo(cmd.CourseId));
        }

        [Then(@"the course rubric is (.*)")]
        public void ThenTheCourseRubricShouldBe(string rubric)
        {
            var e = (dynamic) DomainHelper.GetEvent<CreditCourseCreatedEvent>()
                        ?? DomainHelper.GetEvent<ContinuingEducationCourseCreatedEvent>();
            Assert.That(e.Rubric, Is.EqualTo(rubric));
        }

        [Then(@"the course number is (.*)")]
        public void ThenTheCourseNumberShouldBe(string number)
        {
            var e = (dynamic)DomainHelper.GetEvent<CreditCourseCreatedEvent>()
                        ?? DomainHelper.GetEvent<ContinuingEducationCourseCreatedEvent>();
            Assert.That(e.Number, Is.EqualTo(number));
        }

        [Then(@"the approval person is (.*)")]
        public void ThenTheApprovalPersonIs(
            string approvalPerson)
        {
            var e = DomainHelper.GetEvent<CourseApprovedEvent>();
            Assert.That(e.ApprovedBy, Is.EqualTo(approvalPerson));
        }


    }
}
