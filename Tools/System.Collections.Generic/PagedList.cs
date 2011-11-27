using System;

namespace System.Collections.Generic
{

    public interface IPagedEnumerable : IEnumerable
    {
        int Page { get; }
        int PageSize { get; }
        int TotalRecords { get; }
        int TotalPages { get; }
    }

    public interface IPagedEnumerable<T> : IPagedEnumerable, IEnumerable<T>
    {
    }

    public class PagedList<T> : List<T>, IPagedEnumerable<T>
    {

        public int Page { get; internal set; }
        public int PageSize { get; internal set; }
        public int TotalRecords { get; internal set; }

        public int TotalPages
        {
            get
            {
                if ((TotalRecords > 0) && (PageSize > 0)) // Avoid System.DivideByZeroException
                    return (int)Math.Ceiling((decimal)TotalRecords / (decimal)PageSize);
                else
                    return 0;
            }
        }

        public PagedList()
        {
            // Required for non-nulling automapper objects
        }

        public PagedList(IEnumerable<T> collection, int page, int pageSize, int totalRecords)
            : base(collection)
        {
            Page = page;
            PageSize = pageSize;
            TotalRecords = totalRecords;
        }

    }

}