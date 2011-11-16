using System.Web.Routing;

namespace System.Web.Mvc
{
    public static class UrlHelperExtensions
    {

        public static string StateUrl(this UrlHelper helper)
        {
            return helper.StateUrl(new RouteValueDictionary());
        }

        public static string StateUrl(this UrlHelper helper, object routeValues)
        {
            return helper.StateUrl(new RouteValueDictionary(routeValues));
        }

        public static string StateUrl(this UrlHelper helper, RouteValueDictionary routeValues)
        {
            RouteValueDictionary v = new RouteValueDictionary();

            foreach (var kvp in helper.RequestContext.RouteData.Values)
                v[kvp.Key] = kvp.Value;

            //foreach (var kvp in helper.RequestContext.RouteData.DataTokens)
            //    v[kvp.Key] = kvp.Value;

            foreach (var key in helper.RequestContext.HttpContext.Request.QueryString.AllKeys)
                v[key] = helper.RequestContext.HttpContext.Request.QueryString[key];

            foreach (var kvp in routeValues)
                v[kvp.Key] = kvp.Value;
            
            return helper.RouteUrl(v);
        }

    }
}
