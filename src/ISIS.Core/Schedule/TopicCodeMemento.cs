using System;

namespace ISIS.Schedule
{
    public struct TopicCodeMemento
    {
        private readonly Guid _id;
        private readonly string _abbreviation;
        private readonly string _description;

        public TopicCodeMemento(Guid id, string abbreviation, string description)
        {
            _id = id;
            _abbreviation = abbreviation;
            _description = description;
        }

        public Guid Id { get { return _id; } }
        public string Abbreviation { get { return _abbreviation; } }
        public string Description { get { return _description; } }
    }
}
