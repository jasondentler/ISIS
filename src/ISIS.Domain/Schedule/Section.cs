using System;
using Ncqrs.Domain;

namespace ISIS.Schedule
{
    public class Section : AggregateRootMappedByConvention
    {
        private Guid _topicCodeId;

        [Inject]
        private Section()
        {
        }

        public Section(Guid sectionId, Term term, Course course, string sectionNumber)
            : base(sectionId)
        {
            var termData = term.BuildMememto();
            var courseData = course.BuildMemento();

            if (string.IsNullOrEmpty(courseData.ApprovalNumber) &&
                string.IsNullOrEmpty(courseData.CIP))
                throw new InvalidStateException(
                    "Your attempt to create the section failed. Set approval number or CIP at the course level first.");

            ApplyEvent(new SectionCreatedEvent(
                sectionId, 
                courseData.Id,
                courseData.Rubric,
                courseData.CourseNumber,
                termData.Id,
                termData.Abbreviation,
                termData.Name,
                sectionNumber,
                termData.Start,
                termData.End,
                courseData.Title,
                courseData.CourseTypes,
                courseData.ApprovalNumber,
                courseData.CIP));
        }

        protected void OnCreated(SectionCreatedEvent @event)
        {
        }

        public void ChangeLocation(Location location, TopicCode tdcjTopicCode)
        {

            if (tdcjTopicCode == null)
                throw new InvalidStateException("You did not supply the TDCJ topic code.");

            if (tdcjTopicCode.BuildMemento().Abbreviation != "A")
                throw new InvalidStateException(
                    "You supplied the wrong TDCJ topic code. The TDCJ topic code abbreviation is \"A\"");

            var locationData = location.BuildMemento();

            ApplyEvent(new SectionLocationChangedEvent(
                           EventSourceId,
                           locationData.LocationId,
                           locationData.Abbreviation,
                           locationData.Name));

            switch (locationData.Abbreviation)
            {
                case "CLEM":
                case "CV":
                case "DAR":
                case "J1":
                case "J2":
                case "J3":
                case "R1":
                case "R2":
                case "TER":
                    ChangeTopicCode(tdcjTopicCode);
                    break;
                default:
                    ChangeTopicCode(null);
                    break;
            }

        }

        protected void OnLocationChanged(SectionLocationChangedEvent @event)
        {
        }

        public void ChangeTopicCode(TopicCode topicCode)
        {

            if (_topicCodeId == default(Guid) && topicCode == null)
                return;

            if (topicCode == null)
            {
                ApplyEvent(new SectionTopicCodeRemovedEvent(EventSourceId));
                return;
            }

            var topicCodeData = topicCode.BuildMemento();

            if (_topicCodeId == topicCodeData.Id)
                return;

            ApplyEvent(new SectionTopicCodeChangedEvent(
                           EventSourceId,
                           topicCodeData.Id,
                           topicCodeData.Abbreviation,
                           topicCodeData.Description));
        }

        protected void OnTopicCodeRemoved(SectionTopicCodeRemovedEvent @event)
        {
            _topicCodeId = default(Guid);
        }

        protected void OnTopicCodeChanged(SectionTopicCodeChangedEvent @event)
        {
            _topicCodeId = @event.TopicCodeId;
        }


    }
}
