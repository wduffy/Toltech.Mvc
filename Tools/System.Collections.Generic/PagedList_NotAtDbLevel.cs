using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Web.Mvc
{

    public interface IPagedList
    {
        int Page { get; }
        int PageSize { get; }
        int TotalRecords { get; }
        int TotalPages { get; }
    }

    public class PagedList<T> : List<T>, IPagedList
    {
        private int _page;
        private int _pageSize;
        private int _totalRecords;

        public PagedList(IList<T> items, int? page) : this(items, page, null) { }
        public PagedList(IList<T> items, int? page, int? pageSize)
        {
            _page = page ?? 1;
            _pageSize = pageSize ?? 10;
            _totalRecords = items.Count();

            this.AddRange(items.Skip((Page - 1) * PageSize).Take(PageSize));
        }
        private PagedList(IList<T> items, int page, int pagesize, int totalRecords) // Used by ToPagedListOf to convert PagedList types without loss of original full list count
        {
            _page = page;
            _pageSize = pagesize;
            _totalRecords = totalRecords;
            this.AddRange(items);
        }
        
        #region IPagedList Members

        public int Page
        {
            get
            {
                if (_page < 1)
                    return 1;
                else if (_page > TotalPages)
                    return TotalPages;
                else
                    return _page;
            }
        }

        public int PageSize
        {
            get
            {
                return _pageSize;
            }
        }

        public int TotalRecords
        {
            get
            {
                return _totalRecords;
            }
        }

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

        #endregion

        public PagedList<TResult> ToPagedListOf<TResult>(Converter<T, TResult> convertor)
        {
            var conversion = new List<TResult>();
            this.ForEach(x => conversion.Add(convertor.Invoke(x)));
            return new PagedList<TResult>(conversion, _page, _pageSize, _totalRecords);
        }

    }

}