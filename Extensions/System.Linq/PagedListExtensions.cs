using System;
using System.Collections.Generic;

namespace System.Linq
{
    public static class PagedListExtensions
    {
 
        public static IPagedEnumerable<T> ToPagedList<T>(this IQueryable<T> source, int page, int pageSize = 25)
        {
            var count = source.Count();
            var list = source.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            return new PagedList<T>(list, page, pageSize, count);
        }

        /// <summary>
        /// Automapper Helper to map "Page", "PageSize" and "TotalRecords" after AutoMapper.Map has been called on IPagedEnumerable
        /// </summary>
        public static void WithPagerValues<T>(this IPagedEnumerable<T> pagedList, IPagedEnumerable source)
        {
            var destination = (PagedList<T>)pagedList;
            destination.Page = source.Page;
            destination.PageSize = source.PageSize;
            destination.TotalRecords = source.TotalRecords;
        }

    }
}
