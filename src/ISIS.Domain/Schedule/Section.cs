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
        private Guid _termId;
        private ISet<CourseTypes> _courseTypes = new HashSet<CourseTypes>();
        private CreditTypes _creditType;
        private string _title;
        private bool _hasDates;

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
                ChangeDates(termData.Start, termData.End);

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

            ChangeCreditType(courseData.CreditType);

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

        public void ChangeCreditType(CreditTypes creditType)
        {

            if (creditType == _creditType)
                return;

            ApplyEvent(new SectionCreditTypeChangedEvent(
                           EventSourceId,
                           creditType));


            var newCourseType = CourseTypes.CE;
            switch (creditType)
            {
                case CreditTypes.ContractTrainingFunded:
                case CreditTypes.GrantFunded:
                case CreditTypes.WorkforceFunded:
                    newCourseType = CourseTypes.CWECM;
                    break;
            }

            ApplyEvent(new SectionCourseTypeAddedEvent(
                           EventSourceId,
                           newCourseType,
                           _courseTypes.Union(new[] {newCourseType}).Distinct()));


            foreach (var courseType in _courseTypes.Except(new[] { newCourseType }).ToArray())
                ApplyEvent(new SectionCourseTypeRemovedEvent(
                               EventSourceId,
                               courseType,
                               _courseTypes.Except(new[] {courseType}).ToArray()));
        }

        public void ChangeDates(DateTime startDate, DateTime endDate)
        {
            var uow = UnitOfWorkContext.Current;
            var term = uow.GetById<Term>(_termId);
            var termData = term.BuildMememto();

            if (endDate < termData.Start || startDate > termData.End)
                throw new InvalidStateException("Your attempt to create a section failed. The section census date is outside the term dates.");

            ApplyEvent(new SectionDatesChangedEvent(
                           EventSourceId,
                           startDate,
                           endDate));
        }


        public void ChangeSectionNumber(string sectionNumber)
        {
            ApplyEvent(new SectionNumberChangedEvent(
                           EventSourceId,
                           sectionNumber));
        }


        public void ChangeCEUs(decimal ceus)
        {
            ApplyEvent(new SectionCEUsChangedEvent(
                           EventSourceId,
                           ceus));
        }

        public void ChangeTitle(string newTitle)
        {
            ApplyEvent(new SectionTitleChangedEvent(
                           EventSourceId,
                           _title,
                           newTitle));
        }


        public void ChangeTerm(Term term)
        {
            var termData = term.BuildMememto();

            ApplyEvent(new SectionTermChangedEvent(
                           EventSourceId,
                           termData.Id,
                           termData.Abbreviation,
                           termData.Name));

            if (_hasDates)
                ApplyEvent(new SectionDatesRemovedEvent(
                               EventSourceId));
        }

        protected void OnCreated(SectionCreatedEvent @event)
        {
            _termId = @event.TermId;
        }

        protected void OnDatesChanged(SectionDatesChangedEvent @event)
        {
            _hasDates = true;
        }

        protected void OnDatedRemoved(SectionDatesRemovedEvent @event)
        {
            _hasDates = false;
        }

        protected void OnTitleChanged(SectionTitleChangedEvent @event)
        {
            _title = @event.Title;
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

        protected void OnCourseTypeRemoved(SectionCourseTypeRemovedEvent @event)
        {
            _courseTypes.Remove(@event.CourseTypeRemoved);
        }

        protected void OnCreditTypeChanged(SectionCreditTypeChangedEvent @event)
        {
            _creditType = @event.CreditType;
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

        protected void OnSectionNumberChanged(SectionNumberChangedEvent @event)
        {
        }

        protected void OnTermChanged(SectionTermChangedEvent @event)
        {
            _termId = @event.TermId;
        }

    }
}
