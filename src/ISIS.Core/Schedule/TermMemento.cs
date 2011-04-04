using System;

namespace ISIS.Schedule
{
    public struct TermMemento
    {
        private readonly Guid _id;
        private readonly string _abbreviation;
        private readonly string _name;
        private readonly DateTime _start;
        private readonly DateTime _end;

        public TermMemento(
            Guid id, 
            string abbreviation, 
            string name, 
            DateTime start,
            DateTime end)
        {
            _id = id;
            _abbreviation = abbreviation;
            _name = name;
            _start = start;
            _end = end;
        }

        public Guid Id { get { return _id; } }
        public string Abbreviation { get { return _abbreviation; } }
        public string Name { get { return _name; } }
        public DateTime Start { get { return _start; } }
        public DateTime End { get { return _end; } }

    }
}
