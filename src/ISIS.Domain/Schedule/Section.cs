using System;
using System.Collections.Generic;
using System.Linq;
using Ncqrs.Domain;

namespace ISIS.Schedule
{
    public class Section : AggregateRootMappedByConvention
    {
        private Guid _topicCodeId;
        private Guid _locationId;
        private ISet<CourseTypes> _courseTypes = new HashSet<CourseTypes>();

        [Inject]
        private Section()
        {
        }

        public Section(Guid sectionId, Term term, Course course, string sectionNumber)
            : base(sectionId)
        {
            var termData = term.BuildMememto();
            var courseData = course.BuildMemento();

            if (courseData.IsCredit &&
                string.IsNullOrEmpty(courseData.ApprovalNumber) &&
                string.IsNullOrEmpty(courseData.CIP))
                throw new InvalidStateException(
                    "Your attempt to create the section failed. Set approval number or CIP at the course level first.");

            if (!courseData.IsCredit &&
                courseData.CreditType != CreditTypes.SpecialInterests &&
                string.IsNullOrEmpty(courseData.ApprovalNumber) &&
                string.IsNullOrEmpty(courseData.CIP))
                throw new InvalidStateException(
                    "Your attempt to create a section failed. The course doesn't have an approval number or CIP, and it's not a special interests course.");

            ApplyEvent(new SectionCreatedEvent(
                           sectionId,
                           courseData.Id,
                           courseData.Rubric,
                           courseData.CourseNumber,
                           termData.Id,
                           termData.Abbreviation,
                           termData.Name,
                           sectionNumber));

            if (courseData.IsCredit)
                ApplyEvent(new SectionDatesChangedEvent(
                               sectionId,
                               termData.Start,
                               termData.End));

            ApplyEvent(new SectionTitleChangedEvent(
                           sectionId,
                           null,
                           courseData.Title));

            if (!string.IsNullOrEmpty(courseData.ApprovalNumber))
                ApplyEvent(new SectionApprovalNumberChangedEvent(
                               sectionId, courseData.ApprovalNumber));

            if (!string.IsNullOrEmpty(courseData.CIP))
                ApplyEvent(new SectionCIPChangedEvent(
                               sectionId, courseData.CIP));

            foreach (var courseType in courseData.CourseTypes)
                ApplyEvent(new SectionCourseTypeAddedEvent(
                               sectionId,
                               courseType,
                               _courseTypes.Union(new[] {courseType}).Distinct()));

            if (courseData.CreditType != default(CreditTypes))
                ApplyEvent(new SectionCreditTypeChangedEvent(
                               sectionId,
                               courseData.CreditType));

            ApplyEvent(new SectionMadePendingEvent(sectionId));

            if (!courseData.IsCredit)
                ApplyEvent(new SectionCEUsChangedEvent(
                               sectionId, courseData.CEUs));

            if (courseData.TopicCodeId != default(Guid))
            {
                var uow = UnitOfWorkContext.Current;
                var topicCode = uow.GetById<TopicCode>(courseData.TopicCodeId);
                var topicCodeData = topicCode.BuildMemento();
                ApplyEvent(new SectionTopicCodeChangedEvent(
                               sectionId,
                               topicCodeData.Id,
                               topicCodeData.Abbreviation,
                               topicCodeData.Description));
            }

        }

        public void ChangeLocation(Location location, TopicCode tdcjTopicCode)
        {

            if (tdcjTopicCode == null)
                throw new InvalidStateException("You did not supply the TDCJ topic code.");

            if (tdcjTopicCode.BuildMemento().Abbreviation != "A")
                throw new InvalidStateException(
                    "You supplied the wrong TDCJ topic code. The TDCJ topic code abbreviation is \"A\"");

            var locationData = location.BuildMemento();

            if (locationData.LocationId == _locationId)
                return;

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

        protected void OnCreated(SectionCreatedEvent @event)
        {
        }

        protected void OnDatesChanged(SectionDatesChangedEvent @event)
        {
        }

        protected void OnTitleChanged(SectionTitleChangedEvent @event)
        {
        }

        protected void OnApprovalNumberChanged(SectionApprovalNumberChangedEvent @event)
        {
        }

        protected void OnCIPChanged(SectionCIPChangedEvent @event)
        {
        }

        protected void OnCourseTypeAdded(SectionCourseTypeAddedEvent @event)
        {
            _courseTypes.Add(@event.CourseTypeAdded);
        }

        protected void OnCreditTypeChanged(SectionCreditTypeChangedEvent @event)
        {
        }

        protected void OnMadePending(SectionMadePendingEvent @event)
        {
        }

        protected void OnCEUsChanged(SectionCEUsChangedEvent @event)
        {
        }

        protected void OnTopicCodeRemoved(SectionTopicCodeRemovedEvent @event)
        {
            _topicCodeId = default(Guid);
        }

        protected void OnTopicCodeChanged(SectionTopicCodeChangedEvent @event)
        {
            _topicCodeId = @event.TopicCodeId;
        }

        protected void OnLocationChanged(SectionLocationChangedEvent @event)
        {
            _locationId = @event.LocationId;
        }



    }
}
