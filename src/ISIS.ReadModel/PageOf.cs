using System.Collections.Generic;

namespace ISIS
{
    public class PageOf<T>
    {

        public PageOf(long entityCount, int pageNumber, int pageSize, IEnumerable<T> results)
        {
            EntityCount = entityCount;
            PageNumber = pageNumber;
            PageSize = pageSize;
            Results = results;
        }

        public long EntityCount { get; private set; }
        public int PageNumber { get; private set; }
        public int PageSize { get; private set; }
        public IEnumerable<T> Results { get; private set; }
        public int PageCount
        {
            get
            {
                var remainder = (int)EntityCount%PageSize;
                var wholePages =  ((int)EntityCount - remainder)/PageSize;
                return wholePages + (remainder == 0 ? 0 : 1);
            }
        }

    }
}
