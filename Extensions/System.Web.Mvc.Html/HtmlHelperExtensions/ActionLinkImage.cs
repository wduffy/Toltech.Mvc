using System.Collections.Generic;
using System.Web.Routing;

namespace System.Web.Mvc.Html
{
    public static partial class HtmlHelperExtensions
    {

        public static MvcHtmlString ActionLinkImage(this HtmlHelper htmlHelper, string imageLocation, string altTag, string actionName)
        {
            return htmlHelper.ActionLinkImage(imageLocation, altTag, actionName, null, new RouteValueDictionary(), new RouteValueDictionary());
        }

        public static MvcHtmlString ActionLinkImage(this HtmlHelper htmlHelper, string imageLocation, string altTag, string actionName, object routeValues)
        {
            return htmlHelper.ActionLinkImage(imageLocation, altTag, actionName, null, new RouteValueDictionary(routeValues), new RouteValueDictionary());
        }

        public static MvcHtmlString ActionLinkImage(this HtmlHelper htmlHelper, string imageLocation, string altTag, string actionName, string controllerName)
        {
            return htmlHelper.ActionLinkImage(imageLocation, altTag, actionName, controllerName, new RouteValueDictionary(), new RouteValueDictionary());
        }

        public static MvcHtmlString ActionLinkImage(this HtmlHelper htmlHelper, string imageLocation, string altTag, string actionName, RouteValueDictionary routeValues)
        {
            return htmlHelper.ActionLinkImage(imageLocation, altTag, actionName, null, routeValues, new RouteValueDictionary());
        }

        public static MvcHtmlString ActionLinkImage(this HtmlHelper htmlHelper, string imageLocation, string altTag, string actionName, object routeValues, object htmlAttributes)
        {
            return htmlHelper.ActionLinkImage(imageLocation, altTag, actionName, null, new RouteValueDictionary(routeValues), new RouteValueDictionary(htmlAttributes));
        }

        public static MvcHtmlString ActionLinkImage(this HtmlHelper htmlHelper, string imageLocation, string altTag, string actionName, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes)
        {
            return htmlHelper.ActionLinkImage(imageLocation, altTag, actionName, null, routeValues, htmlAttributes);
        }

        public static MvcHtmlString ActionLinkImage(this HtmlHelper htmlHelper, string imageLocation, string altTag, string actionName, string controllerName, object routeValues, object htmlAttributes)
        {
            return htmlHelper.ActionLinkImage(imageLocation, altTag, actionName, controllerName, new RouteValueDictionary(routeValues), new RouteValueDictionary(htmlAttributes));
        }

        public static MvcHtmlString ActionLinkImage(this HtmlHelper htmlHelper, string imageLocation, string altTag, string actionName, string controllerName, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes)
        {
            var urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext, htmlHelper.RouteCollection);
            var link = new TagBuilder("a");
            link.MergeAttribute("href", urlHelper.Action(actionName, controllerName, routeValues));
            link.InnerHtml = htmlHelper.Image(imageLocation, altTag, htmlAttributes).ToString();

            return MvcHtmlString.Create(link.ToString(TagRenderMode.Normal));
        }

    }
}
