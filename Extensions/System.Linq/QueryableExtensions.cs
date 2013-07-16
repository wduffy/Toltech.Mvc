using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace System.Linq
{
    public static class QueryableExtensions
    {

        // Just call asenumerable and return ??
        //public static IEnumerable<SelectListItem> AsSelectList<T>(this IQueryable<T> source, Func<T, string> text, Func<T, object> value)
        //{
        //    return source.AsEnumerable().Select(x => new SelectListItem
        //    {
        //        Text = text.Invoke(x),
        //        Value = value.Invoke(x).ToString()
        //    });
        //}

    }
}
