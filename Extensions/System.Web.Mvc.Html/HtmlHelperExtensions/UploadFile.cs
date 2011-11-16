using System;
using System.Collections.Generic;
using System.Web.Routing;
using Toltech.Mvc.Tools;

namespace System.Web.Mvc.Html
{
    public static partial class HtmlHelperExtensions
    {

        public static string UploadFile(this HtmlHelper htmlHelper, string name, object value)
        {
            return htmlHelper.UploadFile(name, value, new RouteValueDictionary());
        }

        public static string UploadFile(this HtmlHelper htmlHelper, string name, object value, object htmlAttributes)
        {
            return htmlHelper.UploadFile(name, value, new RouteValueDictionary(htmlAttributes));
        }

        public static string UploadFile(this HtmlHelper htmlHelper, string name, object value, IDictionary<string, object> htmlAttributes)
        {
            return "[ " + value.ToString() + " ]<br />" + htmlHelper.Upload(name, value, htmlAttributes);
        }

    }
}