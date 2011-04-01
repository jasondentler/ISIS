using System;
using System.Collections.Generic;
using System.Linq;
using Ncqrs;
using Ncqrs.Domain;

namespace ISIS.Schedule
{

    public class Course : AggregateRootMappedByConvention
    {
        private readonly IClock _clock;

        public enum Statuses
        {
            Active,
            Inactive,
            Pending,
            Obsolete
        }

        private bool _isCreditCourse;
        private string _cip;
        private string _approvalNumber;
        private string _title;
        private string _longTitle;
        private string _description;
        private Statuses _status;
        private readonly HashSet<CourseTypes> _courseTypes = new HashSet<CourseTypes>();
        private CreditTypes _creditType;
        private decimal _ceus;
        private Guid _topicCodeId;

        private Course(IClock clock)
        {
            _clock = clock;
        }

        /// <summary>
        /// Creates a new course section
        /// </summary>
        /// <param name="eventSourceId">Course id</param>
        /// <param name="rubric">Course subject. For example: BIOL</param>
        /// <param name="number">4-digit course number. For example: 2302</param>
        /// <param name="longTitle">Course title</param>
        /// <param name="clock">Provides the current date/time</param>
        /// <example>new Course(Guid.NewGuid(), "BIOL","2302","Anatomy and Physiology 1");</example>
        public Course(
            Guid eventSourceId,
            string rubric,
            string number,
            string longTitle,
            IEnumerable<CourseTypes> courseTypes,
            IClock clock)
            : base(eventSourceId)
        {
            _clock = clock;
            var shortTitle = longTitle.Length > 30 ? longTitle.Substring(0, 30) : longTitle;
            ApplyEvent(new CreditCourseCreatedEvent(eventSourceId, rubric, number));
            ApplyEvent(new CourseTitleChangedEvent(eventSourceId, shortTitle));
            ApplyEvent(new CourseLongTitleChangedEvent(eventSourceId, longTitle));
            ApplyEvent(new CourseActivatedEvent(eventSourceId, clock.UtcNow()));
            foreach (var courseType in courseTypes)
                AddCourseType(courseType);
        }

        /// <summary>
        /// Creates a new course section
        /// </summary>
        /// <param name="eventSourceId">Course id</param>
        /// <param name="rubric">Course subject. For example: BIOL</param>
        /// <param name="number">4-digit course number. For example: 2302</param>
        /// <param name="longTitle">Course title</param>
        /// <param name="type">The Credit Type of the course (Grant Funded, Special Interests, etc)</param>
        /// <param name="effectiveDate">The date/time that this course is activated</param>
        /// <param name="clock">Provides the current date/time</param>
        /// <example>new Course(Guid.NewGuid(), "MUSI","1001","Music for seniors");</example>
        public Course(
            Guid eventSourceId,
            string rubric,
            string number,
            string longTitle,
            CreditTypes type,
            DateTime? effectiveDate,
            IClock clock)
            : base(eventSourceId)
        {
            var shortTitle = longTitle.Length > 30 ? longTitle.Substring(0, 30) : longTitle;
            ApplyEvent(new ContinuingEducationCourseCreatedEvent(eventSourceId, rubric, number));
            ApplyEvent(new CourseTitleChangedEvent(eventSourceId, shortTitle));
            ApplyEvent(new CourseLongTitleChangedEvent(eventSourceId, longTitle));
            ApplyEvent(new CourseActivatedEvent(eventSourceId, effectiveDate ?? clock.UtcNow()));
            ChangeCreditType(type);
            ApplyEvent(new CourseCEUsChangedEvent(eventSourceId, 0M));
        }

        protected void OnCreditCourseCreated(CreditCourseCreatedEvent @event)
        {
            _isCreditCourse = true;
        }

        protected void OnContinuingEducationCourseCreated(ContinuingEducationCourseCreatedEvent @event)
        {
            _isCreditCourse = false;
        }

