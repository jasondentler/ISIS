using System.Collections.Generic;

namespace ISIS
{

    public interface IQuery
    {
        string QueryName { get; }
        Dictionary<string, object> GetParameters();
    }

    public interface ISingleQuery<TResult> : IQuery
    {
    }

    public interface IListQuery<TElement> : IQuery
    {
    }

}
