using System.Collections.Generic;

namespace ISIS
{
    public class PageOf<T>
    {

        public PageOf(long entityCount, long pageNumber, int pageSize, IEnumerable<T> results)
        {
            EntityCount = entityCount;
            PageNumber = pageNumber;
            PageSize = pageSize;
            Results = results;
        }

        public long EntityCount { get; private set; }
        public long PageNumber { get; private set; }
        public int PageSize { get; private set; }
        public IEnumerable<T> Results { get; private set; }
        public long PageCount
        {
            get
            {
                var remainder = EntityCount%PageSize;
                return (EntityCount - remainder)/PageSize + (remainder%1);
            }
        }

    }
}
