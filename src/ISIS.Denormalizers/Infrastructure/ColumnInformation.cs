using System;

namespace ISIS.Infrastructure
{
    public class ColumnInformation
    {
        protected readonly string ColumnName;
        protected readonly Type Type;

        public ColumnInformation(string columnName, Type type)
        {
            ColumnName = columnName;
            Type = type;
        }

        protected string GetSqlType()
        {
            if (Type == typeof(string))
                return "nvarchar(MAX)";
            if (Type == typeof(int))
                return "int";
            if (Type == typeof(Guid))
                return "uniqueidentifier";
            throw new NotSupportedException(
                string.Format("Type {0} isn't mapped to a SQL type", Type));
        }

        public override string ToString()
        {
            return string.Format("{0} {1}", ColumnName, GetSqlType());
        }

    }

    public class ColumnInformation<T>
        : ColumnInformation
    {
        public ColumnInformation(string columnName)
            : base(columnName, typeof(T))
        {
        }
    }

    public class PrimaryKeyInformation<T>
        : ColumnInformation<T>
    {
        public PrimaryKeyInformation(string columnName)
            : base(columnName)
        {
        }

        public override string ToString()
        {
            return string.Format("{0} {1} PRIMARY KEY", ColumnName, GetSqlType());
        }
    }




}
