using System.Collections.Generic;
using System.Web.Routing;

namespace System.Web.Mvc.Html
{
    public static partial class HtmlHelperExtensions
    {

        public static MvcHtmlString Image(this HtmlHelper htmlHelper, string imageLocation, string altTag)
        {
            return htmlHelper.Image(imageLocation, altTag, new RouteValueDictionary());
        }

        public static MvcHtmlString Image(this HtmlHelper htmlHelper, string imageLocation, string altTag, object htmlAttributes)
        {
            return htmlHelper.Image(imageLocation, altTag, new RouteValueDictionary(htmlAttributes));
        }

        public static MvcHtmlString Image(this HtmlHelper htmlHelper, string imageLocation, string altTag, IDictionary<string, object> htmlAttributes)
        {
            var urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext, htmlHelper.RouteCollection);

            if (string.IsNullOrEmpty(imageLocation))
                throw new ArgumentException("Value cannot be null or empty.", "imageLocation");

            var image = new TagBuilder("img");
            image.MergeAttributes<string, object>(htmlAttributes);
            image.MergeAttribute("src", urlHelper.Content(imageLocation));
            image.MergeAttribute("alt", htmlHelper.Encode(altTag));

            return MvcHtmlString.Create(image.ToString(TagRenderMode.SelfClosing));
        }

    }
}