using System;
using System.Collections.Generic;
using System.Web.Routing;
using Toltech.Mvc.Tools;

namespace System.Web.Mvc.Html
{
    public static partial class HtmlHelperExtensions
    {

        public static string UploadImage(this HtmlHelper htmlHelper, string name, object value)
        {
            return htmlHelper.UploadImage(name, value, new RouteValueDictionary());
        }

        public static string UploadImage(this HtmlHelper htmlHelper, string name, object value, object htmlAttributes)
        {
            return htmlHelper.UploadImage(name, value, new RouteValueDictionary(htmlAttributes));
        }

        public static string UploadImage(this HtmlHelper htmlHelper, string name, object value, IDictionary<string, object> htmlAttributes)
        {
            return "<img src=\"/imagehandler.axd?path=/Content/Uploads/" + value.ToString() + "&amp;width=100\" alt=\"\" /><br />" + htmlHelper.Upload(name, value, htmlAttributes);
        }

    }
}