        /// <summary>
        /// Assigns a classification of instructional programs (CIP) code to a course
        /// </summary>
        /// <param name="cip">Classification of Instructional Programs code without punctuation. For CIP 11.0701, pass 110701</param>
        public void AssignCIP(string cip)
        {
            if (_cip != cip)
            {
                var cipEvent = new CourseCIPChangedEvent(EventSourceId, cip);
                ApplyEvent(cipEvent);
            }

            if (!string.IsNullOrEmpty(_approvalNumber))
            {
                var approvalNumberEvent = new CourseApprovalNumberChangedEvent(EventSourceId, null);
                ApplyEvent(approvalNumberEvent);
            }
        }

        protected void OnCourseCIPAssigned(CourseCIPChangedEvent @event)
        {
            _cip = @event.CIP;
        }

        /// <summary>
        /// Assigns an approval number to a course
        /// </summary>
        /// <param name="approvalNumber">A 10 digit approval number from the Academic Course Guide Manual (ACGM)</param>
        /// <remarks>ACGM: http://www.thecb.state.tx.us/AAR/UndergraduateEd/WorkforceEd/acgm.htm </remarks>
        public void AssignApprovalNumber(string approvalNumber)
        {

            if (approvalNumber == _approvalNumber)
                return;

            var approvalNumberEvent = new CourseApprovalNumberChangedEvent(EventSourceId, approvalNumber);
            ApplyEvent(approvalNumberEvent);

            var cip = approvalNumber.Substring(0, 6);
            var cipEvent = new CourseCIPChangedEvent(EventSourceId, cip);
            ApplyEvent(cipEvent);
        }

        protected void OnApprovalNumberAssigned(CourseApprovalNumberChangedEvent @event)
        {
            _approvalNumber = @event.ApprovalNumber;
        }

        public void ChangeCourseTitle(string newTitle)
        {
            if (_title == newTitle)
                return;
            if (_title == _longTitle)
                ApplyEvent(new CourseLongTitleChangedEvent(EventSourceId, newTitle));
            ApplyEvent(new CourseTitleChangedEvent(EventSourceId, newTitle));
        }

        protected void OnCourseTitleChanged(CourseTitleChangedEvent @event)
        {
            _title = @event.Title;
        }

        public void ChangeCourseLongTitle(string newLongTitle)
        {
            if (_longTitle == newLongTitle)
                return;
            ApplyEvent(new CourseLongTitleChangedEvent(EventSourceId, newLongTitle));
            var newShortTitle = newLongTitle.Length > 30 ? newLongTitle.Substring(0, 30) : newLongTitle;
            ApplyEvent(new CourseTitleChangedEvent(EventSourceId, newShortTitle));
        }

        protected void OnCourseLongTitleChanged(CourseLongTitleChangedEvent @event)
        {
            _longTitle = @event.LongTitle;
        }

        public void ChangeDescription(string newDescription)
        {
            if (_description != newDescription)
                ApplyEvent(new CourseDescriptionChangedEvent(EventSourceId, newDescription));
        }

        protected void OnCourseDescriptionChanged(CourseDescriptionChangedEvent @event)
        {
            _description = @event.Description;
        }

        public void Activate()
        {
            if (_status != Statuses.Active)
                ApplyEvent(new CourseActivatedEvent(EventSourceId, _clock.UtcNow()));
        }

        protected void OnCourseActivated(CourseActivatedEvent @event)
        {
            _status = Statuses.Active;
        }

        public void Deactivate()
        {
            if (_status != Statuses.Inactive)
                ApplyEvent(new CourseDeactivatedEvent(EventSourceId));
        }

        protected void OnCourseDeactivated(CourseDeactivatedEvent @event)
        {
            _status = Statuses.Inactive;
        }


        public void MakePending()
        {
            if (_status != Statuses.Pending)
                ApplyEvent(new CourseMadePendingEvent(EventSourceId));
        }

        protected void OnCourseMadePending(CourseMadePendingEvent @event)
        {
            _status = Statuses.Pending;
        }

        public void MakeObsolete()
        {
            if (_status != Statuses.Obsolete)
                ApplyEvent(new CourseMadeObsoleteEvent(EventSourceId));
        }

