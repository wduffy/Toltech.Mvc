using System;
using System.Collections.Generic;
using System.Web.Routing;
using Toltech.Mvc.Tools;

namespace System.Web.Mvc.Html
{
    public static partial class HtmlHelperExtensions
    {

        public static MvcHtmlString ImageRandom(this HtmlHelper htmlHelper, string imageFolder, string altTag)
        {
            return htmlHelper.ImageRandom(imageFolder, altTag, new RouteValueDictionary());
        }

        public static MvcHtmlString ImageRandom(this HtmlHelper htmlHelper, string imageFolder, string altTag, object htmlAttributes)
        {
            return htmlHelper.ImageRandom(imageFolder, altTag, new RouteValueDictionary(htmlAttributes));
        }

        public static MvcHtmlString ImageRandom(this HtmlHelper htmlHelper, string imageFolder, string altTag, IDictionary<string, object> htmlAttributes)
        {
            if (string.IsNullOrEmpty(imageFolder))
                throw new ArgumentException("Value cannot be null or empty.", "imageFolder");

            return htmlHelper.Image(ImageTools.GetRandomImage(imageFolder), altTag, htmlAttributes);
        }

    }
}