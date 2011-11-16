using System.Collections.Generic;
using System.Web.Routing;

namespace System.Web.Mvc.Html
{
    public static partial class HtmlHelperExtensions
    {

        public static string ActionLinkSelected(this HtmlHelper htmlHelper, string linkText, string url, string actionName)
        {
            return htmlHelper.ActionLinkSelected(linkText, url, actionName, null, new RouteValueDictionary(), new RouteValueDictionary());
        }

        public static string ActionLinkSelected(this HtmlHelper htmlHelper, string linkText, string url, string actionName, object routeValues)
        {
            return htmlHelper.ActionLinkSelected(linkText, url, actionName, null, new RouteValueDictionary(routeValues), new RouteValueDictionary());
        }

        public static string ActionLinkSelected(this HtmlHelper htmlHelper, string linkText, string url, string actionName, string controllerName)
        {
            return htmlHelper.ActionLinkSelected(linkText, url, actionName, controllerName, new RouteValueDictionary(), new RouteValueDictionary());
        }

        public static string ActionLinkSelected(this HtmlHelper htmlHelper, string linkText, string url, string actionName, RouteValueDictionary routeValues)
        {
            return htmlHelper.ActionLinkSelected(linkText, url, actionName, null, routeValues, new RouteValueDictionary());
        }

        public static string ActionLinkSelected(this HtmlHelper htmlHelper, string linkText, string url, string actionName, object routeValues, object htmlAttributes)
        {
            return htmlHelper.ActionLinkSelected(linkText, url, actionName, null, new RouteValueDictionary(routeValues), new RouteValueDictionary(htmlAttributes));
        }

        public static string ActionLinkSelected(this HtmlHelper htmlHelper, string linkText, string url, string actionName, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes)
        {
            return htmlHelper.ActionLinkSelected(linkText, url, actionName, null, routeValues, htmlAttributes);
        }

        public static string ActionLinkSelected(this HtmlHelper htmlHelper, string linkText, string url, string actionName, string controllerName, object routeValues, object htmlAttributes)
        {
            return htmlHelper.ActionLinkSelected(linkText, url, actionName, controllerName, new RouteValueDictionary(routeValues), new RouteValueDictionary(htmlAttributes));
        }

        public static string ActionLinkSelected(this HtmlHelper htmlHelper, string linkText, string url, string actionName, string controllerName, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes)
        {
            if (string.IsNullOrEmpty(linkText))
                throw new ArgumentException("Value cannot be null or empty.", "linkText");

            if (string.IsNullOrEmpty(url) || url == "/")
                url = "/default.aspx";

            // Build the link
            var urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext, htmlHelper.RouteCollection);
            var link = new TagBuilder("a");
            link.InnerHtml = htmlHelper.Encode(linkText);
            link.MergeAttributes(htmlAttributes);
            link.MergeAttribute("href", urlHelper.Action(actionName, controllerName, routeValues));

            // Check to see if the link is to be selected
            if (htmlHelper.ViewContext.HttpContext.Request.RawUrl.StartsWith(url, StringComparison.OrdinalIgnoreCase))
                link.AddCssClass("selected");

            return link.ToString(TagRenderMode.Normal);
        }

    }
}
