using System;
using System.Collections.Generic;

namespace ISIS.Schedule
{
    public struct CourseMemento
    {
        private readonly Guid _id;
        private readonly string _rubric;
        private readonly string _courseNumber;
        private readonly string _title;
        private readonly string _longTitle;
        private readonly bool _isCredit;
        private readonly IEnumerable<CourseTypes> _courseTypes;
        private readonly string _approvalNumber;
        private readonly string _cip;
        private readonly CreditTypes _creditType;
        private readonly decimal _ceus;
        private readonly Guid _topicCodeId;

        public CourseMemento(
            Guid id,
            string rubric,
            string courseNumber,
            string title,
            string longTitle,
            bool isCredit,
            IEnumerable<CourseTypes> courseTypes,
            string approvalNumber,
            string cip,
            CreditTypes creditType,
            decimal ceus,
            Guid topicCodeId)
        {
            _id = id;
            _rubric = rubric;
            _courseNumber = courseNumber;
            _title = title;
            _longTitle = longTitle;
            _isCredit = isCredit;
            _courseTypes = courseTypes;
            _approvalNumber = approvalNumber;
            _cip = cip;
            _creditType = creditType;
            _ceus = ceus;
            _topicCodeId = topicCodeId;
        }

        public Guid Id { get { return _id; } }
        public string Rubric { get { return _rubric; } }
        public string CourseNumber { get { return _courseNumber; } }
        public string Title { get { return _title; } }
        public string LongTitle { get { return _longTitle; } }
        public IEnumerable<CourseTypes> CourseTypes { get { return _courseTypes; } }
        public bool IsCredit { get { return _isCredit; } }
        public string ApprovalNumber { get { return _approvalNumber; } }
        public string CIP { get { return _cip; } }
        public CreditTypes CreditType { get { return _creditType; }}
        public decimal CEUs { get { return _ceus; } }
        public Guid TopicCodeId { get { return _topicCodeId; } }

    }
}
