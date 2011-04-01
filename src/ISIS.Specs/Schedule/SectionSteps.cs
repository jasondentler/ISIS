using System;
using TechTalk.SpecFlow;

namespace ISIS.Schedule
{
    [Binding]
    public class SectionSteps
    {

        [When(@"I create section ([^\s]+) from the course and term")]
        public void WhenICreateSection(
            string sectionNumber)
        {
            var courseId = DomainHelper.GetId<Course>();
            var termId = DomainHelper.GetId<Term>();

            var id = Guid.NewGuid();
            DomainHelper.SetId<Section>(id, termId.ToString(), courseId.ToString(), sectionNumber);

            var cmd = new CreateSectionCommand()
                          {
                              SectionId = id,
                              CourseId = courseId,
                              TermId = termId,
                              SectionNumber = sectionNumber
                          };
            DomainHelper.WhenExecuting(cmd);
        }


    }
}
