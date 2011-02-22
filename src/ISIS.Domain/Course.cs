using Ncqrs.Domain;

namespace ISIS
{

    public class Course : AggregateRootMappedByConvention
    {

        private string _cip;
        private string _approvalNumber;

        private Course()
        {
        }

        /// <summary>
        /// Creates a new course section
        /// </summary>
        /// <param name="rubric">Course subject. For example: BIOL</param>
        /// <param name="number">4-digit course number. For example: 2302</param>
        /// <param name="title">Course title</param>
        /// <example>new Course("BIOL","2302","Anatomy and Physiology 1");</example>
        public Course(string rubric,
            string number,
            string title)
        {
            var e = new CourseCreatedEvent()
                        {
                            Rubric = rubric,
                            Number = number,
                            Title = title
                        };
            ApplyEvent(e);
        }

        protected void OnCourseCreated(CourseCreatedEvent @event)
        {
        }

        /// <summary>
        /// Assigns a classification of instructional programs (CIP) code to a course
        /// </summary>
        /// <param name="cip">Classification of Instructional Programs code without punctuation. For CIP 11.0701, pass 110701</param>
        public void AssignCIPNumber(string cip)
        {
            if (!string.IsNullOrWhiteSpace(_approvalNumber))
                throw new InvalidStateException("Your attempt to assign a CIP failed because the course already has an approval number.");

            var e = new CourseCIPAssignedEvent()
                        {
                            CIP = cip
                        };
            ApplyEvent(e);
        }

        protected void OnCourseCIPAssigned(CourseCIPAssignedEvent @event)
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
            if (!string.IsNullOrWhiteSpace(_cip))
                throw new InvalidStateException("Your attempt to assign an approval number failed because the course already has a CIP code.");

            var approvalNumberEvent = new CourseApprovalNumberAssignedEvent()
            {
                ApprovalNumber = approvalNumber
            };
            ApplyEvent(approvalNumberEvent);

            var CIPEvent = new CourseCIPAssignedEvent()
                               {
                                   CIP = approvalNumber.Substring(0, 6)
                               };
            ApplyEvent(CIPEvent);

        }

        protected void OnApprovalNumberAssigned(CourseApprovalNumberAssignedEvent @event)
        {
            _approvalNumber = @event.ApprovalNumber;
        }

        public void ChangeCourseTitle(string newTitle)
        {
            var e = new CourseTitleChangedEvent()
                        {
                            Title = newTitle
                        };
            ApplyEvent(e);
        }

        protected void OnCourseTitleChanged(CourseTitleChangedEvent @event)
        {
            
        }

    }
}