        protected void OnCourseMadeObsolete(CourseMadeObsoleteEvent @event)
        {
            _status = Statuses.Obsolete;
        }

        public void AddCourseType(CourseTypes type)
        {
            if (!_isCreditCourse)
                throw new InvalidStateException(
                    "Your attempt to add the course type failed because this is a Continuing Education course. Course type is determined by credit type.");

            if (_courseTypes.Contains(type)) return;
            ApplyEvent(new CourseTypeAddedToCourseEvent(
                           EventSourceId,
                           type,
                           _courseTypes.Union(new[] {type}).ToArray()));
        }

        protected void OnCourseTypeAddedToCourse(CourseTypeAddedToCourseEvent @event)
        {
            _courseTypes.Add(@event.TypeAdded);
        }

        public void RemoveCourseType(CourseTypes type)
        {
            if (!_isCreditCourse)
                throw new InvalidStateException(
                    "Your attempt to remove the course type failed because this is a Continuing Education course. Course type is determined by credit type.");

            if (!_courseTypes.Contains(type)) return;
            
            if (_courseTypes.Count == 1)
                throw new InvalidStateException(
                    "Your attempt to remove the course type failed because it's the last one. Each course must have at least one course type.");

            ApplyEvent(new CourseTypeRemovedFromCourseEvent(
                           EventSourceId,
                           type,
                           _courseTypes.Except(new[] {type}).ToArray()));
        }

        protected void OnCourseTypeRemovedFromCourse(CourseTypeRemovedFromCourseEvent @event)
        {
            _courseTypes.Remove(@event.TypeRemoved);
        }
    
        public void ChangeCreditType(CreditTypes creditType)
        {
            if (_isCreditCourse)
                throw new InvalidStateException(
                    "Your attempt to change the credit type failed because this is a credit course. Credit type may only be set on Continuing Education courses.");

            if (_creditType == creditType)
                return;

            ApplyEvent(new CourseCreditTypeChangedEvent(EventSourceId, creditType));

            var newCourseType = CourseTypes.CE;
            switch (creditType)
            {
                case CreditTypes.ContractTrainingFunded:
                case CreditTypes.GrantFunded:
                case CreditTypes.WorkforceFunded:
                    newCourseType = CourseTypes.CWECM;
                    break;
            }

            var toAdd = (new[] {newCourseType})
                .Except(_courseTypes)
                .ToArray();

            var toRemove = _courseTypes
                .Except(new[] {newCourseType})
                .ToArray();

            foreach (var courseType in toAdd)
                ApplyEvent(new CourseTypeAddedToCourseEvent(EventSourceId,
                                                            courseType,
                                                            _courseTypes.Union(new[] {courseType})));

            foreach (var courseType in toRemove)
                ApplyEvent(new CourseTypeRemovedFromCourseEvent(EventSourceId,
                                                                courseType,
                                                                _courseTypes.Except(new[] {courseType})));
        }

        protected void OnCourseCreditTypeChanged(CourseCreditTypeChangedEvent @event)
        {
            _creditType = @event.CreditType;
        }

        public void ChangeCEUs(decimal ceus)
        {
            if (_isCreditCourse)
                throw new InvalidStateException(
                    "Your attempt to change the CEUs failed because this is a credit course. CEUs may only be set on Continuing Education courses.");

            if (_ceus == ceus)
                return;

            ApplyEvent(new CourseCEUsChangedEvent(EventSourceId, ceus));
        }

        protected void OnCourseCEUsChanged(CourseCEUsChangedEvent @event)
        {
            _ceus = @event.Ceus;
        }

        public void ChangeTopicCode(TopicCode topicCode)
        {
            var memento = topicCode.BuildMemento();

            if (_topicCodeId == memento.Id)
                return;

            ApplyEvent(new CourseTopicCodeChangedEvent(EventSourceId, memento));
        }

        protected void OnTopicCodeChanged(CourseTopicCodeChangedEvent @event)
        {
            _topicCodeId = @event.TopicCodeId;
        }

    }
}
